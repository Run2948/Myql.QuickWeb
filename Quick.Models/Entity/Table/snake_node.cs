using System;
using System.Linq;
using System.Text;

namespace Quick.Models.Entity.Table
{
    ///<summary>
    ///
    ///</summary>
    public partial class snake_node
    {
           public snake_node(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int id {get;set;}

           /// <summary>
           /// Desc:节点名称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string node_name {get;set;}

           /// <summary>
           /// Desc:控制器名
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string control_name {get;set;}

           /// <summary>
           /// Desc:方法名
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string action_name {get;set;}

           /// <summary>
           /// Desc:是否是菜单项 1不是 2是
           /// Default:1
           /// Nullable:False
           /// </summary>           
           public byte is_menu {get;set;}

           /// <summary>
           /// Desc:父级节点id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int type_id {get;set;}

           /// <summary>
           /// Desc:菜单样式
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string style {get;set;}

    }
}
