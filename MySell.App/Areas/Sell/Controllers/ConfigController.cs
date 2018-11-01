using MySell.App.Areas.Sell.Filter;
using MySell.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySell.App.Areas.Sell.Controllers
{
    [LoginSafeFilter]
    public class ConfigController : Controller
    {
        //
        // GET: /Sell/Config/

        public ActionResult Index()
        {
            var model = Config.Get();
            if (model == null)
            {
                model = new Config() { Mobile = "", ActivityDate = DateTime.Now };
            }

            return View(model);
        }

        public ActionResult Update(string mobile, DateTime activityDate)
        {
            var config = new Config()
            {
                Mobile = mobile,
                ActivityDate = activityDate
            };

            var fkb = new Config().Update(config);

            return Json(fkb);
        }
    }
}
