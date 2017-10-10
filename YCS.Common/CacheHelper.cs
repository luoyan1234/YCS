using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Collections;
using ServiceStack.Redis;
using ServiceStack.Redis.Support;
using System.Configuration;

namespace YCS.Common
{
    /// <summary>
    /// 缓存操作基类
    /// Create by Jimy
    /// </summary>
    public class CacheHelper
    {
        static string CacheType = ConfigurationManager.AppSettings["CacheType"].ToString();
        private static string RedisHost = ConfigurationManager.AppSettings["AliRedisHost"].ToString();
        private static int RedisPort = ConfigurationManager.AppSettings["AliRedisPort"].ToInt();
        private static string RedisPassword = ConfigurationManager.AppSettings["AliRedisPassword"].ToString();
        private static string RedisPath = "redis://" + RedisPassword + "@" + RedisHost + ":" + RedisPort;
        static string CachePrefix = ConfigurationManager.AppSettings["CachePrefix"].ToString();

        private static PooledRedisClientManager Redis()
        {
            string[] readWriteHosts = new string[] { RedisPath };
            PooledRedisClientManager redisPoolManager = new PooledRedisClientManager(10, 10, readWriteHosts);
            return redisPoolManager;
        }


        #region 读取缓存
        /// <summary>
        /// 读取缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetCache(string key)
        {
            key = CachePrefix + key;
            if (CacheType == "Redis")
            {
                //IRedisClient redis = Redis().GetClient();
                RedisClient redis = new RedisClient(RedisHost, RedisPort, RedisPassword);

                if (redis.ContainsKey(key))
                {
                    var ser = new ObjectSerializer();
                    object data = ser.Deserialize(redis.Get<byte[]>(key)) as object;
                    redis.Dispose();
                    return data;
                }
                else
                {
                    redis.Dispose();
                    return null;
                }
            }
            else
            {
                if (HttpRuntime.Cache[key] != null)
                    return HttpRuntime.Cache[key];
                else
                    return null;
            }
        }
        #endregion

        #region 写入缓存
        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="dependencies"></param>
        /// <param name="absoluteExpiration"></param>
        /// <param name="slidingExpiration"></param>
        /// <param name="priority"></param>
        /// <param name="onRemovedCallback"></param>
        /// <returns></returns>
        public static object AddCache(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemovedCallback)
        {
            key = CachePrefix + key;
            if (CacheType == "Redis")
            {
                if (key != string.Empty && value != null)
                {
                    //IRedisClient redis = Redis().GetClient();
                    RedisClient redis = new RedisClient(RedisHost, RedisPort, RedisPassword);
                    var ser = new ObjectSerializer();
                    bool IsSet = redis.Set<object>(key, ser.Serialize(value));
                    redis.Dispose();
                    return IsSet;
                }
                else
                    return null;
            }
            else
            {
                if (HttpRuntime.Cache[key] == null && value != null)
                    return HttpRuntime.Cache.Add(key, value, dependencies, absoluteExpiration, slidingExpiration, priority, onRemovedCallback);
                else
                    return null;
            }

        }
        #endregion

        #region 清除缓存
        /// <summary>
        /// 清除缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object RemoveCache(string key)
        {
            key = CachePrefix + key;
            if (CacheType == "Redis")
            {
                //IRedisClient redis = Redis().GetClient();
                RedisClient redis = new RedisClient(RedisHost, RedisPort, RedisPassword);
                if (redis.ContainsKey(key))
                {
                    bool IsRemove = redis.Remove(key);
                    redis.Dispose();
                    return IsRemove;
                }
                else
                {
                    redis.Dispose();
                    return null;
                }
            }
            else
            {
                if (HttpRuntime.Cache[key] != null)
                    return HttpRuntime.Cache.Remove(key);
                else
                    return null;
            }
        }
        #endregion

        #region 清除所有缓存
        /// <summary>
        /// 清除所有缓存
        /// </summary>
        public static StringBuilder RemoveAllCache()
        {

            StringBuilder strReturn = new StringBuilder();
            if (CacheType == "Redis")
            {
                //IRedisClient redis = Redis().GetClient();
                RedisClient redis = new RedisClient(RedisHost, RedisPort, RedisPassword);
                List<string> keys = redis.SearchKeys("*"); //得到所有的Key
                foreach (string key in keys)
                {
                    strReturn.AppendFormat("<div>{0}</div>", key);
                    redis.Remove(key);
                }
                redis.Dispose();
            }
            else
            {
                IDictionaryEnumerator CacheEnum = HttpRuntime.Cache.GetEnumerator();
                while (CacheEnum.MoveNext())
                {
                    HttpRuntime.Cache.Remove(CacheEnum.Key.ToString());
                    strReturn.AppendFormat("<div>{0}</div>", CacheEnum.Key);
                }
            }
            return strReturn;
        }
        #endregion
    }
}
