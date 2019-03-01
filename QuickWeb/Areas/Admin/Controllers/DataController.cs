using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Masuit.Tools.Logging;
using Quick.Common;
using Quick.IService;
using QuickWeb.Areas.Admin.Models.ViewModel;
using QuickWeb.Controllers.Common;

namespace QuickWeb.Areas.Admin.Controllers
{
    public class DataController : UserBaseController
    {
        public Isnake_nodeService snake_nodeService { get; set; }

        private readonly string _dataBackUpPath = JsonConfig.GetString("back_path") ?? "/upload/data/backup";

        #region 数据表备份列表
        // GET: Admin/Data
        [HttpGet]
        public ActionResult Index()
        {
            List<DbTableView> list = new List<DbTableView>();
            var tableDt = snake_nodeService.GetDataTable("show tables;");
            foreach (DataRow row in tableDt.Rows)
            {
                var model = new DbTableView
                {
                    table_name = row[0].ToString()
                };
                model.counts= snake_nodeService.GetCount($"select count(0) from {model.table_name};");
                model.back_time = GetBackFileTime($"{model.table_name}.sql");
                list.Add(model);
            }
            return View(list);
        } 
        #endregion

        #region 备份数据表
        [HttpGet]
        public ActionResult BackData(string table)
        {
            if(string.IsNullOrEmpty(table)) return ParamsError();
            try
            {
                string sql = "SET FOREIGN_KEY_CHECKS=0;\r\n";
                sql += $"DROP TABLE IF EXISTS `{table}`;\r\n";
                var createDt = snake_nodeService.GetDataTable($"show create table {table}");
                sql += $"{createDt.Rows[0]["Create Table"]};\r\n";
                sql += "\r\n";
                var selectDt = snake_nodeService.GetDataTable($"select * from {table};", null);
                var result = GetDict(selectDt);
                if (result != null)
                {
                    foreach (var kv in result)
                    {
                        sql += $"insert into `{table}`({kv.Key}) values({kv.Value});\r\n";
                    }
                }
                //LogManager.Info($"{DateTime.Now}备份的sql语句：\r\n{sql}");
                SaveSqlToFile(sql, table);
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(),e);
                return No(e.Message);
            }
            return Ok();
        }
        #endregion

        #region 还原数据表
        [HttpGet]
        public ActionResult ImportData(string table)
        {
            if(string.IsNullOrEmpty(table)) return ParamsError();
            try
            {
                var filePath = System.IO.Path.Combine(Request.MapPath(_dataBackUpPath),$"{table}.sql");
                if(!FileExists(filePath)) return No("备份数据不存在!");
                var sql = System.IO.File.ReadAllText(filePath,System.Text.Encoding.UTF8);
                snake_nodeService.ExecuteSql(sql,null);
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(),e);
                return No(e.Message);
            }
            return Ok();
        }
        #endregion

        #region 辅助方法
        /// <summary>
        /// 根据DataTable获得列名
        /// </summary>
        /// <param name="dt">表对象</param>
        /// <returns>返回结果的数据列数组</returns>
        private string[] GetColumns(DataTable dt)
        {
            if(dt.Columns.Count < 1) return null;
            int columnCount = dt.Columns.Count;
            string[] strColumns = new string[columnCount];
            for (int i = 0; i < columnCount; i++)
            {
                strColumns[i] = dt.Columns[i].ColumnName;
            }
            return strColumns;
        }

        /// <summary>
        /// 根据DataTable获得数据集合
        /// </summary>
        /// <param name="dt">表对象</param>
        /// <param name="columnCount">取前多少列的值</param>
        /// <returns>返回结果的数据列集合</returns>
        private List<string> GetRows(DataTable dt,int columnCount = 0)
        {
            if (columnCount == 0) 
                columnCount = dt.Columns.Count;
            if (columnCount == 0) return null;
            List<string> list = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                var strValues = new string[columnCount];
                for (int i = 0; i < columnCount; i++)
                {
                    strValues[i] =  "'"+row[i]+"'";
                }
                list.Add(string.Join(",",strValues));
            }
            return list;
        }

        /// <summary>
        /// 根据DataTable获得列名和数据集合
        /// </summary>
        /// <param name="dt">表对象</param>
        /// <returns></returns>
        private Dictionary<string,string> GetDict(DataTable dt)
        {
            if(dt.Columns.Count == 0) return null;
            if(dt.Rows.Count == 0) return null;
            Dictionary<string,string> kv = new Dictionary<string, string>();
            var columns = string.Join(",",GetColumns(dt));
            var rows = GetRows(dt);
            rows.ForEach(row => kv.Add(columns,row));
            return kv;
        }

        /// <summary>
        /// 将sql语句写入文件
        /// </summary>
        /// <param name="sql">待写入的sql语句</param>
        /// <param name="fileName">待保存的文件名</param>
        /// <returns></returns>
        private void SaveSqlToFile(string sql,string fileName)
        {
            var dir = Request.MapPath(_dataBackUpPath);
            DirectoryCreates(dir);
            System.IO.File.WriteAllText(System.IO.Path.Combine(dir,$"{fileName}.sql"),sql,System.Text.Encoding.UTF8);
        }

        /// <summary>
        /// 获取备份的文件的创建时间
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <returns></returns>
        private string GetBackFileTime(string fileName)
        {
            var filePath = System.IO.Path.Combine(Request.MapPath(_dataBackUpPath),fileName);
            if(!FileExists(filePath)) return null;
            System.IO.FileInfo file = new System.IO.FileInfo(filePath);
            return file.CreationTime.ToString("yyyy-MM-dd HH:mm:ss");
        }
        
        #endregion
    }
}