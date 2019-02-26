/* ==============================================================================
* 命名空间：Quick.Common.Models
* 类 名 称：SouhuIp
* 创 建 者：Qing
* 创建时间：2018-12-18 21:36:36
* CLR 版本：4.0.30319.42000
* 保存的文件名：SouhuIp
* 文件版本：V1.0.0.0
*
* 功能描述：N/A 
*
* 修改历史：
*
*
* ==============================================================================
*         CopyRight @ 班纳工作室 2018. All rights reserved
* ==============================================================================*/



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Common.Models
{
    /// <summary>
    /// C#通过IP获取用户地理位置省份城市的接口使用
    /// https://blog.csdn.net/u014424282/article/details/78603407
    /// </summary>
    public class SouhuAddress
    {
        public string cip { get; set; }

        public string cid { get; set; }

        public string cname { get; set; }
    }
}
