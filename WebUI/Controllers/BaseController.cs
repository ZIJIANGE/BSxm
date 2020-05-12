using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using System.Data.SqlClient;
using WebUI.Models;
 
namespace WebUI.Controllers
{
    public class BaseController : Controller
    {
        protected Users MySession { get; set; }
        public BaseController()
        {
            MySession = GetUserSession();
        }
        public Users GetUserSession()
        {
            object user = System.Web.HttpContext.Current.Session["user"];

            if (null != user)
            {
                Users sm = user as Users;
             
                return sm;
            }
            return new Users();
        }
    }
}
