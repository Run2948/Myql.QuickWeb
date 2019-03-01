using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Masuit.Tools.Logging;
using Quick.Common;
using Quick.IService;
using Quick.Models.Entity.Table;
using QuickWeb.Areas.Admin.Models.ViewModel;
using QuickWeb.Controllers.Common;
using QuickWeb.Filters;

namespace QuickWeb.Areas.Admin.Controllers
{
    public class NodeController : UserBaseController
    {
        public Isnake_nodeService snake_nodeService { get; set; }

        #region 节点列表
        // GET: Admin/Node
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet,CustomAllowed]
        public ActionResult GetData()
        {
            var roots = GetNodeTrees();
            foreach (var root in roots)
            {
                root.children = GetNodeTrees(root.id).ToArray();
                foreach (var child in root.children)
                {
                    child.children = GetNodeTrees(child.id).ToArray();
                }
            }
            return Ok(roots);
        }

        #endregion

        #region 添加节点
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NodeAdd(snake_node model)
        {
            try
            {
                snake_nodeService.AddEntity(model);
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }
            return Ok();
        }
        #endregion

        #region 编辑节点
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NodeEdit(snake_node model)
        {
            if (IsIllegalId(model.id)) return ParamsError();
            try
            {
                var editModel = snake_nodeService.GetById(model.id);
                if (editModel == null) return NoOrDeleted();
                editModel.node_name = model.node_name;
                editModel.control_name = model.control_name;
                editModel.action_name = model.action_name;
                editModel.is_menu = model.is_menu;
                editModel.style = model.style;
                snake_nodeService.UpdateEntity(editModel);
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }
            return Ok();
        }
        #endregion

        #region 删除节点
        public ActionResult NodeDel(int ? id)
        {
            if (IsIllegalId(id)) return ParamsError();
            try
            {
                var delModel = snake_nodeService.GetById(id);
                if (delModel == null) return NoOrDeleted();
                snake_nodeService.DeleteEntity(delModel);
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }
            return Ok();
        }
        #endregion

        #region 循环获取节点树信息
        private List<NodeTreeView> GetNodeTrees(int pid = 0)
        {
            var list = snake_nodeService.LoadEntities(l => l.type_id == pid).ToList();
            var data = list.Mapper<List<NodeTreeView>>();
            return data;
        }
        #endregion
    }
}