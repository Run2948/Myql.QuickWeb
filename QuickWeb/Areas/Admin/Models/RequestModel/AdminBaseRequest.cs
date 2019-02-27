
/* ==============================================================================
* 命名空间：QuickWeb.Areas.Admin.Models.RequestModel 
* 类 名 称：AdminBaseRequest
* 创 建 者：Qing
* 创建时间：2019-02-27 15:02:08
* CLR 版本：4.0.30319.42000
* 保存的文件名：AdminBaseRequest
* 文件版本：V1.0.0.0
*
* 功能描述：N/A 
*
* 修改历史：
*
*
* ==============================================================================
*         CopyRight @ 班纳工作室 2019. All rights reserved
* ==============================================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quick.Models.Common;

namespace QuickWeb.Areas.Admin.Models.RequestModel
{
    public class AdminBaseRequest : PageRequest
    {
        public string searchText { get; set; }
    }
}