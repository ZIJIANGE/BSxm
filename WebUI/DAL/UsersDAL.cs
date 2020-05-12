using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebUI.Models;
using Dapper;
using System.Configuration;
namespace WebUI.DAL
{
    public class UsersDAL
    {
        string conStr = ConfigurationManager.ConnectionStrings["DBConn"].ToString();
        public Users GetLogin(string userName, string Pwd)
        {
            Users model = new Users();
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string sql = "select * from Sys_Users where (UsersName='" + userName + "' or tel = '"+userName+"') and Password='" + Pwd + "'";
                model = con.QueryFirstOrDefault<Users>(sql);
            }
            return model;
        }

    }
}