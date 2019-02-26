/* ==============================================================================
* 命名空间：Quick.Common
* 类 名 称：CommonHelper
* 创 建 者：Qing
* 创建时间：2018-11-29 23:35:28
* CLR 版本：4.0.30319.42000
* 保存的文件名：CommonHelper
* 文件版本：V1.0.0.0
*
* 功能描述：N/A 
*
* 修改历史：
*
*
* ==============================================================================
*         CopyRight @ 班纳工作室 2018. All rights reserved
* ==============================================================================*/

using Masuit.Tools.NoSQL;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Quick.Common
{
    /// <summary>
    /// 公共类库
    /// </summary>
    public static class CommonHelper
    {
        /// <summary>
        /// 类型映射
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T Mapper<T>(this object source) where T : class => AutoMapper.Mapper.Map<T>(source);

        /// <summary>
        /// 获取AppSettings中的参数
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetAppSettings(string key)
        {
            return ConfigurationManager.AppSettings[key] ?? "";
        }

        /// <summary>
        /// 根据Key修改Value
        /// </summary>
        /// <param name="key">要修改的Key</param>
        /// <param name="value">要修改为的值</param>
        public static void SetAppSettings(string key, string value)
        {
            System.Xml.XmlDocument xDoc = new System.Xml.XmlDocument();
            xDoc.Load(HttpContext.Current.Server.MapPath("~/Web.config"));
            System.Xml.XmlNode xNode;
            System.Xml.XmlElement xElem1;
            System.Xml.XmlElement xElem2;
            xNode = xDoc.SelectSingleNode("//appSettings");

            xElem1 = (System.Xml.XmlElement)xNode.SelectSingleNode("//add[@key='" + key + "']");
            if (xElem1 != null) xElem1.SetAttribute("value", value);
            else
            {
                xElem2 = xDoc.CreateElement("add");
                xElem2.SetAttribute("key", key);
                xElem2.SetAttribute("value", value);
                xNode.AppendChild(xElem2);
            }
            xDoc.Save(HttpContext.Current.Server.MapPath("~/Web.config"));
        }

        /// <summary>
        /// 在线人数
        /// </summary>
        public static int OnlineCount
        {
            get
            {
                try
                {
                    using (var redisHelper = RedisHelper.GetInstance(1))
                    {
                        return redisHelper.GetServer().Keys(1, "Session:*").Count();
                    }
                }
                catch
                {
                    return 1;
                }
            }
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="tos">收件人</param>
        public static void SendMail(string title, string content, string tos)
        {
#if !DEBUG
            new Email()
            {
                EnableSsl = true,
                Body = content,
                SmtpServer = GetSettings("smtp"),
                Username = GetSettings("EmailFrom"),
                Password = GetSettings("EmailPwd"),
                SmtpPort = GetSettings("SmtpPort").ToInt32(),
                Subject = title,
                Tos = tos
            }.Send();
#endif
        }
    }
}
