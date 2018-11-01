using MySell.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySell.App.Models
{
    public class MallProductModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }
        public string Discount { get; set; }
        public string DPrice { get; set; }
        public string SavePrice { get; set; }
        public int Sold { get; set; }
        public string Describe1 { get; set; }
        public string Describe2 { get; set; }
        public string Describe3 { get; set; }
        public List<string> BbannerPic { get; set; }
        public List<string> DescribePic { get; set; }
        public List<string> ProcessPic { get; set; }
        public List<string> SynopsisPic { get; set; }
        public ProductSpec FirstProductSpec { get; set; }
        public List<ProductSpec> ProductSpec { get; set; }
        public string Mobile { get; set; }
        public int Activity { get; set; }
    }
}