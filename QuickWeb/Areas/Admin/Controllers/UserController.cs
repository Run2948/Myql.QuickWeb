using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Masuit.Tools.Logging;
using Quick.Common;
using Quick.Common.Security;
using Quick.IService;
using Quick.Models.Entity.Table;
using QuickWeb.Areas.Admin.Models.RequestModel;
using QuickWeb.Areas.Admin.Models.ViewModel;
using QuickWeb.Controllers.Common;
using QuickWeb.Filters;

namespace QuickWeb.Areas.Admin.Controllers
{
    public class UserController : UserBaseController
    {
        public Isnake_userService snake_userService { get; set; }
        public Isnake_roleService snake_roleService { get; set; }

        #region 管理员列表
        // GET: Admin/User
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet,CustomAllowed]
        public ActionResult GetData(AdminBaseRequest request)
        {
            var list = snake_userService.GetPureUsers(request.PageIndex, request.PageSize, ref request.TotalCount, request.searchText);
            var data = list.Mapper<List<UserInfoView>>();
            return Table(request.TotalCount, data);
        }
        #endregion

        #region 添加管理员

        [HttpGet]
        public ActionResult UserAdd()
        {
            InitUserStatus();
            InitUserRole();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUser(snake_user model)
        {
            try
            {
                model.head = defaultAvatar;
                model.password = model.password.ToSaltMD5(JsonConfig.GetString("salt"));
                snake_userService.AddEntity(model);
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }

            return Ok();
        }
        #endregion

        #region 编辑管理员
        [HttpGet]
        public ActionResult UserEdit(int? id)
        {
            if (IsIllegalId(id)) return ParamsError();
            var model = snake_userService.GetById(id);
            if (model == null) return NoOrDeleted();
            InitUserStatus();
            InitUserRole();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(snake_user model)
        {
            if(IsIllegalId(model.id)) return ParamsError();
            try
            {
                var editModel = snake_userService.GetById(model.id);
                editModel.user_name = model.user_name;
                editModel.role_id = model.role_id;
                editModel.real_name = model.real_name;
                if(!string.IsNullOrEmpty(model.password))
                    editModel.password = model.password.ToSaltMD5(JsonConfig.GetString("salt"));
                editModel.status = model.status;
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

        #region 删除管理员
        public ActionResult UserDel(int ? id)
        {
            if (IsIllegalId(id)) return ParamsError();
            try
            {
                snake_userService.DeleteById(id);
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }

            return Ok();
        }
        #endregion

        #region 页面基础数据传递
        protected void InitUserStatus()
        {
            ViewData["user_status"] = JsonConfig.GetDict("user_status");
        }

        protected void InitUserRole()
        {
            ViewData["user_role"] = snake_roleService.LoadEntities(l => true).ToList();
        }
        #endregion
    }
}