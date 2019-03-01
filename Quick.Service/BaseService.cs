
/* ==============================================================================
* 命名空间：Quick.Service 
* 类 名 称：BaseService
* 创 建 者：Qing
* 创建时间：2019-02-25 19:06:02
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Quick.IService;
using Quick.Models.Application;
using Quick.Models.Dto;
using Quick.Models.Entity.Table;
using SqlSugar;

namespace Quick.Service
{
    /// <summary>
    /// 业务层基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class BaseService<T>
    {

    }

    /// <summary>
    /// snake_user业务类
    /// </summary>
    public partial class snake_userService : BaseService<snake_user>, Isnake_userService
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>登录用户信息</returns>
        public UserInfoOutputDto Login(string userName, string password)
        {
            var dto = Db.Queryable<snake_user, snake_role>((st, sc) => new object[]
             {
                JoinType.Left,st.role_id==sc.id,
             })
            .Where((st, sc) => st.user_name == userName && st.password == password)
            //.Select((st, sc) => new UserInfoOutputDto
            //{
            //    id = st.id,
            //    user_name = st.user_name,
            //    head = st.head,
            //    login_times = st.login_times,
            //    last_login_ip = st.last_login_ip,
            //    last_login_time = st.last_login_time,
            //    status = st.status,
            //    role_id = st.role_id,
            //    role_name = sc.role_name,
            //    rule = sc.rule,
            //}).First();
            .Select<UserInfoOutputDto>().First();
            return dto;
        }

        /// <summary>
        /// 根据用户名搜索纯净用户信息
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="totalCount">总记录数</param>
        /// <param name="searchText">搜索关键字</param>
        /// <returns>用户信息列表</returns>
        public List<UserInfoOutputDto> GetPureUsers(int pageIndex, int pageSize, ref int totalCount, string searchText)
        {
            var list = Db.Queryable<snake_user, snake_role>((st, sc) => new object[]
                {
                    JoinType.Left,st.role_id==sc.id,
                })
                .Where((st, sc) => st.user_name.Contains(searchText) || searchText == null)
                .Select<UserInfoOutputDto>()
                .OrderBy(st => st.id, OrderByType.Desc)
                .ToPageList(pageIndex, pageSize, ref totalCount);
            return list;
        }
    }

    /// <summary>
    /// snake_node业务类
    /// </summary>
    public partial class snake_nodeService : BaseService<snake_node>, Isnake_nodeService
    {

    }
}
