using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySell.Dal;
using MySell.Dal.Model;
using MySell.Dal.Repository;
using MySell.Service.Products;

namespace MySell.Service.Orders
{
    public class OrderService : IOrderService
    {
        private OrderRepository orderRepository = null;
        private ProductSpecRepository productSpecRepository = null;
        private RefundRepository refundRepository = null;
        private LogisticsRepository logisticsRepository = null;
        public OrderService()
        {
            orderRepository = new OrderRepository();
            productSpecRepository = new ProductSpecRepository();
            refundRepository = new RefundRepository();
            logisticsRepository = new LogisticsRepository();
        }

        public ProOrder GetOrder(int id)
        {
            return orderRepository.GetOrder(id);
        }

        public ProOrder GetOrderByMobile(string mobile)
        {
            return orderRepository.GetOrderByMobile(mobile);
        }

        public ProOrder GetOrderByOrderNo(string orderNo)
        {
            return orderRepository.GetOrderByOrderNo(orderNo);
        }

        public Feedback Install(ProOrder order)
        {
            var proSpec = productSpecRepository.Get(order.ProductId);
            order.Amount = Math.Ceiling(proSpec.Price * order.Qty);//重新计算价格
            order.OrderNo = ProOrder.GetOrderNo();
            order.CreateDate = DateTime.Now;
            int result = orderRepository.Install(order);
            if (result < 1)
                return Feedback.Error("服务器繁忙下单失败，请重新下单！");

            //添加销售量
            new ProductShowService().AddSold(order.ProductId, order.Qty);
            //添加物流信息
            var log = new Logistics()
            {
                OrderNo = order.OrderNo,
                Message = string.Format("订单创建成功，您的订单号.no" + order.OrderNo),
                CreateDate = DateTime.Now
            };
            logisticsRepository.Install(log);
            //发送短信

            string uid = "ldony";
            string pwd = "eternal178";
            string mob = order.Mobile;
            string msg = string.Format("【优品购】您的订单：尊敬的{0}，您好。您的订单已经成功提交。快递大约三天左右到，请到时候注意签收。如需帮助请联系客服。", order.UserName);

            string backinfo = SMS.PostData("http://service.winic.org:8009/sys_port/gateway/index.asp?", "id=" + uid + "&pwd=" + pwd + "&to=" + mob + "&content=" + msg + "&time=");


            return Feedback.Ok("恭喜下单成功。", 0, order.OrderNo);
        }

        public List<ProOrder> Search(string name, Paged page)
        {
            var orderList = orderRepository.GetOrderAll(name, page);

            return orderList;
        }

        public Feedback UpdateStatus(int id, int status)
        {
            var order = orderRepository.GetOrder(id);
            if (order.Status == 2)
            {
                return Feedback.Failure("该订单已经退货，不能修改！");
            }
            else if (order.Status == 1 && status == 1)
            {
                return Feedback.Failure("该订单已处于发货中，无需修改！");
            }
            else if (order.Status == 1 && status == 0)
            {
                return Feedback.Failure("该订单已处于发货中，不能修改为未发货！");
            }

            var result = orderRepository.UpdateStatus(id, status, DateTime.Now);

            var text = "null";
            if (status == 1)
                text = "发货";
            else if (status == 2)
                text = "退货";

            if (result > 0)
                return Feedback.Ok(string.Format("订单已{0}成功！", text));

            return Feedback.Failure(string.Format("订单{0}失败！", text));
        }



        public List<Refund> SearchRefund(Paged page)
        {
            var list = refundRepository.GetRefundALL(page);

            return list;
        }

        public Feedback InstallRefund(Refund refund)
        {
            var order = orderRepository.GetOrderByOrderNo(refund.OrderNo);
            if (order == null)
            {
                return Feedback.Failure("订单号不存在，请检查！");
            }
            if (order.Mobile != refund.Mobile)
            {
                return Feedback.Failure("登记失败，手机号码与提交订单时的号码不一致，请检查！");
            }

            var result = refundRepository.Install(refund);
            if (result < 1)
                return Feedback.Failure("抱歉，退货申请失败，请联系客服处理！");

            if (refund.ProcessMode == "换货")
                orderRepository.UpdateStatus(order.Id, 4, DateTime.Now);
            else if (refund.ProcessMode == "退货")
                orderRepository.UpdateStatus(order.Id, 5, DateTime.Now);

            var log = new Logistics()
            {
                OrderNo = refund.OrderNo,
                Message = refund.ProcessMode + "申请成功，等待客服处理。",
                CreateDate = DateTime.Now
            };
            logisticsRepository.Install(log);

            return Feedback.Ok("退货申请成功，我们会尽快处理，请在左边查看处理结果！");
        }

        public Feedback InstallLogistics(Logistics log, int orderId)
        {
            var result = logisticsRepository.Install(log);
            if (result < 1)
                return Feedback.Failure("抱歉，发货失败，请重试");

            var order = orderRepository.GetOrder(orderId);

            string uid = "ldony";
            string pwd = "eternal178";
            string mob = order.Mobile;
            string msg = string.Format("【优品购】亲，您的订单已发货。{0}：{1}，请注意签收。如需帮助请联系客服。", log.Company, log.CourierNumber);

            string backinfo = SMS.PostData("http://service.winic.org:8009/sys_port/gateway/index.asp?", "id=" + uid + "&pwd=" + pwd + "&to=" + mob + "&content=" + msg + "&time=");


            orderRepository.UpdateStatus(orderId, 1, DateTime.Now);
            return Feedback.Ok("发货成功。");
        }

