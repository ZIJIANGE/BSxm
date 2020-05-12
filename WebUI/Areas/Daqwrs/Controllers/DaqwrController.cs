using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using System.Configuration;
using WebUI.Areas.Daqwrs.Models;
namespace WebUI.Areas.Daqwrs.Controllers
{
    public class DaqwrController : Controller
    {
        //
        // GET: /Daqwrs/Daqwr/Index

        string conStr = ConfigurationManager.ConnectionStrings["DBConn"].ToString();

        public ActionResult Index()
        {
            string rq = "";
            string AQI = "";
            string PM25 = "";
            string PM10 = "";
            string types = "";
            string nums = "";
            string SO2 = "";
            string CO = "";
            string NO2 = "";
            string O3 = "";
            string legend1 = "";
            string series1 = "";

            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                dt.Load(con.ExecuteReader("select * from [dbo].[Daqwr]   "));
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow it in dt.Rows)
                    {
                        rq += "'" + it["dates"] + "',";
                        AQI += it["AQI"] + ",";
                        PM25 += it["PM25"] + ",";
                        PM10 += it["PM10"] + ",";
                        types += it["types"] + ",";
                        nums += it["nums"] + ",";
                        SO2 += it["SO2"] + ",";
                        CO += it["CO"] + ",";
                        NO2 += it["NO2"] + ",";
                        O3 += it["O3"] + ",";
                    }
                }
                dt1.Load(con.ExecuteReader("SELECT types, COUNT(1) num FROM [Daqwr] GROUP BY types"));

                if (dt1.Rows.Count > 0)
                {
                    foreach (DataRow it in dt1.Rows)
                    {
                        legend1 += "'" + it["types"] + "',";
                        series1 += "{ \"value\": \"" + it["num"] + "\", \"name\": \"" + it["types"] + "\" },";
                    }
                }

            }
            ViewBag.RQ = rq.Substring(0, rq.Length - 1);
            ViewBag.AQI = AQI.Substring(0, AQI.Length - 1);
            ViewBag.PM25 = PM25.Substring(0, PM25.Length - 1);
            ViewBag.PM10 = PM10.Substring(0, PM10.Length - 1);
            ViewBag.types = types.Substring(0, types.Length - 1);
            ViewBag.nums = nums.Substring(0, nums.Length - 1);
            ViewBag.SO2 = SO2.Substring(0, SO2.Length - 1);
            ViewBag.CO = CO.Substring(0, CO.Length - 1);
            ViewBag.NO2 = NO2.Substring(0, NO2.Length - 1);
            ViewBag.O3 = O3.Substring(0, O3.Length - 1);

            ViewBag.legend1 = legend1.Substring(0, legend1.Length - 1);
            ViewBag.series1 = series1.Substring(0, series1.Length - 1);


            return View();
        }

        public ActionResult Index2()
        {
            return View();
        }
        public ActionResult Index3()
        {
            return View();
        }
        public JsonResult _Index(Daqwr param, int page, int limit)
        {
            List<Daqwr> list = new List<Daqwr>();
            string where = " where 1=1 ";
            #region 查询条件
            if (!string.IsNullOrEmpty(param.dates))
            {
                where += " and dates='" + Request.Params["dates"] + "'";
            }
            #endregion
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string sql = @"SELECT id, dates, AQI, nums, types, PM25, PM10, SO2, Convert(decimal(18,2),CO) CO, NO2, O3  from  SmartPlatformDB.dbo.Daqwr    " + where + " Order by id desc";
                list = con.Query<Daqwr>(sql).ToList();
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

    }
}
