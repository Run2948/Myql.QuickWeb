using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quick.Common;
using Quick.Common.Net;
using Quick.Models.Entity.Table;
using Quick.Models.Dto;
using QuickWeb.Filters;

namespace QuickWeb.Controllers.Common
{
    [QuickPermission]
    public class UserBaseController : BusinessController
    {
        #region 用户Session相关操作

        protected bool IsUserLogin()
        {
            return GetUserSession() != null;
        }

        protected UserInfoOutputDto GetUserSession()
        {
            if (IsDebug)
            {
                UserInfoOutputDto dto = new UserInfoOutputDto() { id = 1, user_name = "admin", real_name = "admin", head = defaultAvatar, role_id = 1, role_name = "超级管理员", rule = "*" };
                System.Web.HttpContext.Current.Session.Set(QuickKeys.UserSession, dto, 60 * 12);
                return dto;
            }
            else
                return System.Web.HttpContext.Current.Session.Get<UserInfoOutputDto>(QuickKeys.UserSession);
        }

        protected void SetUserSession(UserInfoOutputDto dto, int timeout = 20)
        {
            System.Web.HttpContext.Current.Session.Set(QuickKeys.UserSession, dto, timeout);
        }

        protected void SetUserLogOut()
        {
            //System.Web.HttpContext.Current.Session[QuickKeys.UserSession] = null;
            System.Web.HttpContext.Current.Session.Remove(QuickKeys.UserSession);
            System.Web.HttpContext.Current.Session.Abandon();
        }

        #endregion

        #region 跳转自定义错误页面
        protected ActionResult Error() => RedirectToAction("Index", "Error");
        protected ActionResult ParamsError() => RedirectToAction("Index", "Error");
        protected ActionResult NoOrDeleted() => RedirectToAction("Index", "Error");
        #endregion
    }
}