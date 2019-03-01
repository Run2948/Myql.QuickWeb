 
/* ==============================================================================
* 命名空间：Quick.Models.Infrastructure 
* 类 名 称：DbContext
* 创 建 者：Qing
* 创建时间：2019-02-25 19:43:19
* CLR 版本：4.0.30319.42000
* 保存的文件名：DbContext
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

using Quick.Models.Entity.Table;
using SqlSugar;

namespace Quick.Models.Application
{
    public partial class DbContext
    {
		/// <summary>
		/// 用来处理事务多表查询和复杂的操作
		/// </summary>
		public SqlSugarClient Db; 
		
		/// <summary>
		/// 用来处理snake_article表的常用操作
		/// </summary>
		public DbSet<snake_article> snake_articleDb => new DbSet<snake_article>(Db); 

		/// <summary>
		/// 用来处理snake_node表的常用操作
		/// </summary>
		public DbSet<snake_node> snake_nodeDb => new DbSet<snake_node>(Db); 

		/// <summary>
		/// 用来处理snake_role表的常用操作
		/// </summary>
		public DbSet<snake_role> snake_roleDb => new DbSet<snake_role>(Db); 

		/// <summary>
		/// 用来处理snake_user表的常用操作
		/// </summary>
		public DbSet<snake_user> snake_userDb => new DbSet<snake_user>(Db); 

    }
}
