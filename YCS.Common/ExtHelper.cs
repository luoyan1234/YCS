using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Security;
using System.Xml;

namespace YCS.Common
{
    /// <summary>
    /// 扩展类
    /// Create by Jimy
    /// </summary>
    public static class ExtHelper
    {
        #region 是否为邮箱地址
        /// <summary>
        /// 是否为邮箱地址
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        /// <returns></returns>
        public static bool IsEmail(this string str)
        {
            Regex re = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            return re.IsMatch(str);
        }
        #endregion

        #region 是否为手机号码
        /// <summary>
        /// 是否为手机号码
        /// </summary>
        /// <param name="str">要检查的字符串</param>
        /// <returns></returns>
        public static bool IsMobile(this string str)
        {
            Regex re = new Regex(@"^1[34578]\d{9}$");
            return re.IsMatch(str);
        }
        #endregion

        #region 是否为十进制数字
        /// <summary>
        /// 是否为十进制数字
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        /// <returns></returns>
        public static bool IsDecimal(this string str)
        {
            decimal dec;
            return decimal.TryParse(str, out dec);
        }
        #endregion

        #region 是否为整型
        /// <summary>
        /// 是否为整型
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        /// <returns></returns>
        public static bool IsInt(this string str)
        {
            int dec;
            return int.TryParse(str, out dec);
        }
        #endregion

        #region 是否为日期
        /// <summary>
        /// 是否为日期
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        /// <returns></returns>
        public static bool IsDate(this string str)
        {
            DateTime dt;
            return DateTime.TryParse(str, out dt);
        }
        #endregion

        #region 是否为IP地址
        /// <summary>
        /// 是否为IP地址
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        /// <returns></returns>
        public static bool IsIP(this string str)
        {
            IPAddress ip;
            return IPAddress.TryParse(str, out ip);
        }
        #endregion

        #region 是否为用户名
        /// <summary>
        /// 是否为用户名
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        /// <returns></returns>
        public static bool IsUserName(this string str)
        {
            Regex re = new Regex(@"[a-zA-Z]\w{3,15}");
            return re.IsMatch(str);
        }
        #endregion

        #region 是否为GUID
        /// <summary>
        /// 是否为GUID
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        /// <returns></returns>
        public static bool IsGuid(this string str)
        {
            Guid guid;
            return Guid.TryParse(str, out guid);
        }
        #endregion

        #region md5加密
        /// <summary>
        /// md5加密
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        /// <returns></returns>
        public static string md5(this string str)
        {
            MD5 md5 = MD5.Create();
            string pwd = "";
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(str));

            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符

