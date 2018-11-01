using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySell.App.Models
{
    public class MainProductModel
    {
        public int Id { get; set; }
        public string PicSrc { get; set; }
        public string Price { get; set; }
        public string DPrice { get; set; }
    }
}