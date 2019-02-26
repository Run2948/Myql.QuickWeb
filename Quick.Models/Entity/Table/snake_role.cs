using System;
using System.Linq;
using System.Text;

namespace Quick.Models.Entity.Table
{
    ///<summary>
    ///
    ///</summary>
    public partial class snake_role
    {
           public snake_role(){


           }
           /// <summary>
           /// Desc:id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int id {get;set;}

           /// <summary>
           /// Desc:角色名称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string role_name {get;set;}

           /// <summary>
           /// Desc:权限节点数据
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string rule {get;set;}

    }
}
