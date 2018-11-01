using MySell.App.Areas.Sell.Filter;
using MySell.Service.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySell.App.Areas.Sell.Controllers
{
    public class MasterController : Controller
    {
        private IUserService userService = null;
        public MasterController()
        {
            userService = new UserService();
        }

        //
        // GET: /Sell/Master/
        [LoginSafeFilter]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string name, string password)
        {
            var fkb = userService.Login(name, password);

            return Json(fkb);
        }

        [LoginSafeFilter]
        [HttpPost]
        public ActionResult OutLogin()
        {
            var fkb = userService.OutLogin();

            return RedirectToAction("Login", "Master");
        }
    }
}
