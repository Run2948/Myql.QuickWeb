
/* ==============================================================================
* 命名空间：Quick.Models.Common 
* 类 名 称：BsTableInfo
* 创 建 者：Qing
* 创建时间：2019-02-27 15:37:22
* CLR 版本：4.0.30319.42000
* 保存的文件名：BsTableInfo
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
using System.Text;
using System.Threading.Tasks;

namespace Quick.Models.Common
{
    /// <summary>
    /// BootStrap表格数据
    /// </summary>
    public class BsTableInfo
    {
        public BsTableInfo()
        {
        }

        public BsTableInfo(int total, object rows, int code)
        {
            this.total = total;
            this.rows = rows;
            this.code = code;
        }

        public int total { get; set; }

        public object rows { get; set; }

        public int code { get; set; }
    }
}
