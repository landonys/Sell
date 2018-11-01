using MySell.Dal;
using MySell.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySell.Service.Orders
{
    public interface IOrderService
    {
        /// <summary>
        /// 添加订单
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        Feedback Install(ProOrder order);
        /// <summary>
        /// 根据手机号获取订单
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        ProOrder GetOrderByMobile(string mobile);
        /// <summary>
        /// 根据订单号获取订单
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        ProOrder GetOrderByOrderNo(string orderNo);
        /// <summary>
        /// 获取全部订单
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        List<ProOrder> Search(string name, Paged page);
        /// <summary>
        /// 修改订单状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Feedback UpdateStatus(int id, int status);
        /// <summary>
        /// 根据id获取订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ProOrder GetOrder(int id);

        /// <summary>
        /// 获取退货信息
        /// </summary>
        /// <returns></returns>
        List<Refund> SearchRefund(Paged page);
        /// <summary>
        /// 退货申请
        /// </summary>
        /// <param name="refund"></param>
        /// <returns></returns>
        Feedback InstallRefund(Refund refund);
        /// <summary>
        /// 添加物料信息
        /// </summary>
        /// <param name="Log"></param>
        /// <returns></returns>
        Feedback InstallLogistics(Logistics Log, int orderId);
        /// <summary>
        /// 根据手机号获取物流信息
        /// </summary>
        /// <param name="moblie"></param>
        /// <returns></returns>
        List<Logistics> GetLogistics(string moblie);
        /// <summary>
        /// 修改退换货信息
        /// </summary>
        /// <param name="refundId">退货Id</param>
        /// <param name="type">0不同意1同意</param>
        /// <returns></returns>
        Feedback ModifyRefund(int refundId, int type);
        /// <summary>
        /// 获取最近20条订单信息
        /// </summary>
        /// <returns></returns>
        Feedback OrderInfo();
    }
}
