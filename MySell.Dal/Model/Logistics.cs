using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySell.Dal.Model
{
    /// <summary>
    /// 物流信息
    /// </summary>
    public class Logistics
    {
        public int Id { get; set; }
        public string OrderNo { get; set; }
        public string Company { get; set; }
        public string CourierNumber { get; set; }
        public string Message { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
