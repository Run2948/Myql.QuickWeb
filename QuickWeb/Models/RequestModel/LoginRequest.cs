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
        public string Username { get; set; }

        [Required(ErrorMessage = "登陆密码不能为空！")]
        public string Password { get; set; }

        [Required(ErrorMessage = "非法请求！校验码不能为空！")]
        public string VerifyCode { get; set; }
    }
}