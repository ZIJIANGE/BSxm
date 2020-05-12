using System.Web.Mvc;

namespace WebUI.Areas.Daqwrs
{
    public class DaqwrsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Daqwrs";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Daqwrs_default",
                "Daqwrs/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
