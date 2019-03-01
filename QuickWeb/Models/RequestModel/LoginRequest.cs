using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuickWeb.Models.RequestModel
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "用户名不能为空！")]
        public string user_name { get; set; }

        [Required(ErrorMessage = "登陆密码不能为空！")]
        public string password { get; set; }

        [Required(ErrorMessage = "非法请求！校验码不能为空！")]
        public string code { get; set; }
    }
}