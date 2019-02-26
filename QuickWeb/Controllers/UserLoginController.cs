using Masuit.Tools.Logging;
using Quick.Common.Extension;
using Quick.Models.Entity.Table;
using QuickWeb.Controllers.Common;
using QuickWeb.Models.RequestModel;
using System;
using System.Web.Mvc;

namespace QuickWeb.Controllers
{
    public class UserLoginController : UserBaseController
    {
        //public IUserInfoService UserInfoService { get; set; }

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
                    var userInfo = new snake_user();//UserInfoService.GetFirstEntity(l => l.Username == request.Username && l.Password == request.Password);
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