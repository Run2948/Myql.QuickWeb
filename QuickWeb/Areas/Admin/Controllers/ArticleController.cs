using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Masuit.Tools.Logging;
using Quick.IService;
using Quick.Models.Entity.Table;
using Quick.Service;
using QuickWeb.Areas.Admin.Models.RequestModel;
using QuickWeb.Controllers.Common;

namespace QuickWeb.Areas.Admin.Controllers
{
    public class ArticleController : UserBaseController
    {
        public Isnake_articlesService snake_articlesService { get; set; }

        #region 文章列表
        // GET: Admin/Article
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetData(AdminBaseRequest request)
        {
            Expression<Func<snake_articles, bool>> where = l => true;
            if (!string.IsNullOrEmpty(request.searchText))
                where = l => l.title.Contains(request.searchText);
            var data = snake_articlesService.LoadPageEntities<snake_articles>(request.PageIndex, request.PageSize, ref request.TotalCount, where, l => l.id, false).ToList();
            return Table(request.TotalCount, data);
        }
        #endregion

        #region 添加文章
        public ActionResult ArticleAdd()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult AddArticle(snake_articles model)
        {
            try
            {
                model.add_time = DateTime.Now;
                snake_articlesService.AddEntity(model);
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }
            return Ok();
        }
        #endregion

        #region 编辑文章
        public ActionResult ArticleEdit(int? id)
        {
            if (IsIllegalId(id)) return ParamsError();
            var model = snake_articlesService.GetById(id);
            if (model == null) return NoOrDeleted();
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditArticle(snake_articles model)
        {
            if (IsIllegalId(model.id)) return ParamsError();
            try
            {
                var editModel = snake_articlesService.GetById(model.id);
                if (editModel == null) return NoOrDeleted();
                editModel.title = model.title;
                editModel.description = model.description;
                editModel.keywords = model.keywords;
                editModel.thumbnail = model.thumbnail;
                editModel.content = model.content;
                snake_articlesService.UpdateEntity(editModel);
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }
            return Ok();
        }
        #endregion

        #region 删除文章
        public ActionResult ArticleDel(int? id)
        {
            if (IsIllegalId(id)) return ParamsError();
            try
            {
                var model = snake_articlesService.GetById(id);
                if (model == null) return NoOrDeleted();
                if (!string.IsNullOrEmpty(model.thumbnail))
                    DeleteFile(Server.MapPath(model.thumbnail));
                snake_articlesService.DeleteEntity(model);
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }
            return Ok();
        }
        #endregion
    }
}