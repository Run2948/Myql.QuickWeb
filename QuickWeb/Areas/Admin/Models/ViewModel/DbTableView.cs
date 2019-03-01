
/* ==============================================================================
* 命名空间：QuickWeb.Areas.Admin.Models.ViewModel 
* 类 名 称：DbTableView
* 创 建 者：Qing
* 创建时间：2019-02-28 20:54:37
* CLR 版本：4.0.30319.42000
* 保存的文件名：DbTableView
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

namespace QuickWeb.Areas.Admin.Models.ViewModel
{
    public class DbTableView
    {
        /// <summary>
        /// 数据表
        /// </summary>
        public string table_name { get; set; }
        /// <summary>
        /// 记录数
        /// </summary>
        public int counts { get; set; }
        /// <summary>
        /// 备份时间
        /// </summary>
        public string back_time { get; set; }
    }
}