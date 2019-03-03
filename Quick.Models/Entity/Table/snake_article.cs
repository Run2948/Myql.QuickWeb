using System;
using System.Linq;
using System.Text;

namespace Quick.Models.Entity.Table
{
    ///<summary>
    ///
    ///</summary>
    public partial class snake_article
    {
           public snake_article(){


           }
           /// <summary>
           /// Desc:文章id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int id {get;set;}

           /// <summary>
           /// Desc:文章标题
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string title {get;set;}

           /// <summary>
           /// Desc:文章描述
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string description {get;set;}

           /// <summary>
           /// Desc:文章关键字
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string keywords {get;set;}

           /// <summary>
           /// Desc:文章缩略图
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string thumbnail {get;set;}

           /// <summary>
           /// Desc:文章内容
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string content {get;set;}

           /// <summary>
           /// Desc:发布时间
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime add_time {get;set;}

    }
}
