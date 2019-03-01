using Masuit.Tools.Logging;
using Quick.Common.Extension;
using QuickWeb.Controllers.Common;
using QuickWeb.Models.RequestModel;
using System;
using System.Web.Mvc;
using Quick.IService;
using Quick.Common.Security;
using Quick.Common;

namespace QuickWeb.Controllers
{
    public class UserLoginController : UserBaseController
    {
        public Isnake_userService snake_userService { get;set; }	

        // GET: UserLogin
        [HttpGet]
        [Route("UserLogin")]
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    request.Password = request.Password.ToSaltMD5(JsonConfig.GetString("salt"));
                    var userInfo = snake_userService.Login(request.Username,request.Password);
                    if (userInfo != null)
                    {
                        SetUserSession(userInfo, 60 * 12);// 12个小时
                        return No("非法账户！");
                    }
                    else
                        return No("用户名或密码错误！");
                }
                else
                {
                    return No(ModelState.GetErrMsg());
                }
            }
            catch (Exception e)
            {
                LogManager.Error(nameof(UserLoginController), e);
                return No(e.Message);
            }
        }
    }
}