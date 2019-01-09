using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dgssj.WeiXin.Web.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "用户名")]
        public string UserName { set; get; }

        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "密码")]
        [DataType(DataType.Password)]
        public string Password { set; get; }

        [Display(Name = "记住我")]
        public bool RememberMe { set; get; }

        [Display(Name = "验证码")]
        public string ValidateCode { set; get; }
    }
}
