using MySell.Dal;
using MySell.Dal.Model;
using MySell.Service.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySell.App.Web;
using System.Text.RegularExpressions;
using MySell.App.Areas.Sell.Filter;

namespace MySell.App.Areas.Sell.Controllers
{
    [LoginSafeFilter]
    public class ProductController : Controller
    {
        public IProductService productService = null;
        public ProductController()
        {
            productService = new ProductService();
        }
        //
        // GET: /Sell/Product/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            var product = productService.GetProduct(id);
            return View(product);
        }

        [HttpPost]
        public ActionResult Add(Product product)
        {
            var fkb = productService.Install(product);
            return Json(fkb);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            var oldproduct = productService.GetProduct(product.Id);

            var fkb = productService.Update(product);
            return Json(fkb);
        }

        [HttpPost]
        public ActionResult Search(string name, int pageIndex = 1, int pageSize = 20)
        {
            var paged = new Paged(pageIndex, pageSize);
            var result = productService.Search(name, paged);

            return this.DataTableSource(paged, result);
        }

        [HttpPost]
        public ActionResult DeletePro(int id)
        {
            var fbk = productService.DeletePro(id);

            return Json(fbk);
        }

        [HttpPost]
        public ActionResult UpdateIsShow(int id)
        {
            var fbk = productService.UpdateIsShow(id);

            return Json(fbk);
        }

        [HttpPost]
        public ActionResult ResetPrice(int id)
        {
            var product = productService.GetProduct(id);
            var proSpecList = new List<ProductSpec>();
            var specArray = product.ProductSpec.Split(',');
            foreach (string spec in specArray)
            {
                var productSpec = new ProductSpec();
                foreach (var number in ExtractNumbersFromString(spec))
                {
                    productSpec.ProductId = product.Id;
                    productSpec.Name = spec;
                    productSpec.Price = number;
                }

                proSpecList.Add(productSpec);
            }
            var fkb = productService.ResetPrice(id, proSpecList);

            return Json(fkb);
        }


        private static IEnumerable<int> ExtractNumbersFromString(string s)
        {
            //Regex.Matches 方法：在输入字符串中搜索正则表达式的所有匹配项并返回所有匹配。
            //一次或多次匹配前面的字符或子表达式。等效于 {1,}。如果将+去掉，就是
            //return Regex.Matches(s, @"\d").Cast<Match>().Select(m => Convert.ToInt32(m.Value));
            return Regex.Matches(s, @"\d+").Cast<Match>().Select(m => Convert.ToInt32(m.Value));
        }
    }
}
