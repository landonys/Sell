using MySell.App.Models;
using MySell.Service.Products;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySell.App.Controllers
{
    public class HomeController : Controller
    {
        private IProductShowService productShowService = null;
        public HomeController()
        {
            productShowService = new ProductShowService();
        }
        //
        // GET: /Home/

        public ActionResult Index()
        {
            var imageRoot = ConfigurationManager.AppSettings["ImageRoot"];
            var modelList = new List<MainProductModel>();
            var proPicList = productShowService.GetProPic();
            foreach (var item in proPicList)
            {
                var model = new MainProductModel
                {
                    Id = item.Id,
                    PicSrc = string.Format("{0}{1}", imageRoot, item.PicSrc),// "/Image/" + item.PicSrc
                    Price = item.Price.ToString("0.##"),
                    DPrice = item.DPrice.ToString("0.##")
                };
                modelList.Add(model);
            }

            return View(modelList);
        }

    }
}
