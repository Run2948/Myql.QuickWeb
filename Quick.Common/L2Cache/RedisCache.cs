
/* ==============================================================================
* 命名空间：Quick.Common.Cache 
* 类 名 称：RedisCache
* 创 建 者：Qing
* 创建时间：2019-02-26 13:14:47
* CLR 版本：4.0.30319.42000
* 保存的文件名：RedisCache
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
using Quick.Common.NoSQL;
using SqlSugar;

namespace Quick.Common.L2Cache
{
    public class RedisCache : ICacheService
    {
        private RedisHelper service = null;

        public RedisCache(string host, int db)
        {
            service = RedisHelper.GetInstance(host, db);
        }

        public RedisCache(int db)
        {
            service = RedisHelper.GetInstance(db);
        }

        public RedisCache(string host)
        {
            service = RedisHelper.GetInstance(host);
        }

        public RedisCache()
        {
            service = RedisHelper.GetInstance();
        }

        public void Add<V>(string key, V value)
        {
            service.SetString<V>(key, value);
        }

        public void Add<V>(string key, V value, int cacheDurationInSeconds)
        {
            service.SetString<V>(key, value, TimeSpan.FromSeconds(cacheDurationInSeconds));
        }

        public bool ContainsKey<V>(string key)
        {
            return service.KeyExists(key);
        }

        public V Get<V>(string key)
        {
            return service.GetString<V>(key);
        }

        public IEnumerable<string> GetAllKey<V>()
        {
            return service.GetAllKeys();
        }

        public V GetOrCreate<V>(string cacheKey, Func<V> create, int cacheDurationInSeconds = int.MaxValue)
        {
            if (this.ContainsKey<V>(cacheKey))
            {
                return this.Get<V>(cacheKey);
            }
            else
            {
                var result = create();
                this.Add(cacheKey, result, cacheDurationInSeconds);
                return result;
            }
        }

        public void Remove<V>(string key)
        {
            service.DeleteKey(key);
        }
    }
}
