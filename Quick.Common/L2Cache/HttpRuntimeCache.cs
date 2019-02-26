
/* ==============================================================================
* 命名空间：Quick.Common.Cache 
* 类 名 称：HttpRuntimeCache
* 创 建 者：Qing
* 创建时间：2019-02-26 13:24:58
* CLR 版本：4.0.30319.42000
* 保存的文件名：HttpRuntimeCache
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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using SqlSugar;

namespace Quick.Common.L2Cache
{
    public class HttpRuntimeCache : ICacheService
    {
        public void Add<V>(string key, V value)
        {
            HttpRuntimeCacheHelper<V>.GetInstance().Add(key, value);
        }

        public void Add<V>(string key, V value, int cacheDurationInSeconds)
        {
            HttpRuntimeCacheHelper<V>.GetInstance().Add(key, value, cacheDurationInSeconds);
        }

        public bool ContainsKey<V>(string key)
        {
            return HttpRuntimeCacheHelper<V>.GetInstance().ContainsKey(key);
        }

        public V Get<V>(string key)
        {
            return HttpRuntimeCacheHelper<V>.GetInstance().Get(key);
        }

        public IEnumerable<string> GetAllKey<V>()
        {
            return HttpRuntimeCacheHelper<V>.GetInstance().GetAllKey();
        }

        public V GetOrCreate<V>(string cacheKey, Func<V> create, int cacheDurationInSeconds = int.MaxValue)
        {
            var cacheManager = HttpRuntimeCacheHelper<V>.GetInstance();
            if (cacheManager.ContainsKey(cacheKey))
            {
                return cacheManager[cacheKey];
            }
            else
            {
                var result = create();
                cacheManager.Add(cacheKey, result, cacheDurationInSeconds);
                return result;
            }
        }

        public void Remove<V>(string key)
        {
            HttpRuntimeCacheHelper<V>.GetInstance().Remove(key);
        }
    }

    internal class HttpRuntimeCacheHelper<V>
    {

        #region 全局变量
        private static HttpRuntimeCacheHelper<V> _instance = null;
        private static readonly object _instanceLock = new object();
        #endregion

        #region 构造函数

        private HttpRuntimeCacheHelper() { }
        
        #endregion

        #region  属性
        /// <summary>         
        ///根据key获取value     
        /// </summary>         
        /// <value></value>      
        public V this[string key] => (V)HttpRuntime.Cache[CreateKey(key)];

        #endregion

        #region 公共函数

        /// <summary>         
        /// key是否存在       
        /// </summary>         
        /// <param name="key">key</param>         
        /// <returns>存在<c>true</c> 不存在<c>false</c>.</returns>         
        public bool ContainsKey(string key)
        {
            return HttpRuntime.Cache[CreateKey(key)] != null;
        }

        /// <summary>         
        /// 获取缓存值         
        /// </summary>         
        /// <param name="key">key</param>         
        /// <returns></returns>         
        public V Get(string key)
        {
            return (V)HttpRuntime.Cache.Get(CreateKey(key));
        }

        /// <summary>         
        /// 获取实例 （单例模式）       
        /// </summary>         
        /// <returns></returns>         
        public static HttpRuntimeCacheHelper<V> GetInstance()
        {
            if (_instance == null)
                lock (_instanceLock)
                    if (_instance == null)
                        _instance = new HttpRuntimeCacheHelper<V>();
            return _instance;
        }

        /// <summary>         
        /// 插入缓存(默认20分钟)        
        /// </summary>         
        /// <param name="key"> key</param>         
        /// <param name="value">value</param>          
        public void Add(string key, V value)
        {
            Add(key, value, 60 * 20);
        }

        /// <summary>         
        /// 插入缓存        
        /// </summary>         
        /// <param name="key"> key</param>         
        /// <param name="value">value</param>         
        /// <param name="cacheDurationInSeconds">过期时间单位秒</param>         
        public void Add(string key, V value, int cacheDurationInSeconds)
        {
            Add(key, value, cacheDurationInSeconds, CacheItemPriority.Default);
        }

        /// <summary>         
        /// 插入缓存.         
        /// </summary>         
        /// <param name="key">key</param>         
        /// <param name="value">value</param>         
        /// <param name="cacheDurationInSeconds">过期时间单位秒</param>         
        /// <param name="priority">缓存项属性</param>         
        public void Add(string key, V value, int cacheDurationInSeconds, CacheItemPriority priority)
        {
            string keyString = CreateKey(key);
            HttpRuntime.Cache.Insert(keyString, value, null,
            DateTime.Now.AddSeconds(cacheDurationInSeconds), Cache.NoSlidingExpiration, priority, null);
        }

        /// <summary>         
        /// 插入缓存.         
        /// </summary>         
        /// <param name="key">key</param>         
        /// <param name="value">value</param>         
        /// <param name="cacheDurationInSeconds">过期时间单位秒</param>
        /// <param name="dependency"></param>
        /// <param name="priority">缓存项属性</param>         
        public void Add(string key, V value, int
         cacheDurationInSeconds, CacheDependency dependency, CacheItemPriority priority)
        {
            string keyString = CreateKey(key);
            HttpRuntime.Cache.Insert(keyString, value,
             dependency, DateTime.Now.AddSeconds(cacheDurationInSeconds), Cache.NoSlidingExpiration, priority, null);
        }

        /// <summary>         
        /// 删除缓存         
        /// </summary>         
        /// <param name="key">key</param>         
        public void Remove(string key)
        {
            HttpRuntime.Cache.Remove(CreateKey(key));
        }

        /// <summary>
        /// 清除所有缓存
        /// </summary>
        public void RemoveAll()
        {
            System.Web.Caching.Cache cache = HttpRuntime.Cache;
            IDictionaryEnumerator cacheEnum = cache.GetEnumerator();
            ArrayList al = new ArrayList();
            while (cacheEnum.MoveNext())
            {
                al.Add(cacheEnum.Key);
            }
            foreach (string key in al)
            {
                cache.Remove(key);
            }
        }

        /// <summary>
        /// 清除所有包含关键字的缓存
        /// </summary>
        /// <param name="removeExpression">关键字</param>
        public void RemoveAll(Func<string, bool> removeExpression)
        {
            System.Web.Caching.Cache cache = HttpRuntime.Cache;
            var allKeyList = GetAllKey();
            var delKeyList = allKeyList.Where(removeExpression).ToList();
            foreach (var key in delKeyList)
            {
                HttpRuntime.Cache.Remove(key); ;
            }
        }

        /// <summary>
        /// 获取所有缓存key
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetAllKey()
        {
            IDictionaryEnumerator cacheEnum = HttpRuntime.Cache.GetEnumerator();
            while (cacheEnum.MoveNext())
            {
                yield return cacheEnum.Key?.ToString();
            }
        }
        #endregion

        #region 私有函数

        /// <summary>         
        ///创建KEY   
        /// </summary>         
        /// <param name="key">Key</param>         
        /// <returns></returns>         
        private string CreateKey(string key)
        {
            return key;
        }
        #endregion
    }
}
