
/* ==============================================================================
* 命名空间：Quick.IService 
* 类 名 称：IBaseService
* 创 建 者：Qing
* 创建时间：2019-02-25 19:05:33
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Quick.Models.Dto;
using Quick.Models.Entity.Table;
using SqlSugar;

namespace Quick.IService
{
    /// <summary>
    /// 业务层基类接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial interface IBaseService<T>
    {

    }

    /// <summary>
    /// snake_user业务接口
    /// </summary>
    public partial interface Isnake_userService : IBaseService<snake_user>
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>登录用户信息</returns>
        UserInfoOutputDto Login(string userName, string password);

        /// <summary>
        /// 根据用户名搜索纯净用户信息
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="totalCount">总记录数</param>
        /// <param name="searchText">搜索关键字</param>
        /// <returns>用户信息列表</returns>
        List<UserInfoOutputDto> GetPureUsers(int pageIndex, int pageSize, ref int totalCount, string searchText);
    }

    /// <summary>
    /// snake_node业务接口
    /// </summary>
    public partial interface Isnake_nodeService : IBaseService<snake_node>
    {

    }
}
