using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Masuit.Tools.Logging;
using Quick.Common;
using Quick.Common.Security;
using Quick.IService;
using QuickWeb.Areas.Admin.Models.RequestModel;
using QuickWeb.Controllers.Common;

namespace QuickWeb.Areas.Admin.Controllers
{
    public class ProfileController : UserBaseController
    {
        public Isnake_userService snake_userService { get; set; }

        #region 修改信息

        // GET: Admin/Profile
        [HttpGet]
        public ActionResult Index()
        {
            var dto = GetUserSession();
            var model = snake_userService.GetById(dto.id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditInfo(AdminEditRequest request)
        {
            try
            {
                var dto = GetUserSession();
                var editModel = snake_userService.GetById(dto.id);
                if (!string.IsNullOrEmpty(request.head))
                    editModel.head = request.head;
                if (!string.IsNullOrEmpty(request.real_name))
                    editModel.head = request.real_name;
                snake_userService.UpdateEntity(editModel);
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }
            return Ok();
        }
        #endregion

        #region 修改密码
        [HttpGet]
        public ActionResult PwdEdit()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPwd(AdminPwdRequest request)
        {
            if (!request.re_new_password.Equals(request.new_password))
                return No("两次输入的密码不相同！");
            if (request.new_password.Equals(request.old_password))
                return No("新密码不能和旧密码相同！");
            try
            {
                var dto = GetUserSession();
                var editModel = snake_userService.GetById(dto.id);
                var saltPwd = request.old_password.ToSaltMD5(JsonConfig.GetString("salt"));
                if (!editModel.password.Equals(saltPwd))
                    return No("原始密码错误！");
                var saltNewPwd = request.new_password.ToSaltMD5(JsonConfig.GetString("salt"));
                editModel.password = saltNewPwd;
                snake_userService.UpdateEntity(editModel);
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }
            return Ok();
        }
        #endregion

        #region 辅助方法
        /// <summary>
        /// 上传图像
        /// </summary>
        /// <returns></returns>
        public ActionResult UploadHead()
        {
            var result = PreFileUpload("img", "images/avatars");
            if (!result.Item1)
                return Json(new { status = "error", message = result.Item2 }, JsonRequestBehavior.AllowGet);
            var image = Image.FromFile(Request.MapPath(result.Item2));
            var imgW = image.Width;
            var imgH = image.Height;
            image.Dispose();
            return Json(new
            {
                status = "success",
                url = result.Item2,
                width = imgW,
                height = imgH
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 裁剪头像
        /// </summary>
        /// <param name="imgUrl">原始图片的服务器地址</param>
        /// <param name="imgInitW">裁剪区域宽度 600</param>
        /// <param name="imgInitH">裁剪区域高度 340</param>
        /// <param name="imgW">图像保存宽度 640</param>
        /// <param name="imgH">图像保存高度 362.6...7</param>
        /// <param name="imgX1">裁剪区域x坐标 195</param>
        /// <param name="imgY1">裁剪区域y坐标 11</param>
        /// <param name="cropW">裁剪区域宽度 250</param>
        /// <param name="cropH">裁剪区域高度 340</param>
        /// <param name="rotation">旋转角度：0-360°</param>
        /// <returns></returns>
        public ActionResult CropHead(string imgUrl,double imgInitW,double imgInitH,double imgW,double imgH, double imgX1, int imgY1, double cropW, double cropH,double rotation)
        {
            if (string.IsNullOrEmpty(imgUrl))
                return Json(new
                {
                    status = "error",
                    message = "找不到图片！"
                }, JsonRequestBehavior.AllowGet);
            int x = (int)(imgX1 - (imgW - imgInitW) / 2);
            int y = (int)(imgY1 - (imgH - imgInitH) / 2);
            var result = PreCutAndSaveImage("avatars", imgUrl, x, y, (int)cropW, (int)cropH);
            return !result.Item1
                ? Json(new
                {
                    status = "error",
                    message = result.Item2
                }, JsonRequestBehavior.AllowGet)
                : Json(new
            {
                status = "success",
                url = result.Item2
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}