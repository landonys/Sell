using MySell.App.Models;
using MySell.Dal.Model;
using MySell.Service.Orders;
using MySell.Service.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySell.App.Controllers
{
    public class MallController : Controller
    {
        private IProductShowService productShowService = null;
        private IOrderService orderService = null;
        public MallController()
        {
            productShowService = new ProductShowService();
            orderService = new OrderService();
        }

        public ActionResult Index(int id)
        {

            var mor = new List<string> { "s" };
            var product = productShowService.GetProduct(id);
            var proSpecList = productShowService.GetProductSpec(product.Id);
            var FirstProductSpec = proSpecList.FirstOrDefault();
            proSpecList.Remove(FirstProductSpec);

            TimeSpan activityDate = Config.Get().ActivityDate - DateTime.Now;
            int activity = activityDate.Days * 24 * 3600 + activityDate.Hours * 3600 + activityDate.Minutes * 60 + activityDate.Seconds;
            if (activity <= 0)
                activity = 100000;

            var model = new MallProductModel()
            {
                Id = product.Id,
                Title = product.Title,
                Price = product.Price.ToString("0.##"),
                Discount = product.Discount.ToString("0.##"),
                DPrice = product.DPrice.ToString("0.##"),
                SavePrice = (product.Price - product.DPrice).ToString("0.##"),
                Sold = product.Sold,
                Describe1 = product.Describe1,
                Describe2 = product.Describe2,
                Describe3 = product.Describe3,
                BbannerPic = string.IsNullOrWhiteSpace(product.BannerPic) ? mor : product.BannerPic.Split(',').ToList(),
                DescribePic = string.IsNullOrWhiteSpace(product.DescribePic) ? mor : product.DescribePic.Split(',').ToList(),
                ProcessPic = string.IsNullOrWhiteSpace(product.ProductSpec) ? mor : product.ProcessPic.Split(',').ToList(),
                SynopsisPic = string.IsNullOrWhiteSpace(product.SynopsisPic) ? mor : product.SynopsisPic.Split(',').ToList(),
                FirstProductSpec = FirstProductSpec,
                ProductSpec = proSpecList,
                Mobile = Config.Get().Mobile,
                Activity = activity
            };

            return View(model);
        }

        public ActionResult Success(string orderNo)
        {
            var order = orderService.GetOrderByOrderNo(orderNo);
            return View(order);
        }

        [HttpPost]
        public ActionResult OrderInfo()
        {
            var fkb = orderService.OrderInfo();
            return Json(fkb);
        }
    }
}
