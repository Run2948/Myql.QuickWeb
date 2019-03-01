
/* ==============================================================================
* 命名空间：QuickWeb.Areas.Admin.Models.RequestModel 
* 类 名 称：AdminPwdRequest
* 创 建 者：Qing
* 创建时间：2019-03-01 10:21:27
* CLR 版本：4.0.30319.42000
* 保存的文件名：AdminPwdRequest
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
    public class AdminPwdRequest
    {
        /// <summary>
        /// 原始密码
        /// </summary>
        public string old_password { get; set; }
        /// <summary>
        /// 新密码
        /// </summary>
        public string new_password { get; set; }
        /// <summary>
        /// 重复新密码
        /// </summary>
        public string re_new_password { get; set; }
    }
}