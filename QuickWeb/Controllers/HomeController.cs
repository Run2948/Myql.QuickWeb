using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quick.IService;
using QuickWeb.Controllers.Common;

namespace QuickWeb.Controllers
{
    public class HomeController : BaseController
    {
        public Isnake_roleService snake_roleService { get;set; }	

        // GET: Home
        public ActionResult Index()
        {
//            var role = snake_roleService.AddEntity(new Quick.Models.Entity.Table.snake_role()
//            {
//                role_name = "测试，亲",
//                rule = "*"
//            });
            return Ok(snake_roleService.GetAll().ToList());
        }
    }
}