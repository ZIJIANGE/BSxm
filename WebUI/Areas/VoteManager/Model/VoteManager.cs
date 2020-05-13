using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebUI.Areas.VoteManager.Model
{
    public class VoteManager
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "问卷标题不能为空！")]
        [Display(Name ="问卷标题")]
        public string voteTitle { get; set; }
        [Display(Name = "选项1")]
        public string anwser1 { get; set; }
        [Display(Name = "选项2")]
        public string anwser2 { get; set; }
        [Display(Name = "选项3")]
        public string anwser3 { get; set; }
        [Display(Name = "选项4")]
        public string anwser4 { get; set; }
        [Display(Name = "选项5")]
        public string anwser5 { get; set; }
        [Display(Name = "选项6")]
        public string anwser6 { get; set; }
        [Required(ErrorMessage = "问卷结束时间不能为空！")]
        [Display(Name = "问卷结束时间")]
        public DateTime endTime { get; set; }
    }
}