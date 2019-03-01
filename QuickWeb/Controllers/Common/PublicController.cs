using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Masuit.Tools.Logging;
using Quick.IService;
using QuickWeb.Controllers.Common;

namespace QuickWeb.Controllers
{
    public class PublicController : UserBaseController
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
    }
}