using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using Quick.Common.NoSQL;
using Masuit.Tools.Logging;
using Masuit.Tools;

namespace QuickWeb
{
    public class Global : HttpApplication
    {
        /// <summary>
        /// 是否启用Redis：如果启用了，请注意保证正确配置Redis连接
        /// </summary>
        protected bool EnableRedis = ConfigurationManager.AppSettings["EnableRedis"] != null && bool.Parse(ConfigurationManager.AppSettings["EnableRedis"]);

        void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            // 使用之前请根据需要配置Bundles文件
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutofacConfig.Register();
            StartupConfig.Startup();          
        }

                void Application_BeginRequest()
        {
            if (EnableRedis)
            {
                RedisHelper RedisHelper = RedisHelper.GetInstance();
#if !DEBUG          
            //if (Request.UserHostAddress != null && Request.UserHostAddress.IsDenyIpAddress())
            //{
            //    //Response.ContentEncoding = Encoding.GetEncoding("GB2312");
            //    Response.Write($"检测到您的IP（{Request.UserHostAddress}）异常，已被本站禁止访问，如有疑问，请联系站长！");
            //    Response.End();
            //    return;
            //}
#endif
                string httpMethod = Request.HttpMethod;
                if (httpMethod.Equals("OPTIONS", StringComparison.InvariantCultureIgnoreCase) || httpMethod.Equals("HEAD", StringComparison.InvariantCultureIgnoreCase))
                {
                    Response.End();
                    return;
                }

                bool isSpider = Request.UserAgent != null && Request.UserAgent.Contains(new[] { "DNSPod", "Baidu", "spider", "Python", "bot" });
                if (isSpider) return;
                try
                {
                    var times = RedisHelper.StringIncrement("Frequency:" + Request.UserHostAddress + ":" + Request.UserAgent);
                    RedisHelper.Expire("Frequency:" + Request.UserHostAddress + ":" + Request.UserAgent, TimeSpan.FromMinutes(1));
                    if (times > 200)
                    {
                        //Response.ContentEncoding = Encoding.GetEncoding("GB2312");
                        Response.Write($"检测到您的IP（{Request.UserHostAddress}）访问过于频繁，已被本站暂时禁止访问，如有疑问，请联系站长！");
                        Response.End();
                        return;
                    }
                }
                catch
                {
                    // ignored
                }
            }
        }

        void Application_Error(object sender, EventArgs e)
        {
            HttpException exception = ((HttpApplication)sender).Context.Error as HttpException;
            LogManager.Error(typeof(HttpApplication), exception ?? ((HttpApplication)sender).Server.GetLastError());
#if !DEBUG           
            int? errorCode = exception?.GetHttpCode() ?? 503;
            switch (errorCode)
            {
                case 401:
                    Response.Redirect("/AccessNoRight");
                    break;
                case 404:
                    Response.Redirect("/PageNotFound");
                    break;
                case 500:
                    Response.Redirect("/ServerError");
                    break;
                case 503:
                    Response.Redirect("/ServiceUnavailable");
                    break;
                default:
                    return;
            }
#endif
        }
    }
}