                pwd = pwd + s[i].ToString("X2");

            }
            return pwd;
        }
        #endregion

        #region  SHA1加密
        /// <summary>
        /// SHA1加密
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <returns></returns>
        public static byte[] SHA1(this string str)
        {
            byte[] SHA1Data = Encoding.UTF8.GetBytes(str);
            SHA1Managed Sha1 = new SHA1Managed();
            byte[] result = Sha1.ComputeHash(SHA1Data);
            return result;
        }
        #endregion

        #region  SHA256加密
        /// <summary>
        /// SHA256加密
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <returns></returns>
        public static byte[] SHA256(this string str)
        {
            byte[] SHA256Data = Encoding.UTF8.GetBytes(str);
            SHA256Managed Sha256 = new SHA256Managed();
            byte[] result = Sha256.ComputeHash(SHA256Data);
            return result;
        }
        #endregion

        #region  HMACSHA1加密
        /// <summary>
        /// HMACSHA1加密
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <returns></returns>
        public static byte[] HMACSHA1(this string str, string secret)
        {
            HMACSHA1 hmacsha1 = new HMACSHA1();
            hmacsha1.Key = Encoding.UTF8.GetBytes(secret);
            byte[] dataBuffer = Encoding.UTF8.GetBytes(str);
            byte[] hashBytes = hmacsha1.ComputeHash(dataBuffer);
            return hashBytes;
        }
        #endregion

        #region 当对象为null时,赋默认值
        /// <summary>
        /// 当对象为null时,赋默认值
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        /// <returns></returns>
        public static object DefVal(this object obj)
        {
            return obj == null ? "" : obj;
        }
        #endregion

        #region Object转换为Byte
        /// <summary>
        /// Object转换为Byte
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        /// <returns></returns>
        public static int ToByte(this object obj)
        {
            return Convert.IsDBNull(obj) || obj == null || string.IsNullOrEmpty(obj.ToString()) ? 0 : Convert.ToByte(obj);
        }
        #endregion

        #region Object转换为Int
        /// <summary>
        /// Object转换为Int
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        /// <returns></returns>
        public static int ToInt(this object obj)
        {
            return Convert.IsDBNull(obj) || obj == null || string.IsNullOrEmpty(obj.ToString()) ? 0 : Convert.ToInt32(obj);
        }
        #endregion

        #region Object转换为Decimal
        /// <summary>
        /// Object转换为Decimal
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        /// <returns></returns>
        public static decimal ToDecimal(this object obj)
        {
            return Convert.IsDBNull(obj) || obj == null || string.IsNullOrEmpty(obj.ToString()) ? 0 : Convert.ToDecimal(obj);
        }
        #endregion

        #region Object转换为float
        /// <summary>
        /// Object转换为float
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        /// <returns></returns>
        public static float ToFloat(this object obj)
        {
            return Convert.IsDBNull(obj) || obj == null || string.IsNullOrEmpty(obj.ToString()) ? 0 : Convert.ToSingle(obj);
        }
        #endregion

        #region Object转换为Long
        /// <summary>
        /// Object转换为Long
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        /// <returns></returns>
        public static long ToLong(this object obj)
        {
            return Convert.IsDBNull(obj) || obj == null || string.IsNullOrEmpty(obj.ToString()) ? 0 : Convert.ToInt64(obj);
        }
        #endregion

        #region Object转换为Boolean
        /// <summary>
        /// Object转换为Boolean
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        /// <returns></returns>
        public static bool ToBoolean(this object obj)
        {
            return Convert.IsDBNull(obj) || obj == null || string.IsNullOrEmpty(obj.ToString()) ? false : Convert.ToBoolean(obj);
        }
        #endregion

        #region Object转换为String
        /// <summary>
        /// Object转换为String
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        /// <returns></returns>
        public static string ToString2(this object obj)
        {
            return Convert.IsDBNull(obj) || obj == null ? "" : Convert.ToString(obj);
        }
        #endregion

        #region Object转换为DateTime
        /// <summary>
        /// Object转换为DateTime
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        /// <returns></returns>
        public static string ToDateTime(this object obj, string strFmt, string strReturn)
        {
            if (Convert.IsDBNull(obj) || obj == null)
            {
                return strReturn;
            }
            else
            {
                DateTime dt;
                if (DateTime.TryParse(obj.ToString(), out dt))
                {
                    if (dt.Year > 1900)
                    {
                        return dt.ToString(strFmt);
                    }
                }
                return strReturn;
            }
        }
        #endregion

        #region Object转换为LocalTime
        /// <summary>
        /// Object转换为LocalTime
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        /// <returns></returns>
        public static string ToLocalTime(this object obj, string strFmt, string strReturn)
        {
            if (Convert.IsDBNull(obj) || obj == null)
            {
                return strReturn;
            }
            else
            {
                DateTime dt;
                if (DateTime.TryParse(obj.ToString(), out dt))
                {
                    dt = dt.ToLocalTime();
                    if (dt.Year > 1900)
                    {
                        return dt.ToString(strFmt);
                    }
                }
                return strReturn;
            }
        }
        #endregion

        #region 去掉"<"和">"之间HTML代码
        /// <summary>
        /// 去掉&quot;&lt;&quot;和&quot;&gt;&quot;之间HTML代码
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        /// <param name="str">要过滤的字符串</param>
        /// <returns></returns>
        public static string RemoveHtml(this string str)
        {
            Regex re = new Regex(@"<[^>]*>");
            str = re.Replace(str, "");
            str = str.Replace("&nbsp;", "");
            str = str.Replace(" ", "");
            str = str.Replace("　", "");
            return str;
        }
        #endregion

        #region 回车字符替换为<br>
        /// <summary>
        /// 回车字符替换<br>
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        /// <param name="str">要替换的字符串</param>
        /// <returns></returns>
        public static string TextareaToBr(this string str)
        {
            str = str.Replace("\n", "<br/>");
            return str;
        }
        #endregion

        #region <br>替换回车字符
        /// <summary>
        /// <br>替换回车字符
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        /// <param name="str">要替换的字符串</param>
        /// <returns></returns>
        public static string BrToTextarea(this string str)
        {
            str = str.Replace("<br/>", "\n");
            str = str.Replace("<br>", "\n");
            return str;
        }
        #endregion

        #region 取部分字符
        /// <summary>
        ///  取部分字符,len为0则返回原字符
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        /// <param name="str">要截取的字符串</param>
        /// <param name="len">截取长度</param>
        /// <returns></returns>
        public static string GetPartStr(this string str, int len)
        {
            if (len == 0)
            {
                return str;
            }
            else
            {
                if (!string.IsNullOrEmpty(str))
                {
                    int t = 0;
                    int n = 0;
                    string strTemp = "";
                    foreach (char c in str)
                    {
                        n++;
                        if (Convert.ToInt32(c) < 0 || Convert.ToInt32(c) > 255)
                        {
                            t = t + 2;
                        }
                        else
                        {
                            t = t + 1;
                        }
                        if (t > len)
                        {
                            strTemp = str.Substring(0, n - 1) + "...";
                            break;
                        }
                        else
                        {
                            strTemp = str;
                        }
                    }
                    return strTemp;
                }
                else
                {
                    return "";
                }
            }
        }
        #endregion

        #region 取隐藏字符
        /// <summary>
        ///  取隐藏字符
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        /// <returns></returns>
        public static string GetHideStr(this string str, int startLen, int endLen)
        {
            if (!string.IsNullOrEmpty(str))
            {
                int len = str.Length;
                if (len >= 2)
                {
                    if (startLen > len) startLen = 1;
                    if (endLen > len) endLen = 1;
                    string strStart = str.Substring(0, startLen);
                    string strEnd = str.Substring(len - endLen, endLen);
                    return strStart + "***" + strEnd;
                }
                else
                {
                    return str + "***" + str;
                }
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region 取第一个字符
        /// <summary>
        ///  取第一个字符
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        /// <returns></returns>
        public static string GetFirstStr(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                int len = str.Length;
                string strStart = str.Substring(0, 1);
                return strStart;
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region 泛型转字符串
        /// <summary>
        /// 泛型转字符串
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string ListToStr<T>(this List<T> list)
        {
            return string.Join<T>(",", list);
        }
        /// <summary>
        /// 泛型转字符串
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string ListToStr<T>(this List<T> list, string separator)
        {
            return string.Join<T>(separator, list);
        }
        #endregion

        #region 字符串数组转字符串
        /// <summary>
        /// 字符串数组转字符串
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static string ArrayToStr(this string[] arr)
        {
            return string.Join(",",arr);
        }
        /// <summary>
        /// 字符串数组转字符串
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static string ArrayToStr(this string[] arr, string separator)
        {
            return string.Join(separator, arr);
        }
        #endregion

        #region xml转字符串
        /// <summary>
        /// xml转字符串
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static string XmlToStr(this XmlElement xml, char separator)
        {
            StringBuilder str = new StringBuilder();
            if (xml != null)
            {
                foreach (XmlNode item in xml.ChildNodes)
                {
                    str.AppendFormat("{0}{1}", item.InnerText, separator);
                }
            }
            return str.ToString().TrimEnd(separator);
        }
        #endregion

        #region xml转list
        /// <summary>
        /// xml转list
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static List<string> XmlToList(this XmlElement xml)
        {
            List<string> list = new List<string>();
            if (xml != null)
            {
                foreach (XmlNode item in xml.ChildNodes)
                {
                    list.Add(item.InnerText);
                }
            }
            return list;
        }
        #endregion

        #region 取几分钟/几小时/几天/几月/几年前
        /// <summary>
        /// 取时间
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static string GetTimeAgo(this DateTime dt)
        {
            string returnStr = "";
            if (dt.Year > DateTime.Now.Year - 1)
            {
                TimeSpan ts = DateTime.Now - dt;
                int year = 0;
                int month = 0;
                int day = ts.TotalDays.ToInt();
                int hour = ts.TotalHours.ToInt();
                int min = ts.TotalMinutes.ToInt();
                int sec = ts.TotalSeconds.ToInt();
                if (sec == 0)
                {
                    returnStr = "刚刚";
                }
                else if (sec > 0 && sec < 60)
                {
                    returnStr = sec + "秒前";
                }
                else
                {
                    if (min > 0 && min < 60)
                    {
                        returnStr = min + "分钟前";
                    }
                    else
                    {
                        if (hour > 0 && hour < 24)
                        {
                            returnStr = hour + "小时前";
                        }
                        else
                        {
                            if (day > 0 && day < 30)
                            {
                                returnStr = day + "天前";
                            }
                            else if (day >= 30 && day < 365)
                            {
                                month = (day / 30).ToInt();
                                returnStr = month + "月前";
                            }
                            else
                            {
                                year = (day / 365).ToInt();
                                returnStr = year + "年前";
                            }
                        }
                    }
                }
            }
            return returnStr;
        }
        #endregion

        public static string GetTimeAfter(this DateTime dt)
        {
            string returnStr = "";
            TimeSpan ts = dt - DateTime.Now;
            int year = 0;
            int month = 0;
            int day = ts.TotalDays.ToInt() - 1;
            int hour = ts.TotalHours.ToInt();
            int min = ts.TotalMinutes.ToInt();
            int sec = ts.TotalSeconds.ToInt();
            if (sec == 0)
            {
                returnStr = "刚刚";
            }
            else if (sec > 0 && sec < 60)
            {
                returnStr = sec + "秒后";
            }
            else
            {
                if (min > 0 && min < 60)
                {
                    returnStr = min + "分钟后";
                }
                else
                {
                    if (hour > 0 && hour < 24)
                    {
                        returnStr = hour + "小时后";
                    }
                    else
                    {
                        if (day > 0 && day < 30)
                        {
                            returnStr = day + "天后";
                        }
                        else if (day >= 30 && day < 365)
                        {
                            month = (day / 30).ToInt();
                            returnStr = month + "月后";
                        }
                        else
                        {
                            year = (day / 365).ToInt();
                            returnStr = year + "年后";
                        }
                    }
                }
            }
            return returnStr;
        }

        #region 取几分钟/几小时/几天/几月/几年
        /// <summary>
        /// 取时间
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static int GetTimeLast(this DateTime dt, string type)
        {
            int returnVal = 0;
            TimeSpan ts = dt - DateTime.Now;
            switch (type)
            {
                case "day":
                    returnVal = ts.TotalDays.ToInt();
                    break;
                default:
                    break;
            }
            return returnVal;
        }
        #endregion

        #region 取先生/女士
        /// <summary>
        ///  取先生/女士
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        /// <returns></returns>
        public static string GetMrMs(this int sex)
        {
            if (sex == EnumList.Sex.男.ToInt())
            {
                return "先生";
            }
            else if (sex == EnumList.Sex.女.ToInt())
            {
                return "女士";
            }
            else
            {
                return "同学";
            }
        }
        #endregion

    }
}
