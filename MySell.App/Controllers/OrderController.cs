using MySell.App.Models;
using MySell.Dal;
using MySell.Dal.Model;
using MySell.Service.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySell.App.Controllers
{
    public class OrderController : Controller
    {
        private IOrderService orderService = null;
        public OrderController()
        {
            orderService = new OrderService();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Install(ProOrder order)
        {
            Feedback fkb = orderService.Install(order);

            return Json(fkb);
        }

        [HttpPost]
        public ActionResult InstallRefund(Refund refund)
        {
            Feedback fkb = orderService.InstallRefund(refund);

            return Json(fkb);
        }

        [HttpPost]
        public ActionResult GetLogistics(string moblie)
        {
            var logModel = orderService.GetLogistics(moblie);
            if (logModel.Count < 1)
                return Json(Feedback.Failure("信息不存在！"));

            var logList = new List<LogisticsModel>();
            foreach (var item in logModel)
            {
                var message = string.Empty;
                if (!string.IsNullOrWhiteSpace(item.Company))
                    message = string.Format("{0} ", item.Company);

                if (!string.IsNullOrWhiteSpace(item.CourierNumber))
                    message += string.Format("运单号{0}, ", item.CourierNumber);

                if (!string.IsNullOrWhiteSpace(item.Message))
                    message += item.Message;

                var logmodel = new LogisticsModel()
                {
                    Date = item.CreateDate.ToString("yyyy-MM-dd HH:mm"),
                    Message = message
                };

                logList.Add(logmodel);
            }

            var fkb = Feedback.Ok("查询成功", 0, logList);

            var view = new ContentResult
            {
                ContentType = "application/json",
                Content = Newtonsoft.Json.JsonConvert.SerializeObject(fkb)
            };

            return view;
        }
    }
}
