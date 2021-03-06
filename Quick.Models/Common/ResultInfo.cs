﻿namespace Quick.Models.Common
{
    public class ResultInfo
    {
        public ResultInfo(int status, string msg, object data, string url)
        {
            Status = status;
            Msg = msg;
            Data = data;
            Url = url;
        }

        public ResultInfo(int status, string msg, object data)
        {
            Status = status;
            Msg = msg;
            Data = data;
        }

        public ResultInfo(int status, string msg, string url)
        {
            Status = status;
            Msg = msg;
            Url = url;
        }

        public ResultInfo(int status, string msg)
        {
            Status = status;
            Msg = msg;
        }

        public ResultInfo()
        {
        }

        /// <summary>
        /// 返回的状态码：ok: 1, error: 0, timeout: 2
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 返回的提示信息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 返回的数据对象
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// 需要跳转的地址
        /// </summary>
        public string Url { get; set; }
    }
}
