
/* ==============================================================================
* 命名空间：Quick.Models.Infrastructure 
* 类 名 称：DbSet
* 创 建 者：Qing
* 创建时间：2019-02-25 19:48:01
* CLR 版本：4.0.30319.42000
* 保存的文件名：DbSet
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

using System.Collections.Generic;
using SqlSugar;

namespace Quick.Models.Application
{
    public class DbSet<T> : SimpleClient<T> where T : class, new()
    {
        public DbSet(SqlSugarClient context)
            : base(context)
        {

        }

        //TODO: SimpleClient中的方法满足不了你，你可以扩展自已的方法

        public List<T> GetByIds(dynamic[] ids)
        {
            return Context.Queryable<T>().In(ids).ToList(); ;
        }

    }
}
