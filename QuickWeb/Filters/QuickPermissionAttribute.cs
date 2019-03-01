using Quick.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quick.Common.Net;
using Quick.Models.Dto;

namespace QuickWeb.Filters
{
    #region CustomAllowedAttribute
    //
    // 摘要:
    //     表示一个特性，该特性用于标记在授权期间要跳过 System.Web.Mvc.AuthorizeAttribute 的控制器和操作。
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class CustomAllowedAttribute : Attribute { } 
    
    #endregion


    public sealed class QuickPermissionAttribute: AuthorizeAttribute
    {
        public string ControllerName = string.Empty;

        public string ActionName = string.Empty;

        public string AreaName = string.Empty;

        public bool IsAjax = false;

        public string MethodType = string.Empty;

        public bool IsDebug = ConfigurationManager.AppSettings["IsDebug"] != null && bool.Parse(ConfigurationManager.AppSettings["IsDebug"]);

        #region Session相关

        public UserInfoOutputDto GetUserSession()
        {
            return HttpContext.Current.Session.Get<UserInfoOutputDto>(QuickKeys.UserSession);
        }

        #endregion

        /// <summary>
        /// 授权对象
        /// </summary>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            IsAjax = filterContext.HttpContext.Request.IsAjaxRequest();
            AreaName = filterContext.RouteData.DataTokens["area"]?.ToString();
            ControllerName = filterContext.RouteData.Values["controller"]?.ToString();
            ActionName = filterContext.RouteData.Values["action"]?.ToString();
            MethodType = filterContext.HttpContext.Request.HttpMethod.ToUpper();

            // 调试模式
            if (IsDebug)
                return;
            
            var currentUser = GetUserSession();
            // 拦截 未登录用户
            if (currentUser == null)
            {
                // 放过 不需要登陆的页面 (找回密码页面等等...)
                if (filterContext.ActionDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Length > 0)
                {
                    filterContext.HttpContext.SkipAuthorization = true;
                    return;
                }

                filterContext.Result = new RedirectResult(QuickKeys.UserLogin);
                return;
            }

            // 控制用户的权限
            if("GET".Equals(MethodType) && !currentUser.rights.Contains($"{ControllerName.ToLower()}/{ActionName.ToLower()}"))
            {
                // 放过 登录用户公共操作权限(注销登录首页欢迎页等等...)
                if (filterContext.ActionDescriptor.GetCustomAttributes(typeof(CustomAllowedAttribute), true).Length > 0)
                {
                    filterContext.HttpContext.SkipAuthorization = true;
                    return;
                }

                filterContext.Result = new RedirectResult(QuickKeys.NoPermission);
                return;
            }

            base.OnAuthorization(filterContext);
        }

        /// <summary>
        /// 授权逻辑
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return true;
        }

        /// <summary>
        /// 无权操作
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
        }

    }
}