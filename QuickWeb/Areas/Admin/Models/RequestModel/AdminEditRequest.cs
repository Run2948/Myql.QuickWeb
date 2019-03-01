
/* ==============================================================================
* 命名空间：QuickWeb.Areas.Admin.Models.RequestModel 
* 类 名 称：AdminEditRequest
* 创 建 者：Qing
* 创建时间：2019-03-01 10:33:02
* CLR 版本：4.0.30319.42000
* 保存的文件名：AdminEditRequest
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

namespace QuickWeb.Areas.Admin.Models.RequestModel
{
    public class AdminEditRequest
    {
        /// <summary>
        /// 头像
        /// </summary>
        public string head { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string real_name { get; set; }
    }
}