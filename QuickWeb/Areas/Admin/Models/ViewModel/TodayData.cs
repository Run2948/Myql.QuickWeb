
/* ==============================================================================
* 命名空间：QuickWeb.Areas.Admin.Models.ViewModel 
* 类 名 称：TodayData
* 创 建 者：Qing
* 创建时间：2019-02-27 13:51:47
* CLR 版本：4.0.30319.42000
* 保存的文件名：TodayData
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
    [Serializable]
    public class TodayData
    {
        public int[] is_talking { get; set; }
        public int[] in_queue { get; set; }
        public int[] success_in { get; set; }
        public int[] total_in { get; set; }
    }

    public class DataHelper
    {
        public static TodayData GetData()
        {
            var data = new TodayData
            {
                is_talking = GetRandArray(15, 20, 120),
                in_queue = GetRandArray(15, 0, 20),
                success_in = GetRandArray(15, 50, 200),
                total_in = GetRandArray(15, 150, 300)
            };
            return data;
        }

        private static int[] GetRandArray(int count,int min,int max)
        {
            List<int> nums = new List<int>();
            var random = new Random();
            for (int i = 0; i < count; i++)
            {
                nums.Add(random.Next(min,max));
            }
            return nums.ToArray();
        }
    }
}