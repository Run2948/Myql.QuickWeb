 
/* ==============================================================================
* 命名空间：Quick.IService
* 类 名 称：IBaseService
* 创 建 者：Qing
* 创建时间：2019-02-25 17:45:11
* CLR 版本：4.0.30319.42000
* 保存的文件名：IBaseService
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Quick.IService
{
    public partial interface IBaseService<T>
    {

    }

    /// <summary>
    /// snake_articles业务接口
    /// </summary>
	public partial interface Isnake_articlesService : IBaseService<snake_articles>
	{
		
	}

    /// <summary>
    /// snake_node业务接口
    /// </summary>
	public partial interface Isnake_nodeService : IBaseService<snake_node>
	{
		
	}

    /// <summary>
    /// snake_role业务接口
    /// </summary>
	public partial interface Isnake_roleService : IBaseService<snake_role>
	{
		
	}

    /// <summary>
    /// snake_user业务接口
    /// </summary>
	public partial interface Isnake_userService : IBaseService<snake_user>
	{
		
	}


}
