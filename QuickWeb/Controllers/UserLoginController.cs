using Masuit.Tools.Logging;
using Quick.Common.Extension;
using QuickWeb.Controllers.Common;
using QuickWeb.Models.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Quick.IService;
using Quick.Common.Security;
using Quick.Common;

namespace QuickWeb.Controllers
{
    public class UserLoginController : UserBaseController
    {
        public Isnake_userService snake_userService { get; set; }
        public Isnake_nodeService snake_nodeService { get; set; }

        #region 用户登录
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
        public ActionResult Login(LoginRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(!IsValidateCode(request.code)) return No("验证码错误！");
                    request.password = request.password.ToSaltMD5(JsonConfig.GetString("salt"));
                    var userInfo = snake_userService.Login(request.user_name, request.password);
                    if (userInfo != null)
                    {
                        userInfo.rights = GetUserRightsByRule(userInfo.rule);
                        SetUserSession(userInfo, 60 * 12);// 12个小时
                        return Ok(Url.Action("Index","Home",new { area = "" }));
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
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }
        }
        #endregion

        #region 根据用户rule获取权限集合
        /// <summary>
        /// 根据用户rule获取权限集合
        /// </summary>
        /// <param name="rule"></param>
        /// <returns></returns>
        private List<string> GetUserRightsByRule(string rule)
        {
            List<string> rights = new List<string>();
            if (string.IsNullOrEmpty(rule))
                return rights;
            var ids = rule.Contains("*")
                ? snake_nodeService.LoadEntities(l=>l.type_id > 0).Select(x=>x.id).ToList()
                : rule.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x)).ToList();
            rights = snake_nodeService.LoadEntities(l=> ids.Contains(l.id)).ToList().Select(item => $"{item.control_name}/{item.action_name}").ToList();
            return rights;
        }
        #endregion
    }
}