using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySell.Dal.Model
{
    /// <summary>
    /// 订单信息
    /// </summary>
    public class ProOrder
    {
        public int Id { get; set; }
        /// <summary>
        /// ProductSpec.Id
        /// </summary>
        public int ProductId { get; set; }
        public string OrderNo { get; set; }
        public string ProName { get; set; }
        public int Qty { get; set; }
        public string UserName { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public decimal Amount { get; set; }
        public int PayType { get; set; }
        public string LeavMessage { get; set; }
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 0待处理1已发货2已退货
        /// </summary>
        public int Status { get; set; }
        public DateTime DeliverDate { get; set; }

        public static string GetOrderNo()
        {
            Random rd = new Random();
            return DateTime.Now.ToString("yyyyMMddHHmmss" + rd.Next(1, 100).ToString());
        }
    }
}