        public List<Logistics> GetLogistics(string moblie)
        {
            return logisticsRepository.GetLogistics(moblie);
        }

        public Feedback ModifyRefund(int refundId, int type)
        {
            var refund = refundRepository.Get(refundId);
            var order = orderRepository.GetOrderByOrderNo(refund.OrderNo);
            if (type == 1)
            {
                if (refund.ProcessMode == "退货")
                {
                    orderRepository.UpdateStatus(order.Id, 2, DateTime.Now);
                    //添加物流信息
                    var logs = new Logistics()
                    {
                        OrderNo = order.OrderNo,
                        Message = string.Format("退货申请通过，如有问题请联系客服处理。"),
                        CreateDate = DateTime.Now
                    };
                    logisticsRepository.Install(logs);
                }
                else if (refund.ProcessMode == "换货")
                {
                    orderRepository.UpdateStatus(order.Id, 3, DateTime.Now);
                    //添加物流信息
                    var logs = new Logistics()
                    {
                        OrderNo = order.OrderNo,
                        Message = string.Format("换过申请通过，如有问题请联系客服处理。"),
                        CreateDate = DateTime.Now
                    };
                    logisticsRepository.Install(logs);
                }

                refundRepository.UpdateStatus(refundId);//改为已处理
                return Feedback.Ok("操作成功，已同意申请。");
            }

            //不同意
            var log = new Logistics()
            {
                OrderNo = order.OrderNo,
                Message = string.Format("申请不通过，如有问题请联系客服处理。"),
                CreateDate = DateTime.Now
            };
            logisticsRepository.Install(log);
            refundRepository.UpdateStatus(refundId);//改为已处理
            return Feedback.Ok("操作成功，不同意申请。");
        }

        public Feedback OrderInfo()
        {
            var info = new List<string>();
            var orderList = orderRepository.GetOrderInfo();

            if (orderList.Count <= 0)
            {
                info.Add("张**（130****3260）在1分钟前订购了 劳力士-玫瑰金满天星镶钻 女款");
                info.Add("李**（136****7163）在3分钟前订购了 劳力士-玫瑰金满天星镶钻 男款");
                info.Add("周**（151****2588）在4分钟前订购了 劳力士-玫瑰金满天星镶钻 男款");
                info.Add("赵**（139****1955）在7分钟前订购了 劳力士-玫瑰金满天星镶钻 男款");
                info.Add("刘**（134****6489）在9分钟前订购了 劳力士-玫瑰金满天星镶钻 男款");
                info.Add("王**（133****4096）在10分钟前订购了 劳力士-玫瑰金满天星镶钻 女款");
                info.Add("秦**（139****4975）在15分钟前订购了 劳力士-玫瑰金满天星镶钻 男款");
                info.Add("朱**（180****6999）在20分钟前订购了 劳力士-玫瑰金满天星镶钻 男款");
                return Feedback.Ok("查询成功。", 0, info);
            }

            foreach (ProOrder item in orderList)
            {
                var name = item.UserName;
                var firstName = name.Substring(0, 1);
                if (name.Length < 3)
                    name = string.Format("{0}*", firstName);
                else
                    name = string.Format("{0}**", firstName);

                var mobile = item.Mobile;
                var fm = item.Mobile.Substring(0, 3);
                var lm = item.Mobile.Substring(7, 4);
                mobile = string.Format("（{0}****{1}）", fm, lm);

                TimeSpan time = DateTime.Now - item.CreateDate;
                int day = time.Days;
                int minute = time.Minutes;

                var msg = string.Empty;
                if (day > 0)
                    msg = string.Format("{0}{1}在{2}天{3}分钟前订购了 {4}", name, mobile, day, minute, item.ProName);
                else
                    msg = string.Format("{0}{1}在{2}分钟前订购了 {3}", name, mobile, minute, item.ProName);

                info.Add(msg);
            }

            if (orderList.Count < 10)
            {
                info.Add("张**（130****3260）在1分钟前订购了 劳力士-玫瑰金满天星镶钻 女款");
                info.Add("李**（136****7163）在3分钟前订购了 劳力士-玫瑰金满天星镶钻 男款");
                info.Add("周**（151****2588）在4分钟前订购了 劳力士-玫瑰金满天星镶钻 男款");
                info.Add("赵**（139****1955）在7分钟前订购了 劳力士-玫瑰金满天星镶钻 男款");
                info.Add("刘**（134****6489）在9分钟前订购了 劳力士-玫瑰金满天星镶钻 男款");
                info.Add("王**（133****4096）在10分钟前订购了 劳力士-玫瑰金满天星镶钻 女款");
                info.Add("秦**（139****4975）在15分钟前订购了 劳力士-玫瑰金满天星镶钻 男款");
                info.Add("朱**（180****6999）在20分钟前订购了 劳力士-玫瑰金满天星镶钻 男款");
            }

            return Feedback.Ok("查询成功。", 0, info);
        }
    }
}
