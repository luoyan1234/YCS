using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;
using ServiceStack.Redis.Support;
using System.Configuration;

namespace YCS.Common
{
    public class RedisHelper
    {
        //static RedisClient Redis = new RedisClient("118.178.227.188", 6379, "nFJ63Uq1so45xDby1JvY");//redis服务IP和端口
        static string host = ConfigurationManager.AppSettings["Host"].ToString();
        static int port = ConfigurationManager.AppSettings["Port"].ToInt();
        static string password = ConfigurationManager.AppSettings["Password"].ToString();

        static RedisClient Redis = new RedisClient(host, port, password);//redis服务IP和端口

        #region 读取缓存
        /// <summary>
        /// 读取缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object Get(string key)
        {
            if (Redis.Get<object>(key) != null)
            {
                var ser = new ObjectSerializer();
                return ser.Deserialize(Redis.Get<byte[]>(key)) as object;
            }
            else
                return null;
        }
        #endregion

        #region 写入缓存
        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set(string key, object value)
        {
            if (key != string.Empty && value != null)
            {
                var ser = new ObjectSerializer();//位于namespace ServiceStack.Redis.Support;  
                Redis.Set<byte[]>(key, ser.Serialize(value));
            }
        }
        #endregion

        #region 清除单个缓存
        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object RemoveCache(string key)
        {
            if (Redis.Get<object>(key) != null)
                return Redis.Remove(key);
            else
                return null;
        }
        #endregion

        #region 清除所有缓存
        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static StringBuilder RemoveAllCache()
        {
            StringBuilder strResult = new StringBuilder();
            List<string> keys = Redis.SearchKeys("*"); //得到所有的Key
            foreach (string key in keys)
            {
                strResult.AppendFormat("<div>{0}</div>", key);
                Redis.Remove(key);
            }
            return strResult;
        }
        #endregion
    }
}
