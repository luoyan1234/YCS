using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YCS.Common
{
    /// <summary>
    /// 类型转换 数据处理专用
    /// </summary>
    public class ConvertObjectData
    {
        /// <summary>
        /// 将可为空的数据转换为数值类型
        /// </summary>
        /// <param name="obj">要转换的数据</param>
        /// <returns></returns>
        public static decimal? ConvertToNullOrDecimal(object obj)
        {
            if (obj != null)
            {
                decimal d;
                if (decimal.TryParse(obj.ToString(), NumberStyles.AllowDecimalPoint, NumberFormatInfo.InvariantInfo, out d))
                {
                    return d;
                }
                else
                {
                    return null;
                }
            }
            return null;
        }


        /// <summary>
        /// object转换为decimal
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <returns>转换结果</returns>
        public static decimal ConvertToDecimal(object obj)
        {
            if (obj != null && !obj.GetType().Equals(typeof(System.DBNull)))
            {
                decimal d;
                if (decimal.TryParse(obj.ToString(), out d))
                {
                    return d;
                }
            }
            return 0;
        }


        /// <summary>
        /// object转换为string
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <returns>转换结果</returns>
        public static string ConvertToString(object obj)
        {
            if (obj != null && !obj.GetType().Equals(typeof(System.DBNull)))
            {
                return obj.ToString();
            }
            return "";
        }

        /// <summary>
        /// object转换为DateTime
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <returns>转换后时间，异常时为最小时间</returns>
        public static DateTime ConvertToDateTime(object obj)
        {
            if (obj != null && !obj.GetType().Equals(typeof(System.DBNull)))
            {
                string o = obj.ToString();
                DateTime d;
                if (DateTime.TryParse(o, out d))
                {
                    return d;
                }
            }
            return DateTime.MinValue;
        }

        /// <summary>
        /// object转换为DateTime
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <returns>转换后时间，异常时为空</returns>
        /// <remarks>aizg add:2013-11-7</remarks>
        public static DateTime? ConvertToDateTimeNull(object obj)
        {
            if (obj != null && !obj.GetType().Equals(typeof(System.DBNull)))
            {
                string o = obj.ToString();
                DateTime d;
                if (DateTime.TryParse(o, out d))
                {
                    return d;
                }
            }
            return null;
        }

        /// <summary>
        /// object转换为int
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>转换结果，-1 为失败。</returns>
        public static int ConvertToInt(object obj)
        {
            if (obj != null && !obj.GetType().Equals(typeof(System.DBNull)))
            {
                int d;
                if (int.TryParse(obj.ToString(), out d))
                {
                    return d;
                }
            }
            return 0;
        }

        /// <summary>
        /// object转换为int
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>转换结果，-1 为失败。</returns>
        public static int? ConvertToNullOrInt(object obj)
        {
            if (obj != null && !obj.GetType().Equals(typeof(System.DBNull)))
            {
                int d;
                if (int.TryParse(obj.ToString(), out d))
                {
                    return d;
                }
            }
            return null;
        }

        /// <summary>
        /// object转换为int
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>转换结果</returns>
        public static bool ConvertToBool(object obj)
        {
            if (obj != null && !obj.GetType().Equals(typeof(System.DBNull)))
            {
                int d = 0;
                if (!int.TryParse(obj.ToString(), out d))
                {
                    d = -1;
                }
                if (d == 1 || obj.ToString().Equals("true", StringComparison.CurrentCultureIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// object转换为int
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>转换结果</returns>
        public static bool? ConvertToNullOrBool(object obj)
        {
            if (obj != null && !obj.GetType().Equals(typeof(System.DBNull)))
            {
                int d = 0;
                if (!int.TryParse(obj.ToString(), out d))
                {
                    d = -1;
                }
                if (d == 1)
                {
                    return true;
                }
                return false;
            }
            return null;
        }


        /// <summary>
        /// object转换为decimal
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <returns>转换结果</returns>
        public static decimal ConvertToDecimalDB(object obj)
        {
            if (obj != null && !obj.GetType().Equals(typeof(System.DBNull)))
            {
                decimal d;
                if (decimal.TryParse(obj.ToString(), out d))
                {
                    return d;
                }
            }
            return -1;
        }

        /// <summary>
        /// object转换为byte
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <returns>转换结果</returns>
        public static byte ConvertToByte(object obj)
        {
            if (obj != null && !obj.GetType().Equals(typeof(System.DBNull)))
            {
                byte d;
                if (byte.TryParse(obj.ToString(), out d))
                {
                    return d;
                }
            }
            return 0;
        }

        /// <summary>
        /// object转换为Double
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <returns>转换结果</returns>
        public static double ConvertToDouble(object obj)
        {
            if (obj != null && !obj.GetType().Equals(typeof(System.DBNull)))
            {
                double d;
                if (double.TryParse(obj.ToString(), out d))
                {
                    return d;
                }
            }
            return 0;
        }
    }
}
