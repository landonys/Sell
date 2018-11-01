using System.Web.Mvc;

namespace MySell.App.Areas.Sell
{
    public class SellAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Sell";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Sell_default",
                "Sell/{controller}/{action}/{id}",
                new { controller = "Master", action = "Login", id = UrlParameter.Optional }
            );
        }
    }
}
