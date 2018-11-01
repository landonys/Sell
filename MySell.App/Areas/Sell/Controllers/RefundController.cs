using MySell.App.Areas.Sell.Filter;
using MySell.App.Web;
using MySell.Dal;
using MySell.Service.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySell.App.Areas.Sell.Controllers
{
    [LoginSafeFilter]
    public class RefundController : Controller
    {
        public IOrderService orderService = null;
        public RefundController()
        {
            orderService = new OrderService();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(int pageIndex, int pageSize)
        {
            var paged = new Paged(pageIndex, pageSize);
            var result = orderService.SearchRefund(paged);

            return this.DataTableSource(paged, result);
        }

        [HttpPost]
        public ActionResult ModifyRefund(int id, int type)
        {
            var fkb = orderService.ModifyRefund(id, type);

            return Json(fkb);
        }
    }
}
