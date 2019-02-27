using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quick.Common;
using Quick.Models.Common;

namespace QuickWeb.Controllers.Common
{
    /// <summary>
    /// 业务控制器
    /// </summary>
    public class BusinessController : BaseController
    {
        #region 通用用户验证码校验方法
        protected bool IsValidateCode(string code)
        {
            var vCode = TempData["valid_code"]?.ToString();
            if (vCode == null)
                return false;
            return code.ToLower().Equals(vCode.ToLower());
        }
        #endregion

        #region 默认图标定义

        /// <summary>
        /// 用户默认图标
        /// </summary>
        public string defaultAvatar { get; set; } = "/Content/admin/images/profile_small.jpg";

        #endregion

        #region 通用返回BootstrapTable数据集合
        /// <summary>
        /// 返回BootstrapTable数据集合
        /// </summary>
        /// <param name="rows">请求记录</param>
        /// <param name="get">是否允许GET请求</param>
        /// <param name="code">错误代码</param>
        /// <param name="total">记录总数</param>
        /// <returns></returns>
        protected static JsonResult Table(int code, int total, object rows, bool get = true)
        {
            var js = new JsonResult
            {
                Data = new BsTableInfo(total, rows, code)
            };
            if (get)
                js.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return js;
        }

        /// <summary>
        /// 返回BootstrapTable数据集合
        /// </summary>
        /// <param name="rows">请求记录</param>
        /// <param name="get">是否允许GET请求</param>
        /// <param name="total">记录总数</param>
        /// <returns></returns>
        protected static JsonResult Table(int total, object rows, bool get = true)
        {
            var js = new JsonResult
            {
                Data = new BsTableInfo(total, rows, 1)
            };
            if (get)
                js.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return js;
        }

        /// <summary>
        /// 返回BootstrapTable数据集合
        /// </summary>
        /// <param name="get">是否允许GET请求</param>
        /// <returns></returns>
        protected static JsonResult Empty(bool get = true)
        {
            var js = new JsonResult
            {
                Data = new BsTableInfo(0, null, 0)
            };
            if (get)
                js.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return js;
        }
        #endregion
    }
}