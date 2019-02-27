using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quick.IService;
using QuickWeb.Controllers.Common;

namespace QuickWeb.Controllers
{
    public class PublicController : UserBaseController
    {
        public Isnake_roleService snake_roleService { get;set; }
        

    }
}