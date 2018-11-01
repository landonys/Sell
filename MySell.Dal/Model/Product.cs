using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySell.Dal.Model
{
    /// <summary>
    /// 产品信息
    /// </summary>
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal DPrice { get; set; }
        public string PicSrc { get; set; }
        public DateTime ActivityDate { get; set; }
        public int Sold { get; set; }
        public string Describe1 { get; set; }
        public string Describe2 { get; set; }
        public string Describe3 { get; set; }
        public string BannerPic { get; set; }
        public string DescribePic { get; set; }
        public string ProcessPic { get; set; }
        public string SynopsisPic { get; set; }
        public string ProductSpec { get; set; }
        public int Sort { get; set; }
        /// <summary>
        /// 0不显示 1显示
        /// </summary>
        public Boolean IsShow { get; set; }
    }
}
