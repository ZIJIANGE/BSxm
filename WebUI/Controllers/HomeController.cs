﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.App_Start;
using WebUI.DAL;
using WebUI.Models;
using Dapper;
using System.Data.SqlClient;
using System.Configuration;
using CommonHelper;
namespace WebUI.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/Index

        string conStr = ConfigurationManager.ConnectionStrings["DBConn"].ToString();
        UsersDAL ud = new UsersDAL();
        DBHelpers dbh = new DBHelpers();
        int row = -1;
        [SessionUserParameter]
        public ActionResult Index()
        {
            ViewBag.user = Session["user"];
            return View();
        }
        #region 登录
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login()
        {
            Session.Clear();
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult JsonLogin(string UserName, string UserPwd)
        {
            List<Users> list = new List<Users>();
            //list.Add(new Users() { ID = 1, UsersName = "admin", Password = "123456", UsersNo = "0001", Sex = "男", Birthday = "1999-01-02", ClassInfo = "管理员", Tel = "13145677654", Address = "中国", Img = "/lib/Hui/img/a5.jpg", States = 1 });
            //list.Add(new Users() { ID = 2, UsersName = "张三", Password = "123456", UsersNo = "0002", Sex = "女", Birthday = "1999-04-02", ClassInfo = "老师", Tel = "13145677654", Address = "中国", Img = "/lib/Hui/img/a1.jpg", States = 1 });
            //list.Add(new Users() { ID = 3, UsersName = "李四", Password = "123456", UsersNo = "0003", Sex = "女", Birthday = "1999-05-02", ClassInfo = "学生", Tel = "13145677654", Address = "中国", Img = "/lib/Hui/img/a3.jpg", States = 1 });
            //list.Add(new Users() { ID = 4, UsersName = "王五", Password = "123456", UsersNo = "0004", Sex = "男", Birthday = "1999-06-02", ClassInfo = "学生", Tel = "13145677654", Address = "中国", Img = "/lib/Hui/img/a4.jpg", States = 1 });
            //Users model = list.Where(a => a.UsersName == UserName && a.Password == UserPwd).FirstOrDefault();

            Users model = ud.GetLogin(UserName, UserPwd);
            string json = "";
            if (model != null)
            {
                Session["user"] = model;
                json = "1";

            }
            else
            {
                json = "0";
            }
            //更新登录时间
            return Json(json, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult LoginOut()
        {
            try
            {
                Session.Clear();
                return Json(new { validate = "true", message = "注销成功！" });
            }
            catch (Exception ex)
            {
                return Json(new { validate = "Exception", message = "注销异常：" + ex.Message });
            }
        }

        #endregion

        #region Register

        public ActionResult Register()
        {
            ViewBag.ClassInfo = dbh.GetSelectItemList("SELECT Types Texts, Types  Value FROM Sys_Users", conStr);
            ViewBag.ClassInfo = dbh.GetSelectItemList("SELECT Types Texts, Types  Value FROM Sys_Users", conStr);
            return View();
        }
        [HttpPost]
        public JsonResult Register(Users model)
        {
            if (!string.IsNullOrEmpty(model.Password))
            {
                model.Password = "123456";
            }
            string sql = @" IF not EXISTS (SELECT * FROM [BSXMDB].[dbo].[Sys_Users] WHERE  UsersNo='" + model.UsersNo
                + "') INSERT INTO [dbo].[Sys_Users]( UsersName, Password, UsersNo, Types, Sex, Birthday, ClassInfo, Tel, Address, Img, States, RZKM, ZC, XL )"
                + "   VALUES(@UsersName, @Password, @UsersNo, @Types, @Sex, @Birthday, @ClassInfo, @Tel, @Address, @Img, @States, @RZKM, @ZC, @XL)  ELSE SELECT -1; ";
            row = dbh.ExecuteSQL<Users>(sql, conStr, model);
            if (row >= 0)
            {
                if (ud.GetLogin(model.UsersName, model.Password) != null)
                {
                    Session["user"] = model;
                    row = 1;
                }
            }
            return Json(row, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 忘记密码
        [HttpGet]
        public ActionResult UpdatePwd()
        {
            return View();
        }
        [HttpPost]
        public JsonResult UpdatePwd(string Types, string Tel, string UsersNo, string Password)
        {
            string update = @"IF  EXISTS (SELECT * FROM [BSXMDB].[dbo].[Sys_Users] WHERE  UsersNo='" + UsersNo + "' AND tel='"
                            + Tel + "' and Types='" + Types + "')    UPDATE [BSXMDB].[dbo].[Sys_Users] SET [Password]='"
                            + Password + "' WHERE  UsersNo='" + UsersNo + "' AND tel='" + Tel + "' and  Types='" + Types + "' ELSE  SELECT -1;";
            if (dbh.ExecuteSQL(update, conStr) >= 0)
            {
                Session["user"] = ud.GetLogin(UsersNo, Password);
                row = 1;
            }
            return Json(row, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
