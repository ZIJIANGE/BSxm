using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.App_Start
{
    /// <summary>
    /// Session 过期
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class SessionUserParameterAttribute : FilterAttribute, IAuthorizationFilter
    {

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var loginUser = filterContext.HttpContext.Session["user"];
            //When user has not login yet
            if (loginUser == null)
            {
                // var redirectUrl = "/Home/Login";
                var redirectUrl = "/Home/Login";
                if (!filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new RedirectResult(redirectUrl);
                }
                else
                {
                    filterContext.Result = new JsonResult
                    {
                        Data = new
                        {
                            Success = false,
                            Message = string.Empty,
                            Redirect = redirectUrl
                        }
                    };
                }
                return;
            }

        }


    }
}