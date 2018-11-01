using MySell.App.Areas.Sell.Filter;
using MySell.App.Web;
using MySell.Dal;
using MySell.Dal.Model;
using MySell.Service.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySell.App.Areas.Sell.Controllers
{
    [LoginSafeFilter]
    public class OrdersController : Controller
    {
        public IOrderService orderService = null;
        public OrdersController()
        {
            orderService = new OrderService();
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add(int orderId)
        {
            ViewBag.OrderId = orderId;
            return View();
        }

        [HttpPost]
        public ActionResult Search(string name, int pageIndex, int pageSize)
        {
            var paged = new Paged(pageIndex, pageSize);
            var result = orderService.Search(name, paged);

            return this.DataTableSource(paged, result);
        }

        [HttpPost]
        public ActionResult UpdateStatus(int id, int status = 1)
        {
            var fkb = orderService.UpdateStatus(id, status);
            return Json(fkb);
        }

        [HttpPost]
        public ActionResult InstallLogistics(Logistics logistics)
        {
            var order = orderService.GetOrder(logistics.Id);
            var log = new Logistics()
            {
                OrderNo = order.OrderNo,
                Company = logistics.Company,
                CourierNumber = logistics.CourierNumber,
                Message = logistics.Message,
                CreateDate = DateTime.Now
            };
            var fkb = orderService.InstallLogistics(log, logistics.Id);

            return Json(fkb);
        }
    }
}
