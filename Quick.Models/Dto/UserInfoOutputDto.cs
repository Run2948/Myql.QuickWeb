using System;
using System.Collections.Generic;

namespace Quick.Models.Dto
{
    /// <summary>
    /// 用户信息输出模型
    /// </summary>
    public class UserInfoOutputDto : BaseDto
    {
        /// <summary>
        /// Desc:用户名
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string user_name { get; set; }

        /// <summary>
        /// Desc:头像
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string head { get; set; }

        /// <summary>
        /// Desc:登陆次数
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int login_times { get; set; }

        /// <summary>
        /// Desc:最后登录IP
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string last_login_ip { get; set; }

        /// <summary>
        /// Desc:最后登录时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? last_login_time { get; set; }

        /// <summary>
        /// Desc:真实姓名
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string real_name { get; set; }

        /// <summary>
        /// Desc:状态
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int status { get; set; }

        /// <summary>
        /// Desc:用户角色id
        /// Default:1
        /// Nullable:False
        /// </summary>           
        public int role_id { get; set; }

        /// <summary>
        /// Desc:角色名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string role_name { get; set; }

        /// <summary>
        /// Desc:权限节点数据
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string rule { get; set; }

        /// <summary>
        /// Desc:用户所有权限集合
        /// </summary>
        public List<string> rights { get; set; }
    }
}
