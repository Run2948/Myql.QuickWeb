using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuickWeb.Controllers.Common;

namespace QuickWeb.Areas.Admin.Controllers
{
    public class RoleController : UserBaseController
    {
        // GET: Admin/Role
        public ActionResult Index()
        {
            return View();
        }
    }
}