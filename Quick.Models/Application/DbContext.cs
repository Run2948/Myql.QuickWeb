
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

using System;
using System.Configuration;
using System.Linq;
using Quick.Common.L2Cache;
using SqlSugar;
using SyntacticSugar;

namespace Quick.Models.Application
{
    public partial class DbContext
    {
        public static string SqlConnStr = ConfigSugar.GetConfigString("DefaultConnection");
        /// <summary>
        /// 是否启用Redis：如果启用了，请注意保证正确配置Redis连接
        /// </summary>
        protected bool EnableRedis = ConfigurationManager.AppSettings["EnableRedis"] != null && bool.Parse(ConfigurationManager.AppSettings["EnableRedis"]);

        public static string RedisConnStr = ConfigSugar.GetConfigString("RedisHosts");

        public DbContext()
        {
            try
            {
                Db = new SqlSugarClient(new ConnectionConfig()
                {
                    ConnectionString = SqlConnStr,
                    DbType = DbType.MySql,
                    InitKeyType = InitKeyType.SystemTable,//InitKeyType.Attribute:从特性读取主键和自增列信息/InitKeyType.SystemTable:从数据库主键的方式获取主键信息
                    IsAutoCloseConnection = true,//开启自动释放模式和EF原理一样我就不多解释了
                    ConfigureExternalServices = new ConfigureExternalServices()
                    {
                        //new RedisCache()  //RedisCache是继承ICacheService自已实现的一个类
                        //new HttpRuntimeCache()  //HttpRuntimeCache是继承ICacheService自已实现的一个类
                        DataInfoCacheService = new HttpRuntimeCache() 
                        //DataInfoCacheService = new RedisCache(RedisConnStr)
                    },
                    MoreSettings = new ConnMoreSettings()
                    {
                        //IsWithNoLockQuery = true,
                        IsAutoRemoveDataCache = true,
                    }
                });
            }
            catch (Exception ex)
            {
                throw new Exception("连接数据库出错，请检查您的连接字符串，和网络。 ex:".AppendString(ex.Message));
            }

            #region 1.查看生成的SQL
            //Db.Aop.OnLogExecuted = (sql, pars) => //SQL执行完事件
            //{

            //};
            //Db.Aop.OnLogExecuting = (sql, pars) => //SQL执行前事件
            //{

            //};
            //Db.Aop.OnError = (exp) =>//执行SQL 错误事件
            //{
            //    //exp.sql exp.parameters 可以拿到参数和错误Sql             
            //};
            //Db.Aop.OnExecutingChangeSql = (sql, pars) => //SQL执行前 可以修改SQL
            //{
            //    return new KeyValuePair<string, SugarParameter[]>(sql, pars);
            //};
            #endregion

            #region  2.新版本获取执行时间
            // 2.新版本获取执行时间
            //Db.Ado.SqlExecutionTime 
            #endregion

            #region 3.使用事务
            //3.使用db处理事务
            //Db.Ado.UseTran(() =>
            //{
            //    var s= SchoolDb.GetById(1);//使用SchoolDb查询一条记录
            //});
            #endregion

            #region 4.多表查询
            //4.多表查询
            //var viewList= Db
            //    .Queryable<Student, School>((st, sc) => st.SchoolId == sc.Id)
            //    .Select((st, sc) => new { st, sc })
            //    .ToList();
            #endregion

#if DEBUG
            //TODO: 调式代码 用来打印SQL 
            Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql + "\r\n" + Db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                Console.WriteLine();
            };
#endif
            //TODO: 推荐用法-SqlSugar4-文档园 http://www.codeisbug.com/Doc/8/1151
        }
    }
}
