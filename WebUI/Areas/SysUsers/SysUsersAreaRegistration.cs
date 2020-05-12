using System.Web.Mvc;

namespace WebUI.Areas.SysUsers
{
    public class SysUsersAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "SysUsers";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "SysUsers_default",
                "SysUsers/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
