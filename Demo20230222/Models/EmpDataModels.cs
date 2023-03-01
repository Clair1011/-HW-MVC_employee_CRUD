using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Demo20230222.Models
{
    public class EmpData
    {
        [Display(Name = "電腦編號")]
        [Required(ErrorMessage = "電腦編號必填")]
        public string EmpNo { get; set; }
        [Display(Name = "姓名")]
        [Required(ErrorMessage = "姓名必填")]
        public string Name { get; set; }
        [Display(Name = "分機")]
        [Required(ErrorMessage = "分機必填")]
        public string Ext { get; set; }
    }

    public class MessageInfo
    {
        public bool IsSuccess { get; set; }
        public string Msg { get; set; }
    }
}