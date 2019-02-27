using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quick.IService;
using QuickWeb.Controllers.Common;

namespace QuickWeb.Areas.Admin.Controllers
{
    public class ProfileController : UserBaseController
    {
        public Isnake_userService snake_userService { get;set; }	

        // GET: Admin/Profile
        public ActionResult Index()
        {
            return View();
        }
    }
}