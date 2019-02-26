using System;

namespace Quick.Models.Common
{
    [Serializable]
    public class PageRequest
    {
        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize { get; set; } = 12;
        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; set; } = 1;
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPage { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalCount = 0;
    }
}
