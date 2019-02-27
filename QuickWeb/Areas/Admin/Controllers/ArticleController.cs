using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuickWeb.Controllers.Common;

namespace QuickWeb.Areas.Admin.Controllers
{
    public class ArticleController : UserBaseController
    {
        // GET: Admin/Article
        public ActionResult Index()
        {
            return View();
        }
    }
}