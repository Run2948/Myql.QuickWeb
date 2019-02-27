using System;
using System.Linq;
using System.Text;

namespace Quick.Models.Entity.Table
{
    ///<summary>
    ///
    ///</summary>
    public partial class snake_user
    {
           public snake_user(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int id {get;set;}

           /// <summary>
           /// Desc:用户名
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string user_name {get;set;}

           /// <summary>
           /// Desc:密码
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string password {get;set;}

           /// <summary>
           /// Desc:头像
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string head {get;set;}

           /// <summary>
           /// Desc:登陆次数
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int login_times {get;set;}

           /// <summary>
           /// Desc:最后登录IP
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string last_login_ip {get;set;}

           /// <summary>
           /// Desc:最后登录时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? last_login_time {get;set;}

           /// <summary>
           /// Desc:真实姓名
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string real_name {get;set;}

           /// <summary>
           /// Desc:状态
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int status {get;set;}

           /// <summary>
           /// Desc:用户角色id
           /// Default:1
           /// Nullable:False
           /// </summary>           
           public int role_id {get;set;}

    }
}
