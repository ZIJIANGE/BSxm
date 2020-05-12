using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class Users
    {

        /// <summary>
        ///  教务处、教师、学生、家长
        /// </summary>
        public string Types { get; set; }

        #region 基本信息 

        public int ID { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string UsersName { get; set; }
        /// <summary>
        /// 学号
        /// </summary>
        public string UsersNo { get; set; }
        public string Email { get; set; }
        
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public string Birthday { get; set; }
        /// <summary>
        /// 所属班级
        /// </summary>
        public string ClassInfo { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 住址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Img { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 状态 1启用  2未启用
        /// </summary>
        public int States { get; set; }
        
        #endregion


        #region 教师职工

        /// <summary>
        /// 任职科目
        /// </summary>
        public string RZKM { get; set; }

        /// <summary>
        /// 任称
        /// </summary>
        public string ZC { get; set; }

        /// <summary>
        /// 学历
        /// </summary>
        public string XL { get; set; }

        #endregion

    }
}