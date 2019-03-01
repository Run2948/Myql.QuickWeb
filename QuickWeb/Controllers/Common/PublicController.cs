using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Masuit.Tools.Logging;
using Quick.Common;
using Quick.IService;
using QuickWeb.Controllers.Common;

namespace QuickWeb.Controllers
{
    public class PublicController : BaseController
    {
        #region LayUI通用上传图片 
        /// <summary>
        /// LayUI通用上传图片
        /// </summary>
        /// <param name="fileInput">file表单参数名</param>
        /// <param name="folderName">上传文件夹</param>
        /// <returns></returns>
        public ActionResult Upload(string fileInput, string folderName)
        {
            try
            {
                var result = PreFileUpload(fileInput, folderName);
                return !result.Item1 ? No(result.Item2) : Ok(new { src = result.Item2 });
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }
        }
        #endregion

        #region 平台通用图形验证码
        [HttpGet]
        public ActionResult VerifyCode(int l = 4, int f = 15, int h = 30)
        {
            string code = DrawingSecurityCode.GenerateCheckCode(l);
            //Session.SetByRedis("member_valid_code", code, expire: 5);//将验证码生成到Session中
            TempData["valid_code"] = code;  // 将验证码存储到 TempData 中
            System.Web.HttpContext.Current.CreateCheckCodeImage(code, fontsize: f, height: h);
            Response.ContentType = "image/jpeg";
            return File(Encoding.UTF8.GetBytes(code), Response.ContentType);
        }
        #endregion
    }
}