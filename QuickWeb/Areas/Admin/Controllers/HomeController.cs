using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Masuit.Tools.Core.Linq;
using Quick.IService;
using Quick.Models.Dto;
using Quick.Models.Entity.Table;
using QuickWeb.Areas.Admin.Models.ViewModel;
using QuickWeb.Controllers.Common;
using QuickWeb.Filters;

namespace QuickWeb.Areas.Admin.Controllers
{
    public class HomeController : UserBaseController
    {
        public Isnake_nodeService snake_nodeService { get; set; }

        // GET: Admin/Home
        public ActionResult Index()
        {
            UserInfoOutputDto user = GetUserSession();
            ViewData["menus"] = GetUserMenuByRule(user.rule);
            return View(user);
        }

        #region 欢迎页面

        [CustomAllowedAttribute]
        [HttpGet]
        public ActionResult Welcome()
        {
            return View();
        }

        [CustomAllowedAttribute]
        [HttpGet]
        public ActionResult GetToday()
        {
            return Ok(DataHelper.GetData());
        } 
        #endregion

        #region 获取权限数据
        /// <summary>
        /// 根据用户权限加载权限菜单
        /// </summary>
        /// <param name="rule"></param>
        /// <returns></returns>
        private List<snake_node> GetUserMenuByRule(string rule)
        {
            List<string> ruleList = rule.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            // 可展示的菜单类型：is_menu == 2 
            Expression<Func<snake_node, bool>> @where = l => l.is_menu == 2;
            if (!ruleList.Contains("*"))
                where = where.And(l => ruleList.Contains(l.id.ToString()));
            return snake_nodeService.LoadEntities(where).ToList();
        }

        #endregion
    }
}