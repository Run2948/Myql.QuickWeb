using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Masuit.Tools.Logging;
using Quick.Common;
using Quick.IService;
using Quick.Models.Entity.Table;
using Quick.Service;
using QuickWeb.Areas.Admin.Models.RequestModel;
using QuickWeb.Areas.Admin.Models.ViewModel;
using QuickWeb.Controllers.Common;
using QuickWeb.Filters;

namespace QuickWeb.Areas.Admin.Controllers
{
    public class RoleController : UserBaseController
    {
        public Isnake_roleService snake_roleService { get; set; }
        public Isnake_nodeService snake_nodeService { get; set; }

        #region 角色列表
        // GET: Admin/Role
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet,CustomAllowed]
        public ActionResult GetData(AdminBaseRequest request)
        {
            Expression<Func<snake_role, bool>> where = l => true;
            if (!string.IsNullOrEmpty(request.searchText))
                where = l => l.role_name.Contains(request.searchText);
            var list = snake_roleService.LoadPageEntities<snake_role>(request.PageIndex, request.PageSize, ref request.TotalCount, where, l => l.id, false);
            var data = list.Mapper<List<RoleInfoView>>();
            return Table(request.TotalCount, data);
        }
        #endregion

        #region 添加角色
        [HttpGet]
        public ActionResult RoleAdd()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRole(snake_role model)
        {
            try
            {
                snake_roleService.AddEntity(model);
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }
            return Ok();
        }
        #endregion

        #region 编辑角色
        [HttpGet]
        public ActionResult RoleEdit(int? id)
        {
            if (IsIllegalId(id)) return ParamsError();
            var model = snake_roleService.GetById(id);
            if (model == null) return NoOrDeleted();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRole(snake_role model)
        {
            if (IsIllegalId(model.id)) return ParamsError();
            try
            {
                var editModel = snake_roleService.GetById(model.id);
                editModel.role_name = model.role_name;
                snake_roleService.UpdateEntity(editModel);
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }
            return Ok();
        }
        #endregion

        #region 删除角色
        public ActionResult RoleDel(int? id)
        {
            if (IsIllegalId(id)) return ParamsError();
            try
            {
                snake_roleService.DeleteById(id);
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }

            return Ok();
        }
        #endregion

        #region 为角色分配权限
        [HttpGet]
        public ActionResult GiveAccess(int? id)
        {
            if (IsIllegalId(id)) return ParamsError();
            var nodes = GetAllNodes();
            var ret = GetUserRuleIds(id);
            if (ret.Item1 < 1) return Ok(nodes);
            if (ret.Item1 > 1)
            {
                //nodes.ForEach(item => item.@checked = 1);
                foreach (var n in nodes)
                {
                    n.@checked = 1;
                }
            }
            else
            {
                foreach (var n in nodes)
                {
                    if (ret.Item2.Contains(n.id))
                        n.@checked = 1;
                }
            }
            return Ok(nodes);
        }

        [HttpPost]
        [CustomAllowed]
        public ActionResult SetAccess(int? id, string rule)
        {
            if (IsIllegalId(id)) return ParamsError();
            var model = snake_roleService.GetById(id);
            if (model == null) return NoOrDeleted();
            try
            {
                if (!string.IsNullOrEmpty(rule)) model.rule = rule;
                snake_roleService.UpdateEntity(model);
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }
            return Ok();
        }
        #endregion

        #region 获取所有权限信息
        /// <summary>
        /// 获取所有权限信息
        /// </summary>
        /// <returns></returns>
        private List<NodeInfoView> GetAllNodes()
        {
            var list = snake_nodeService.GetAll().ToList();
            var data = list.Mapper<List<NodeInfoView>>();
            return data;
        }

        /// <summary>
        /// 根据角色Id获取权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private (int, List<int>) GetUserRuleIds(int? id)
        {
            List<int> ids = new List<int>();
            if (id == null)
                return (0, ids);
            var rule = snake_roleService.GetById(id).rule;
            if (string.IsNullOrEmpty(rule))
                return (0, ids);
            if (rule.Contains("*"))
                return (2, ids);
            ids = rule.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x)).ToList();
            return (1, ids);
        }
        #endregion
    }
}