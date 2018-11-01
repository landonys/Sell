using MySell.Dal;
using MySell.Service.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

/*
 * 登录安全检查
 *       
 * @Landony
 * 20181029 create.
**/

namespace MySell.App.Areas.Sell.Filter
{
    public class LoginSafeFilter : ActionFilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var user = UserService.CurrentBusiness;

            if (user != null)
                return;

            if (!filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new EmptyResult();
                filterContext.HttpContext.Response.Redirect("/Master/Login");
            }
            else
            {
                var fkb = Feedback.Failure("抱歉，您尚未登录或者登录超时！");
                var view = new JsonResult();

                view.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                view.Data = fkb;
                filterContext.Result = view;
            }
        }
    }
}