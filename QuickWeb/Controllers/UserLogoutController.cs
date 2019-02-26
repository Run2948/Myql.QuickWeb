using Masuit.Tools.Logging;
using QuickWeb.Controllers.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuickWeb.Filters;

namespace QuickWeb.Controllers
{
    public class UserLogoutController : UserBaseController
    {
        // GET: UserLogout
        [Route("UserLogout")]
        [CustomAllowedAttribute]
        public ActionResult Index()
        {
            try
            {
                SetUserLogOut();
                if (Request.HttpMethod.ToLower().Equals("get"))
                    return RedirectToAction("Index","Home",new { area = ""});
                return Ok("注销成功！", Url.Action("Index", "Home",new { area = ""}));
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                if (Request.HttpMethod.ToLower().Equals("get"))
                    return RedirectToAction("Index", "Home",new { area = ""});
                return No("注销失败！" + e.Message);
            }
        }
    }
}