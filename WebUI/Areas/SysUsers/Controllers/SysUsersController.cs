using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonHelper;
using Dapper;
using WebUI.Models;
namespace WebUI.Areas.SysUsers.Controllers
{
    public class SysUsersController : Controller
    {
        //
        // GET: /SysUsers/SysUsers/DetailUsers
        string conStr = ConfigurationManager.ConnectionStrings["DBConn"].ToString();
        DBHelpers dbh = new DBHelpers();
        Users m = new Users();
        int row = -1;
        #region Index
        public ActionResult Index()
        {
            ViewBag.ClassInfo = dbh.GetSelectItemList("SELECT Types Texts, Types  Value FROM Sys_Users", conStr);
            return View();
        }

        public JsonResult _Index(Users param, int page, int limit)
        {

            List<Users> list = new List<Users>();
            //list.Add(new Users() { ID = 1, UsersName = "admin", Password = "123456", UsersNo = "0001", Sex = "男", Birthday = "1999-01-02", ClassInfo = "管理员", Tel = "13145677654", Address = "中国", Img = "/lib/Hui/img/a5.jpg", States = 1 });
            //list.Add(new Users() { ID = 2, UsersName = "张三", Password = "123456", UsersNo = "0002", Sex = "女", Birthday = "1999-04-02", ClassInfo = "老师", Tel = "13145677654", Address = "中国", Img = "/lib/Hui/img/a1.jpg", States = 1 });
            //list.Add(new Users() { ID = 3, UsersName = "李四", Password = "123456", UsersNo = "0003", Sex = "女", Birthday = "1999-05-02", ClassInfo = "学生", Tel = "13145677654", Address = "中国", Img = "/lib/Hui/img/a3.jpg", States = 1 });
            //list.Add(new Users() { ID = 4, UsersName = "王五", Password = "123456", UsersNo = "0004", Sex = "男", Birthday = "1999-06-02", ClassInfo = "学生", Tel = "13145677654", Address = "中国", Img = "/lib/Hui/img/a4.jpg", States = 1 });
            string where = " where 1=1 ";
            #region 查询条件
            if (!string.IsNullOrEmpty(param.UsersName))
            {
                where += " and UsersName='" + Request.Params["UsersName"] + "'";
            }
            if (!string.IsNullOrEmpty(param.UsersNo))
            {
                where += " and UsersNo='" + Request.Params["UsersNo"] + "'";
            }
            if (!string.IsNullOrEmpty(param.Types))
            {
                where += " and Types='" + Request.Params["Types"] + "'";
            }
            #endregion
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string sql = @"SELECT *  from  [dbo].[Sys_Users]    " + where + " Order by id desc";
                list = con.Query<Users>(sql).ToList();
            }
            int count = list.Count();
            if (count > 0)
            {
                list = list.Skip(Math.Min(count - 1, (page - 1) * limit)).Take(limit).ToList();
            }
            return Json(new
            {
                code = 0,
                msg = string.Empty,
                count = count,
                data = list
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Edit
        [HttpGet]
        public ActionResult EditUsers(int ID)
        {
            return View(dbh.ExecuteSQLT<Users>("SELECT *  from  [dbo].[Sys_Users]   where id=" + ID, conStr, m));
        }

        [HttpPost]
        public ActionResult EditUsers(Users model)
        {

            string update = @"UPDATE  [dbo].[Sys_Users] SET  UsersName=@UsersName, Password=@Password, UsersNo=@UsersNo, Types=@Types, Sex=@Sex, Birthday=@Birthday, ClassInfo=@ClassInfo, Tel=@Tel, 
                               Address=@Address, Img=@Img, States=@States, RZKM=@RZKM, ZC=@ZC, XL=@XL WHERE ID=@ID ";
            row = dbh.ExecuteSQL<Users>(update, conStr, model);
            if (row >= 0)
            {
                return this.Content("<script>parent.layer.msg('操作成功'); parent.layer.close(parent.layer.getFrameIndex(window.name));layer.iframeSrc(index, '/SysUsers/SysUsers/Index'); </script>");
            }
            else
            {
                return this.Content("<script>parent.layer.msg('操作失败,请您操作认真点哟！');</script>");
            }
        }
        #endregion

        #region Detail
        public ActionResult DetailUsers(int ID)
        {
            return View(dbh.ExecuteSQLT<Users>("SELECT *  from  [dbo].[Sys_Users]   where id=" + ID, conStr, m));
        }

        #endregion
    }
}
