using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySell.Dal.Model
{
    /// <summary>
    /// 退货信息
    /// </summary>
    public class Refund
    {
        public int Id { get; set; }
        public string OrderNo { get; set; }
        public string Mobile { get; set; }
        public string ProcessMode { get; set; }
        public string Message { get; set; }
        public DateTime CreateDate { get; set; }
        public string Account { get; set; }
        /// <summary>
        /// 0待处理1已退货
        /// </summary>
        public int Status { get; set; }
    }
}
