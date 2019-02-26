 
/* ==============================================================================
* 命名空间：Quick.Service
* 类 名 称：BaseService
* 创 建 者：Qing
* 创建时间：2019-02-25 17:45:11
* CLR 版本：4.0.30319.42000
* 保存的文件名：BaseService
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
using Quick.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quick.Models.Application;

namespace Quick.Service
{
    /// <summary>
    /// 业务层基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class BaseService<T> : DbContext
    {

    }

    /// <summary>
    /// snake_articles业务类
    /// </summary>
	public partial class snake_articlesService : BaseService<snake_articles>, Isnake_articlesService
	{
		
	}

    /// <summary>
    /// snake_node业务类
    /// </summary>
	public partial class snake_nodeService : BaseService<snake_node>, Isnake_nodeService
	{
		
	}

    /// <summary>
    /// snake_role业务类
    /// </summary>
	public partial class snake_roleService : BaseService<snake_role>, Isnake_roleService
	{
		
	}

    /// <summary>
    /// snake_user业务类
    /// </summary>
	public partial class snake_userService : BaseService<snake_user>, Isnake_userService
	{
		
	}


}
