using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quick.Common.NoSQL;
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
            // 测试数据库操作
            //TestDbOpt();
            // 测试Redis操作
            //TestRedisOpt();
            // 测试查询snake_role表单
            return Ok(snake_roleService.GetAll().ToList());
        }

        #region 测试方法

        private void TestDbOpt()
        {
            var role = snake_roleService.AddEntity(new Quick.Models.Entity.Table.snake_role()
            {
                role_name = "测试，亲",
                rule = "*"
            });
        }

        private void TestRedisOpt()
        {
            using (RedisHelper service = RedisHelper.GetSingleInstance())
            {
                service.SetString("test", "caonima", TimeSpan.FromMinutes(1));
            }
        }

        #endregion
    }
}