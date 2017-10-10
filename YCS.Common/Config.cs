using InsideSystem.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Xml;
using drawing = System.Drawing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.DirectoryServices;
using Microsoft.Web.Administration;
using System.Drawing;
using Microsoft.VisualBasic;

namespace YCS.Common
{
    /// <summary>
    /// 通用类
    /// Create by Jimy
    /// </summary>
    public class Config
    {
        #region 加密key
        /// <summary>
        /// 邮箱密码加密Key
        /// </summary>
        public const string EmailPassKey = "vfyuJNrl";

        /// <summary>
        /// 管理员ID加密Key
        /// </summary>
        public const string AdminIdKey = "LXqmeCCz";

        /// <summary>
        /// 记住密码加密Key
        /// </summary>
        public const string RememberPassKey = "aawRjxj2";

        #endregion
        #region 模块配置
        /// <summary>
        /// 单页面
        /// </summary>
        public const int SysSinglePageMouldId = 1;
        /// <summary>
        /// 文章
        /// </summary>
        public const int SysArticleMouldId = 2;
        /// <summary>
        /// 包装商品
        /// </summary>
        public const int SysProductMouldId = 3;
        /// <summary>
        /// 外链
        /// </summary>
        public const int SysOuterLinkMouldId = 4;
        #endregion
        #region 后台配置
        /// <summary>
        /// 超级管理员ID
        /// </summary>
        public const int SystemAdminId = 1;
        /// <summary>
        /// 印想经销商ID
        /// </summary>
        public const string SystemDistributorId = "1";
        /// <summary>
        /// 文件上传目录路径
        /// </summary>
        public const string FileUploadPath = "/Files/";
        /// <summary>
        /// 系统名称
        /// </summary>
        public const string SystemName = "奥印网管理平台";
        /// <summary>
        /// 版权所有
        /// </summary>
        public const string Copyright = "Copyright &copy; 2017 OINONE Tech. All Rights Reserved. Powered By OinOne.com";

        #endregion
        #region 默认配置
        /// <summary>
        /// 默认头像
        /// </summary>
        public const string DefaultHeadPic = "/content/images/default-head.png";
        /// <summary>
        /// 默认发货省份
        /// </summary>
        public static int DefaultProvinceID
        {
            get
            {
                return ConfigurationManager.AppSettings["DefaultProvinceID"].ToInt();
            }
        }
        /// <summary>
        /// 默认发货城市
        /// </summary>
        public static int DefaultCityID
        {
            get
            {
                return ConfigurationManager.AppSettings["DefaultCityID"].ToInt();
            }
        }
        #endregion
        #region 配送区域配置
        /// <summary>
        /// 可配送省份,多个用","分隔
        /// </summary>
        public static string DeliverProvinces
        {
            get
            {
                return ConfigurationManager.AppSettings["DeliverProvinces"].ToString();
            }
        }
        /// <summary>
        /// 可配送城市,多个用","分隔
        /// </summary>
        public static string DeliverCitys
        {
            get
            {
                return ConfigurationManager.AppSettings["DeliverCitys"].ToString();
            }
        }
        #endregion

        #region 数据库配置
        #region 数据库连接字符串
        /// <summary>
        /// 数据库连接字符是否加密码,针对MSSQL数据库,0-公开方式,1-加密方式
        /// </summary>
        public static bool IsEncrypt
        {
            get { return ConfigurationManager.AppSettings["IsEncrypt"].ToString() == "1"; }
        }
        /// <summary>
        /// Sql数据库连接字符串
        /// </summary>
        public static string SqlConnStr
        {
            get
            {
                if (IsEncrypt)
                {
                    return ConnectionInfo.DecryptDBConnectionString(ConfigurationManager.ConnectionStrings["SqlConnStr"].ToString());
                }
                else
                {
                    return ConfigurationManager.ConnectionStrings["SqlConnStr"].ToString();
                }
            }
        }
        #endregion
        #region 数据库接口实例化
        private static SqlHelper _sqlhelper;
        /// <summary>
        /// SQL数据库连接实例化
        /// </summary>
        /// <returns></returns>
        public static SqlHelper SqlHelper()
        {
            if (_sqlhelper == null)
            {
                _sqlhelper = new SqlHelper(Config.SqlConnStr);
            }
            return _sqlhelper;
        }
        #endregion
        #endregion
        #region 楼层配置
        /// <summary>
        /// 首页楼层分类显示的个数
        /// </summary>
        public static int IndexCategoryCount
        {
            get { return ConfigurationManager.AppSettings["IndexCategoryCount"].ToInt(); }
        }
        #endregion
        #region 栏目配置
        /// <summary>
        /// 底部栏目
        /// </summary>
        public static int BottomClassId
        {
            get { return ConfigurationManager.AppSettings["BottomClassId"].ToInt(); }
        }
        /// <summary>
        /// 首页广告位
        /// </summary>
        public static int IndexBanner
        {
            get { return ConfigurationManager.AppSettings["IndexBanner"].ToInt(); }
        }
        /// <summary>
        /// 促销广告位
        /// </summary>
        public static int SalesBanner
        {
            get { return ConfigurationManager.AppSettings["SalesBanner"].ToInt(); }
        }
        #endregion
        #region 字典配置
        #endregion
        #region URL配置
        /// <summary>
        /// 网站URL地址
        /// </summary>
        public static string SiteUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["SiteUrl"].ToString();
            }
        }
        #endregion
        #region CDN配置
        /// <summary>
        /// 是否启用CDN
        /// </summary>
        public static bool IsEnableCDN
        {
            get
            {
                return ConfigurationManager.AppSettings["IsEnableCDN"].ToInt() == 1;
            }
        }
        /// <summary>
        /// 是否启用JCSS/JS合并及压缩
        /// </summary>
        public static bool IsEnableOptimizations
        {
            get
            {
                return ConfigurationManager.AppSettings["IsEnableOptimizations"].ToInt() == 1;
            }
        }
        /// <summary>
        /// 资源版本
        /// </summary>
        public static string ResourceVersion
        {
            get
            {
                return ConfigurationManager.AppSettings["ResourceVersion"].ToString();
            }
        }
        /// <summary>
        /// CDN请求URL地址,不包含末尾的"/"
        /// </summary>
        public static string CDNRequestURL
        {
            get
            {
                if (IsEnableCDN)
                {
                    return ConfigurationManager.AppSettings["CDNRequestURL"].ToString();
                }
                else
                {
                    return "";
                }
            }
        }
        /// <summary>
        /// CDN产品图片请求URL地址,不包含末尾的"/"
        /// </summary>
        public static string CDNProductRequestURL
        {
            get
            {
                if (IsEnableCDN)
                {
                    return ConfigurationManager.AppSettings["CDNProductRequestURL"].ToString();
                }
                else
                {
                    return "";
                }
            }
        }
        #endregion

        #region 验证码配置
        /// <summary>
        /// 验证码重发间隔时间(秒)
        /// </summary>
        public static int ReSendTime
        {
            get
            {
                return ConfigurationManager.AppSettings["ReSendTime"].ToInt();
            }
        }
        /// <summary>
        /// 验证码有效时间(分)
        /// </summary>
        public static int ValidTime
        {
            get
            {
                return ConfigurationManager.AppSettings["ValidTime"].ToInt();
            }
        }
        #endregion
        #region 登录配置
        /// <summary>
        /// 登录过期时间(分钟)
        /// </summary>
        public static int LoginExpiredTime
        {
            get
            {
                return ConfigurationManager.AppSettings["LoginExpiredTime"].ToInt();
            }
        }
        /// <summary>
        /// 显示验证码登录失败次数
        /// </summary>
        public static int VerifyLoginFailedCount
        {
            get
            {
                return ConfigurationManager.AppSettings["VerifyLoginFailedCount"].ToInt();
            }
        }
        /// <summary>
        /// 计算登录失败次数时间段(分钟)
        /// </summary>
        public static int CountLoginFailedTime
        {
            get
            {
                return ConfigurationManager.AppSettings["CountLoginFailedTime"].ToInt();
            }
        }
        /// <summary>
        /// 锁定帐号登录失败次数
        /// </summary>
        public static int LockLoginFailedCount
        {
            get
            {
                return ConfigurationManager.AppSettings["LockLoginFailedCount"].ToInt();
            }
        }
        #endregion
        #region 其他配置
        /// <summary>
        /// 可添加收货地址数量
        /// </summary>
        public static int UserAddrCount
        {
            get
            {
                return ConfigurationManager.AppSettings["UserAddrCount"].ToInt();
            }
        }
        #endregion
        #region 编辑器配置
        /// <summary>
        /// 编辑器地址
        /// </summary>
        public static string EditorDomain
        {
            get
            {
                return ConfigurationManager.AppSettings["EditorDomain"].ToString();
            }
        }
        /// <summary>
        /// 进入编辑器URL地址
        /// </summary>
        public static string EditorEnterUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["EditorEnterUrl"].ToString();
            }
        }
        /// <summary>
        /// 等待製作PDF URL地址
        /// </summary>
        public static string EditorPrepareGeneratePdfUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["EditorPrepareGeneratePdfUrl"].ToString();
            }
        }
        /// <summary>
        /// 製作PDF URL地址
        /// </summary>
        public static string EditorGeneratePdfUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["EditorGeneratePdfUrl"].ToString();
            }
        }
        #endregion

        #region 阿里雲

        /// <summary>
        /// STS預設有效時間(秒)[Min~Max: 900~3600]
        /// </summary>
        public static long STSDefaultDurationSeconds
        {
            get
            {
                return long.Parse(ConfigurationManager.AppSettings["STSDefaultDurationSeconds"]);
            }
        }
        /// <summary>
        /// STS延長有效時間(秒)
        /// </summary>
        public static long STSExtendDurationSeconds
        {
            get
            {
                return long.Parse(ConfigurationManager.AppSettings["STSExtendDurationSeconds"]);
            }
        }
        #endregion // 阿里雲

        #region 订单配置
        /// <summary>
        /// 未支付订单过期时间(小时)
        /// </summary>
        public static decimal OrderNoPayTime
        {
            get
            {
                return ConfigurationManager.AppSettings["OrderNoPayTime"].ToDecimal();
            }
        }
        /// <summary>
        /// 订单自动确认收货时间(天)
        /// </summary>
        public static int OrderMotionDay
        {
            get
            {
                return ConfigurationManager.AppSettings["OrderMotionDay"].ToInt();
            }
        }
        #endregion
        #region  台湾EC对接AIPS接口配置信息
        /// <summary>
        /// 服务器地址
        /// </summary>
        public static string TwEcIpAddress
        {
            get { return ConfigurationManager.AppSettings["TwEcIpAddress"].ToString(); }
        }
        /// <summary>
        /// 資料交換服務將會自行取得會員資料，並新增或更新至AIPS
        /// </summary>
        public static string MemberUrl
        {
            get { return ConfigurationManager.AppSettings["MemberUrl"].ToString(); }
        }
        /// <summary>
        /// 取得AIPS會員相關資訊ForEC
        /// </summary>
        public static string ForEcUrl
        {
            get { return ConfigurationManager.AppSettings["ForEcUrl"].ToString(); }
        }
        /// <summary>
        /// 觸發EC後端服務 call AIPS，EC訂單建立與更新同步機制
        /// </summary>
        public static string OrderTriggerPushUrl
        {
            get { return ConfigurationManager.AppSettings["OrderTriggerPushUrl"].ToString(); }
        }
        /// <summary>
        /// 觸發EC後端服務Call AIPS，EC付款儲值機制
        /// </summary>
        public static string RechargeTriggerPushUrl
        {
            get { return ConfigurationManager.AppSettings["RechargeTriggerPushUrl"].ToString(); }
        }

        #endregion


        #region 根据条件输出字符
        /// <summary>
        /// 根据条件输出字符,strParA与strParB相等,则返回str1,否则返回str2
        /// </summary>
        /// <param name="strParA">要比较的参数</param>
        /// <param name="strParB">要比较的参数</param>
        /// <param name="str1">要返回的字符</param>
        /// <param name="str2">要返回的字符</param>
        /// <returns></returns>
        public static string ShowStr(string strParA, string strParB, string str1, string str2)
        {
            if (strParA == strParB)
            {
                return str1;
            }
            else
            {
                return str2;
            }
        }
        #endregion
        #region 弹出信息跳转到指定地址
        /// <summary>
        /// 弹出信息跳转到指定地址
        /// </summary>
        /// <param name="Msg">要弹出的信息</param>
        /// <param name="Link">要跳转的链接地址</param>
        public static string MsgGotoUrl(string Msg, string Link)
        {
            StringBuilder strReturn = new StringBuilder();
            strReturn.Append("<script>alert('" + Msg + "');location.href='" + Link + "';</script>");
            return strReturn.ToString();
        }
        #endregion
        #region 弹出信息并返回上一页
        /// <summary>
        /// 弹出信息并返回上一页
        /// </summary>
        /// <param name="Msg">要弹出的信息</param>
        public static string MsgGoBack(string Msg)
        {
            StringBuilder strReturn = new StringBuilder();
            strReturn.Append("<script>alert('" + Msg + "');history.go(-1);</script>");
            return strReturn.ToString();
        }
        #endregion
        #region 弹出信息并关闭窗口
        /// <summary>
        /// 弹出信息并关闭窗口
        /// </summary>
        /// <param name="Msg">要弹出的信息</param>
        public static string MsgClose(string Msg)
        {
            StringBuilder strReturn = new StringBuilder();
            strReturn.Append("<script>alert('" + Msg + "');window.close();</script>");
            return strReturn.ToString();
        }
        #endregion
        #region 弹出信息并刷新父窗口
        /// <summary>
        /// 弹出信息并刷新父窗口
        /// </summary>
        /// <param name="Msg">要弹出的信息</param>
        public static string MsgReloadQT(string Msg)
        {
            StringBuilder strReturn = new StringBuilder();
            strReturn.Append("<script>alert('" + Msg + "');parent.window.location.href=parent.document.URL;</script>");
            return strReturn.ToString();
        }
        /// <summary>
        /// 弹出信息并刷新父窗口
        /// </summary>
        /// <param name="Msg">要弹出的信息</param>
        public static string MsgReloadHT(string Msg)
        {
            StringBuilder strReturn = new StringBuilder();
            strReturn.Append("<script>alert('" + Msg + "');parent.window.location.href=parent.document.URL;</script>");
            return strReturn.ToString();
        }
        /// <summary>
        /// 弹出信息并刷新父窗口
        /// </summary>
        /// <param name="Msg">要弹出的信息</param>
        public static string MsgReload(string Msg)
        {
            StringBuilder strReturn = new StringBuilder();
            strReturn.Append("<!DOCTYPE html>\n");
            strReturn.Append("<html>\n");
            strReturn.Append("<head>\n");
            strReturn.Append("<script src=\"/Content/jQuery/jquery-1.11.1.js\" type=\"text/javascript\"></script>\n");
            strReturn.Append("<script src=\"/Content/layer/layer.js\" type=\"text/javascript\"></script>\n");
            strReturn.Append("<script src=\"/Content/Common/lib.js\" type=\"text/javascript\"></script>\n");
            strReturn.Append("</head>\n");
            strReturn.Append("<body>\n");
            strReturn.Append("<script  type=\"text/javascript\">");
            strReturn.Append("parent.alert('" + Msg + "');\n");
            strReturn.Append("lib.reloadIframe();\n");
            strReturn.Append("lib.closeFrameDialog();\n");
            strReturn.Append("</script>");
            strReturn.Append("</body>\n");
            strReturn.Append("</html>\n");
            return strReturn.ToString();
        }
        #endregion
        #region 弹窗子页刷新父页
        /// <summary>
        /// 弹窗子页刷新父页
        /// </summary>
        /// <param name="Msg">要弹出的信息</param>
        public static string OpenReload()
        {
            StringBuilder strReturn = new StringBuilder();
            strReturn.Append("<script>window.opener.location.reload();self.close();</script>");
            return strReturn.ToString();
        }
        #endregion
        #region 显示信息并终止程序
        /// <summary>
        /// 显示信息并终止程序
        /// </summary>
        /// <param name="str">显示的信息</param>
        public static void ShowEnd(string str)
        {

            HttpContext.Current.Response.Write(str);
            HttpContext.Current.Response.End();
        }
        #endregion
        #region 订单号
        /// <summary>
        /// 取订单号,日期+随机码
        /// </summary>
        /// <remarks>订单类型</remarks>
        /// <returns></returns>
        public static string GetOrderNo(int type)
        {
            //Random rad = new Random(GetRandomSeed());
            //int radno = rad.Next(1000, 9999);
            //return DateTime.Now.ToString("yyyyMMddHHmmss") +  radno.ToString();

            Random rad = new Random(GetRandomSeed());
            int radno = rad.Next(100000, 999999);
            return DateTime.Now.ToString("MMdd") + type + radno.ToString();
        }
        #endregion
        #region 数字前面加字符0
        /// <summary>
        /// 数字前面加字符0
        /// </summary>
        /// <param name="n">要检查的数字</param>
        /// <param name="count">要显示的字符位数</param>
        /// <returns></returns>
        public static string ShowZero(int n, int count)
        {
            int len = n.ToString().Length;
            StringBuilder strReturn = new StringBuilder();
            if (count > len)
            {
                for (int i = 0; i < (count - len); i++)
                {
                    strReturn.Append("0");
                }
            }
            strReturn.Append(n.ToString());
            return strReturn.ToString();
        }
        #endregion
        #region 显示flash或图片
        /// <summary>
        /// 显示flash或图片
        /// </summary>
        /// <param name="path">flash或图片的路径</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns></returns>
        public static string Flash(string path, int width, int height)
        {
            StringBuilder fstr = new StringBuilder("");
            string FileExt = path.Substring((path.Length - 3), 3);
            if (FileExt.ToLower() == "swf")
            {
                fstr.Append("<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0\" width=\"" + width + "\" height=\"" + height + "\">\n");
                fstr.Append("<param name=\"movie\" value=\"" + path + "\">\n");
                fstr.Append("<param name=\"quality\" value=\"high\">\n");
                fstr.Append("<param name=\"wmode\" value=\"transparent\">\n");
                fstr.Append("<embed src=\"" + path + "\" wmode=\"transparent\" quality=\"high\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" type=\"application/x-shockwave-flash\" width=\"" + width + "\" height=\"" + height + "\"></embed>\n");
                fstr.Append("</object>\n");
            }
            else
            {
                fstr.Append("<img src=\"" + path + "\" style=\"border;0px;\" width=\"" + width + "\" height=\"" + height + "\"/>");
            }
            return fstr.ToString();
        }
        #endregion
        #region 字符加解密
        /// <summary>
        /// 字符加密
        /// </summary>
        /// <param name="str">要加密的字符</param>
        /// <returns></returns>
        public static string Encrypt(string str)
        {
            return ConnectionInfo.EncryptDBConnectionString(str);
        }
        /// <summary>
        /// 字符解密
        /// </summary>
        /// <param name="str">要解密的字符</param>
        /// <returns></returns>
        public static string Decrypt(string str)
        {
            return ConnectionInfo.DecryptDBConnectionString(str);
        }
        #endregion
        #region DES加密解密字符串
        //默认密钥向量
        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串 </returns>
        public static string EncryptDES(string encryptString, string encryptKey)
        {
            byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));//转换为字节
            byte[] rgbIV = Keys;
            byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
            DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();//实例化数据加密标准
            MemoryStream mStream = new MemoryStream();//实例化内存流
            //将数据流链接到加密转换的流
            CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Convert.ToBase64String(mStream.ToArray());
        }

        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串</returns>
        public static string DecryptDES(string decryptString, string decryptKey)
        {
            byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
            byte[] rgbIV = Keys;
            byte[] inputByteArray = Convert.FromBase64String(decryptString);
            DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Encoding.UTF8.GetString(mStream.ToArray());
        }
        #endregion
        #region 错误日志
        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="ex">Exception对象名</param>
        public static void Err(Exception ex)
        {
            try
            {
                IOHelper.FolderCheck(Config.GetMapPath("~/_err/"));
                string path = Config.GetMapPath("~/_err/err_" + DateTime.Now.ToString("yyyy-MM-dd") + ".err");
                StringBuilder strErrContent = new StringBuilder();
                strErrContent.Append("时间:" + DateTime.Now.ToString() + "\r\n");
                strErrContent.Append("错误描述:" + ex.Message + "\r\n");
                strErrContent.Append("引发错误的实例:" + ex.InnerException.ToString2() + "\r\n");
                strErrContent.Append("堆栈描述:\n" + ex.StackTrace + "\r\n");
                strErrContent.Append("错误源:" + ex.Source + "\r\n");
                strErrContent.Append("引发错误的方法:" + ex.TargetSite + "\r\n");
                strErrContent.Append("引发错误的路径:" + HttpContext.Current.Request.Url.ToString2() + "\r\n");
                strErrContent.Append("页面来源:" + HttpContext.Current.Request.UrlReferrer.ToString2() + "\r\n");
                strErrContent.Append("客户端信息:" + HttpContext.Current.Request.UserAgent + "\r\n");
                strErrContent.Append("客户端IP:" + HttpContext.Current.Request.UserHostAddress + "\r\n");
                strErrContent.Append("---------------------------------------------------------------------------------------------------------------\r\n");
                File.AppendAllText(path, strErrContent.ToString(), Encoding.GetEncoding("utf-8"));


            }
            catch
            { }
        }
        public static void Err(string className, string strInfo)
        {
            try
            {
                IOHelper.FolderCheck(Config.GetMapPath("~/_err/"));
                string path = Config.GetMapPath("~/_err/err_" + DateTime.Now.ToString("yyyy-MM-dd") + ".err");
                StringBuilder strLogContent = new StringBuilder();
                strLogContent.Append("时间:" + DateTime.Now.ToString() + "\r\n");
                strLogContent.Append(className + ":" + strInfo + "\r\n");
                strLogContent.Append("--------------------------------------------------------------------------------------------------------------\r\n");
                File.AppendAllText(path, strLogContent.ToString(), Encoding.GetEncoding("utf-8"));
            }
            catch
            { }
        }
        #endregion
        #region 生成日志
        /// <summary>
        /// 生成日志
        /// </summary>
        public static void Log(string strLog)
        {
            try
            {
                IOHelper.FolderCheck(Config.GetMapPath("~/_log/"));
                string path = Config.GetMapPath("~/_log/log_" + DateTime.Now.ToString("yyyy-MM-dd") + ".log");
                StringBuilder strLogContent = new StringBuilder();
                strLogContent.Append("时间:" + DateTime.Now.ToString() + "\r\n");
                strLogContent.Append("日志:" + strLog + "\r\n");
                strLogContent.Append("--------------------------------------------------------------------------------------------------------------\r\n");
                File.AppendAllText(path, strLogContent.ToString(), Encoding.GetEncoding("utf-8"));
            }
            catch
            { }
        }
        #endregion
        #region 调试日志
        /// <summary>
        /// 调试日志
        /// </summary>
        public static void Debug(string className, string strInfo)
        {
            try
            {
                IOHelper.FolderCheck(Config.GetMapPath("~/_debug/"));
                string path = Config.GetMapPath("~/_debug/debug_" + DateTime.Now.ToString("yyyy-MM-dd") + ".debug");
                StringBuilder strLogContent = new StringBuilder();
                strLogContent.Append("时间:" + DateTime.Now.ToString() + "\r\n");
                strLogContent.Append(className + ":" + strInfo + "\r\n");
                strLogContent.Append("--------------------------------------------------------------------------------------------------------------\r\n");
                File.AppendAllText(path, strLogContent.ToString(), Encoding.GetEncoding("utf-8"));
            }
            catch
            { }
        }
        #endregion
        #region Request
        /// <summary>
        /// 过滤POST或GET方式提交的值
        /// </summary>
        /// <param name="obj">Request.Form或Request.QueryString对象</param>
        /// <returns></returns>
        public static string Request(object obj, string strDefaultVal)
        {
            if (obj == null)
            {
                return strDefaultVal;
            }
            else
            {
                string strTemp = (obj.ToString());

                ////添加IllegalCharFilterAttribute作全局字符串处理，by michael in 2016-12-16 16-20
                //strTemp = SqlFilterUtil.IllegalFilter(strTemp);
                if (String.IsNullOrEmpty(strTemp))
                {
                    strTemp = strDefaultVal;
                }
                return strTemp.Trim();
            }
        }
        #endregion
        #region Request Int
        /// <summary>
        /// 过滤POST或GET方式提交的值是否整型数字
        /// </summary>
        /// <param name="obj">Request.Form或Request.QueryString对象</param>
        public static int RequestInt(object obj, int intDefaultVal)
        {
            if (obj == null)
            {
                return intDefaultVal;
            }
            else
            {
                string strTemp = obj.ToString();
                int result;
                if (int.TryParse(strTemp, out result))
                {
                    return result;
                }
                else
                {
                    return intDefaultVal;
                }
            }
        }
        #endregion
        #region Request Decimal
        /// <summary>
        /// 过滤POST或GET方式提交的值是否十进制数字
        /// </summary>
        /// <param name="obj">Request.Form或Request.QueryString对象</param>
        public static decimal RequestDecimal(object obj, decimal decDefaultVal)
        {
            if (obj == null)
            {
                return decDefaultVal;
            }
            else
            {
                string strTemp = obj.ToString();
                decimal result;
                if (decimal.TryParse(strTemp, out result))
                {
                    return result;
                }
                else
                {
                    return decDefaultVal;
                }
            }
        }
        #endregion
        #region Request Double
        /// <summary>
        /// 过滤POST或GET方式提交的值是否为double
        /// </summary>
        /// <param name="obj">Request.Form或Request.QueryString对象</param>
        public static double RequestDouble(object obj, double doubleDefaultVal)
        {
            if (obj == null)
            {
                return doubleDefaultVal;
            }
            else
            {
                string strTemp = obj.ToString();
                double result;
                if (double.TryParse(strTemp, out result))
                {
                    return result;
                }
                else
                {
                    return doubleDefaultVal;
                }
            }
        }
        #endregion
        #region Request Float
        /// <summary>
        /// 过滤POST或GET方式提交的值是否为float
        /// </summary>
        /// <param name="obj">Request.Form或Request.QueryString对象</param>
        public static float RequestFloat(object obj, float doubleDefaultVal)
        {
            if (obj == null)
            {
                return doubleDefaultVal;
            }
            else
            {
                string strTemp = obj.ToString();
                float result;
                if (float.TryParse(strTemp, out result))
                {
                    return result;
                }
                else
                {
                    return doubleDefaultVal;
                }
            }
        }
        #endregion
        #region Request long
        /// <summary>
        /// 过滤POST或GET方式提交的值是否为long
        /// </summary>
        /// <param name="obj">Request.Form或Request.QueryString对象</param>
        public static long RequestLong(object obj, long doubleDefaultVal)
        {
            if (obj == null)
            {
                return doubleDefaultVal;
            }
            else
            {
                string strTemp = obj.ToString();
                long result;
                if (long.TryParse(strTemp, out result))
                {
                    return result;
                }
                else
                {
                    return doubleDefaultVal;
                }
            }
        }
        #endregion
        #region 字符编码转换
        public static string GB2312ToUtf8(string gb2312String)
        {
            Encoding fromEncoding = Encoding.GetEncoding("gb2312");
            Encoding toEncoding = Encoding.UTF8;
            return EncodingConvert(gb2312String, fromEncoding, toEncoding);
        }

        public static string Utf8ToGB2312(string utf8String)
        {
            Encoding fromEncoding = Encoding.UTF8;
            Encoding toEncoding = Encoding.GetEncoding("gb2312");
            return EncodingConvert(utf8String, fromEncoding, toEncoding);
        }

        public static string EncodingConvert(string fromString, Encoding fromEncoding, Encoding toEncoding)
        {
            byte[] fromBytes = fromEncoding.GetBytes(fromString);
            byte[] toBytes = Encoding.Convert(fromEncoding, toEncoding, fromBytes);

            string toString = toEncoding.GetString(toBytes);
            return toString;
        }
        /// <summary>
        /// 汉字转换为Unicode编码
        /// </summary>
        /// <param name="str">要编码的汉字字符串</param>
        /// <returns>Unicode编码的的字符串</returns>
        public static string ToUnicode(string str)
        {
            byte[] bts = Encoding.Unicode.GetBytes(str);
            string r = "";
            for (int i = 0; i < bts.Length; i += 2) r += "\\u" + bts[i + 1].ToString("x").PadLeft(2, '0') + bts[i].ToString("x").PadLeft(2, '0');
            return r;
        }
        /// <summary>
        /// 将Unicode编码转换为汉字字符串
        /// </summary>
        /// <param name="str">Unicode编码字符串</param>
        /// <returns>汉字字符串</returns>
        public static string ToGB2312(string str)
        {
            string r = "";
            MatchCollection mc = Regex.Matches(str, @"\\u([\w]{2})([\w]{2})", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            byte[] bts = new byte[2];
            foreach (Match m in mc)
            {
                bts[0] = (byte)int.Parse(m.Groups[2].Value, NumberStyles.HexNumber);
                bts[1] = (byte)int.Parse(m.Groups[1].Value, NumberStyles.HexNumber);
                r += Encoding.Unicode.GetString(bts);
            }
            return r;
        }
        /// <summary>
        /// Base64解码
        /// </summary>
        /// <param name="strBase64"></param>
        /// <returns></returns>
        public static string Base64ToString(string strBase64)
        {
            byte[] byt = Convert.FromBase64String(strBase64);
            return Encoding.UTF8.GetString(byt);
        }
        /// <summary>
        /// Base64编码
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string StringToBase64(string s)
        {
            byte[] byt = Encoding.UTF8.GetBytes(s);
            return Convert.ToBase64String(byt);
        }
        #endregion
        #region 随机种子
        /// <summary>
        /// 随机种子
        /// </summary>
        /// <returns></returns>
        public static int GetRandomSeed()
        {
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }
        #endregion
        #region 随机字符串
        /// <summary>
        /// 随机字符串
        /// </summary>
        /// <param name="type">字符类型,0-仅数字,1-仅大写,2仅小写,120-大小写及数字混合</param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string GetRndString(int type, int len)
        {
            string strRnd = "0123456789abcdefghigklmnopqrstuvwxyzABCDEFGHIGKLMNOPQRSTUVWXYZ";
            switch (type)
            {
                case 0:
                    strRnd = "0123456789";
                    break;
                case 1:
                    strRnd = "ABCDEFGHIGKLMNOPQRSTUVWXYZ";
                    break;
                case 2:
                    strRnd = "abcdefghigklmnopqrstuvwxyz";
                    break;
                case 120:
                default:
                    break;
            }
            StringBuilder strReturn = new StringBuilder();
            Random rnd = new Random(GetRandomSeed());
            for (int i = 0; i < len; i++)
            {
                strReturn.Append(strRnd[rnd.Next(0, strRnd.Length)]);
            }
            return strReturn.ToString();
        }
        #endregion
        #region 判断是否移动终端访问
        /// <summary>
        /// 判断是否移动终端访问
        /// </summary>
        /// <returns></returns>
        public static bool IsMobileBrowse()
        {
            bool reVal = false;
            string strUserAgent = HttpContext.Current.Request.UserAgent.ToLower();
            string strUserAgentKey = "nokia,sony,ericsson,mot,samsung,htc,sgh,lg,sharp,sie-,philips,panasonic,alcatel,lenovo,iphone,ipod,blackberry,meizu,android,netfront,symbian,ucweb,windowsce,palm,operamini,operamobi,openwave,nexusone,cldc,midp,wap,mobile";
            string[] arrUserAgentKey = strUserAgentKey.Split(',');
            foreach (string item in arrUserAgentKey)
            {
                if (strUserAgent.IndexOf(item) > -1)
                {
                    reVal = true;
                    break;
                }
            }
            if (reVal == false)
            {
                string[] arrHttpAccept = HttpContext.Current.Request.AcceptTypes;
                string strHttpAccept = arrHttpAccept.ArrayToStr();
                if (strHttpAccept.IndexOf("vnd.wap") > -1)
                {
                    reVal = true;
                }
            }
            return reVal;
        }
        #endregion
        #region 加载百度编辑器UEditor
        /// <summary>
        /// 加载百度编辑器UEditor
        /// </summary>
        /// <param name="strInitContent"></param>
        /// <returns></returns>
        public static string LoadUEditor(string strEditorID, string strInitContent)
        {
            StringBuilder strUEditor = new StringBuilder();
            strUEditor.Append("<textarea id=\"" + strEditorID + "\"  style=\"width: 90%; height: 350px;\">" + strInitContent + "</textarea>\n");
            strUEditor.Append("<script type=\"text/javascript\">\n");
            strUEditor.Append("var ue = UE.getEditor('" + strEditorID + "',{\n");
            strUEditor.Append("textarea:'" + strEditorID + "'\n");
            strUEditor.Append("});\n");
            strUEditor.Append("</script>\n");
            return strUEditor.ToString();
        }
        #endregion
        #region 取枚举属性说明
        /// <summary>
        /// 取枚举属性说明
        /// 使用方法:
        /// Config.GetEnumDesc(typeof(枚举名称),数值)
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumDesc(Type enumType, object value)
        {
            string strName = Enum.GetName(enumType, value);
            if (!string.IsNullOrEmpty(strName))
            {
                FieldInfo fdinfo = enumType.GetField(strName);
                DescriptionAttribute attr = (DescriptionAttribute)Attribute.GetCustomAttribute(fdinfo, typeof(DescriptionAttribute));
                return attr.Description;
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 取枚举属性说明
        /// 使用方法:
        /// Config.GetEnumDesc(typeof(枚举名称),名称)
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumDesc(Type enumType, string name)
        {
            string strName = name;
            if (!string.IsNullOrEmpty(strName))
            {
                FieldInfo fdinfo = enumType.GetField(strName);
                DescriptionAttribute attr = (DescriptionAttribute)Attribute.GetCustomAttribute(fdinfo, typeof(DescriptionAttribute));
                return attr.Description;
            }
            else
            {
                return "";
            }
        }
        #endregion
        #region 取枚举列表
        /// <summary>
        /// 取枚举列表
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static Dictionary<string, int> GetEnumList(Type enumType)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            FieldInfo[] fdinfos = enumType.GetFields();
            foreach (var item in fdinfos)
            {
                if (item.Name != "value__")
                {
                    int val = (int)item.GetValue(item.Name);
                    string desc = GetEnumDesc(enumType, val).RemoveHtml();
                    dict.Add(desc, val);
                }
            }
            return dict;
        }
        #endregion
        #region 取枚举下拉列表
        /// <summary>
        /// 取枚举下拉列表
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static List<SelectListItem> GetEnumSelectList(Type enumType)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            FieldInfo[] fdinfos = enumType.GetFields();
            foreach (var item in fdinfos)
            {
                if (item.Name != "value__")
                {
                    int val = (int)item.GetValue(item.Name);
                    string desc = GetEnumDesc(enumType, val).RemoveHtml();
                    list.Add(new SelectListItem() { Text = desc, Value = val.ToString() });
                }
            }
            return list.OrderBy(f => f.Value).ToList();
        }
        /// <summary>
        /// 取枚举下拉列表
        /// Added by wgd,2017-05-04
        /// </summary>
        /// <param name="enumType">枚举</param>
        /// <param name="value">选中的值</param>
        /// <returns></returns>
        public static List<SelectListItem> GetEnumSelectList(Type enumType, string value)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            FieldInfo[] fdinfos = enumType.GetFields();
            foreach (var item in fdinfos)
            {
                if (item.Name != "value__")
                {
                    int val = (int)item.GetValue(item.Name);
                    string desc = GetEnumDesc(enumType, val).RemoveHtml();
                    list.Add(new SelectListItem()
                    {
                        Text = desc,
                        Value = val.ToString(),
                        Selected = !string.IsNullOrEmpty(value)
                            && value.Equals(val.ToString()) ? true : false
                    });
                }
            }
            return list.OrderBy(f => f.Value).ToList();
        }
        /// <summary>
        /// 取枚举下拉列表
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static List<SelectListItem> GetEnumSelectListForName(Type enumType)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            FieldInfo[] fdinfos = enumType.GetFields();
            foreach (var item in fdinfos)
            {
                if (item.Name != "value__")
                {
                    string desc = GetEnumDesc(enumType, item.Name).RemoveHtml();
                    list.Add(new SelectListItem() { Text = desc, Value = item.Name });
                }
            }
            return list.OrderBy(f => f.Value).ToList();
        }
        #endregion
        #region http接口认证
        /// <summary>
        /// http接口认证
        /// </summary>
        /// <param name="sign"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static bool HttpCert(string sign, string time)
        {
            string key = "lXdBFNDTsD";
            return (key + time).md5().ToLower() == sign;
        }
        #endregion
        #region 取时间戳
        /// <summary>
        /// 取时间戳
        /// </summary>
        /// <returns></returns>
        public static long GetTimestamp()
        {
            return (long)(DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds;
        }
        #endregion
        #region 时间戳转时间格式
        /// <summary>
        /// 时间戳转时间格式
        /// </summary>
        /// <returns></returns>
        public static DateTime TimestampToDateTime(long timestamp)
        {
            return DateTime.Parse("1970-01-01 00:00:00").AddSeconds(timestamp);
        }
        #endregion
        #region 日期时间处理
        /// <summary>
        /// 日期时间处理
        /// </summary>
        /// <param name="strDate"></param>
        /// <returns></returns>
        public static string GetDate(string strDate, string strFmt, string strReturnStr)
        {
            DateTime dt;
            if (DateTime.TryParse(strDate, out dt))
            {
                if (dt.Year > 1900)
                {
                    return dt.ToString(strFmt);
                }
            }
            return strReturnStr;
        }
        #endregion
        #region 取SessionID
        /// <summary>
        /// 取SessionID
        /// </summary>
        /// <returns></returns>
        public static string GetSessionID()
        {
            string strSessionID;
            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get("SessionID");
            if (cookie != null)
            {
                strSessionID = cookie.Value;
                if (string.IsNullOrWhiteSpace(strSessionID))
                {
                    strSessionID = SetSessionID();
                }
            }
            else
            {
                strSessionID = SetSessionID();
            }
            return strSessionID;
        }

        /// <summary>
        ///  设置SessionID
        /// </summary>
        /// <returns></returns>
        private static string SetSessionID()
        {
            string strSessionID = HttpContext.Current.Session.SessionID;
            HttpCookie cookie = new HttpCookie("SessionID");
            cookie.Expires = DateTime.Now.AddYears(1);
            cookie.Value = strSessionID;
            HttpContext.Current.Response.Cookies.Add(cookie);
            return strSessionID;
        }
        #endregion
        #region Cookies处理
        /// <summary>
        /// 获取Cookies
        /// </summary>
        /// <returns></returns>
        public static string GetCookies(string key)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(key);
            if (cookie != null)
            {
                return cookie.Value;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        ///  设置Cookies
        /// </summary>
        /// <returns></returns>
        public static void SetCookies(string key, object value, double day)
        {
            HttpCookie cookie = new HttpCookie(key);
            cookie.Expires = DateTime.Now.AddDays(day);
            cookie.Value = value.ToString();
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        #endregion
        #region 检查访问来源
        /// <summary>
        /// 检查访问来源
        /// </summary>
        /// <param name="strDomain"></param>
        /// <returns></returns>
        public static bool CheckUrlReferrer()
        {
            string strUrlReferrer = Request(HttpContext.Current.Request.ServerVariables["HTTP_REFERER"], "");
            string strHttpHost = Request(HttpContext.Current.Request.ServerVariables["HTTP_HOST"], "");
            if (string.IsNullOrEmpty(strUrlReferrer) || string.IsNullOrEmpty(strHttpHost))
            {
                return false;
            }
            else
            {
                return strUrlReferrer.Contains(strHttpHost);
            }
        }
        #endregion
        #region 检查Ajax请求
        /// <summary>
        /// 检查Ajax请求
        /// </summary>
        /// <param name="strDomain"></param>
        /// <returns></returns>
        public static bool CheckAjax()
        {
            return Config.CheckUrlReferrer() && HttpContext.Current.Session["key"].ToString2() == HttpContext.Current.Session.SessionID.md5();
        }
        #endregion
        #region 301永久重定向
        /// <summary>
        /// 301永久重定向
        /// </summary>
        /// <param name="strUrl"></param>
        public static void Redirect301(string strUrl)
        {
            HttpContext.Current.Response.Status = "301 Moved Permanently";
            HttpContext.Current.Response.AddHeader("Location", strUrl);
        }
        #endregion
        #region 输出checkbox
        /// <summary>
        /// 输出checkbox
        /// </summary>
        /// <param name="name"></param>
        /// <param name="text"></param>
        /// <param name="value"></param>
        /// <param name="check"></param>
        /// <returns></returns>
        public static StringBuilder CheckBox(string name, object text, object value, string check)
        {
            StringBuilder strReturn = new StringBuilder();
            strReturn.AppendFormat("<input id=\"{0}_{2}\" name=\"{0}\" type=\"checkbox\" value=\"{2}\" {3}/><label for=\"{0}_{2}\">{1}</label>", name, text, value, check);
            return strReturn;
        }
        #endregion
        #region 输出radio
        /// <summary>
        /// 输出radio
        /// </summary>
        /// <param name="name"></param>
        /// <param name="text"></param>
        /// <param name="value"></param>
        /// <param name="check"></param>
        /// <returns></returns>
        public static StringBuilder Radio(string name, object text, object value, string check)
        {
            StringBuilder strReturn = new StringBuilder();
            strReturn.AppendFormat("<input id=\"{0}_{2}\" name=\"{0}\" type=\"radio\" value=\"{2}\" {3}/><label for=\"{0}_{2}\" style=\"margin-right:10px;\">{1}</label>", name, text, value, check);
            return strReturn;
        }
        public static StringBuilder RadioForBool(string name, object text, object value, string check)
        {
            StringBuilder strReturn = new StringBuilder();
            strReturn.AppendFormat("<input id=\"{0}_{2}\" name=\"{0}\" type=\"radio\" value=\"{2}\" {3}/><label for=\"{0}_{2}\" style=\"margin-right:10px;\">{1}</label>", name, text, Convert.ToBoolean(value), check);
            return strReturn;
        }
        #endregion
        #region 超链接目标列表
        /// <summary>
        /// 超链接目标列表
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> GetTargetSelectList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Text = "新窗口", Value = "_blank" });
            list.Add(new SelectListItem() { Text = "父窗口", Value = "_parent" });
            list.Add(new SelectListItem() { Text = "本窗口", Value = "_self", Selected = true });
            list.Add(new SelectListItem() { Text = "整页", Value = "_top" });
            return list;
        }
        #endregion
        #region 下拉列表中是否存在
        /// <summary>
        /// 下拉列表中是否存在
        /// </summary>
        /// <param name="drp">DropDownList对象名</param>
        /// <param name="val">要比较的值</param>
        /// <returns></returns>
        public static bool DropdownListIsExistItem(List<SelectListItem> list, string val)
        {
            bool tempBool = false;
            foreach (SelectListItem lt in list)
            {
                if (lt.Value == val)
                {
                    tempBool = true;
                    break;
                }
            }
            return tempBool;
        }
        #endregion
        #region 根据IP取省份
        /// <summary>
        /// 根据IP取省份
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static string GetProvinceByIP(string ip)
        {
            string strReturn = "";
            string url = "http://int.dpool.sina.com.cn/iplookup/iplookup.php?format=json&ip=" + ip;
            string json = HttpHelper.HttpGetData(url);
            if (!string.IsNullOrEmpty(json))
            {
                JObject jo = JObject.Parse(json);
                int ret = jo["ret"].Value<int>();
                if (ret == 1)
                {
                    strReturn = jo["province"].Value<string>();
                }
            }
            return strReturn;
        }
        #endregion
        #region 字符泛型下拉列表
        /// <summary>
        /// 字符泛型下拉列表
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static List<SelectListItem> GetSelectList(List<string> listString)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in listString)
            {
                list.Add(new SelectListItem() { Text = item, Value = item });
            }
            return list;
        }
        #endregion
        #region 获取物理路径
        /// <summary>
        /// 获取物理路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetMapPath(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                if (!path.Contains(":"))
                {
                    return HttpContext.Current.Server.MapPath(path);
                }
                else
                {
                    return path;
                }
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 获取物理路径_CDN
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetMapPath_CDN(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                //if (!path.Contains(":"))
                //{
                //    return HttpContext.Current.Server.MapPath(path);
                //}
                //else
                //{
                return path;
                //}
            }
            else
            {
                return "";
            }
        }

        #endregion
        #region 问候
        public static string SayHello()
        {
            string Hello;
            int h = DateTime.Now.Hour;
            if (h >= 0 && h < 6)
            {
                Hello = "凌晨好";
            }
            else if (h >= 6 && h < 9)
            {
                Hello = "早上好";
            }
            else if (h >= 9 && h < 12)
            {
                Hello = "上午好";
            }
            else if (h >= 12 && h < 18)
            {
                Hello = "下午好";
            }
            else
            {
                Hello = "晚上好";
            }
            return Hello;
        }
        #endregion
        #region 文件是否为图片
        public static bool IsPicture(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                string[] ArrPicExt = { "jpg", "gif", "bmp", "jpeg", "png" };
                string strFileExt = filePath.Substring(filePath.Length - 3, 3).ToLower();
                return ArrPicExt.Contains(strFileExt);
            }
            else
            {
                return false;
            }
        }
        #endregion
        #region 字符串缩写
        public static string stringformat(string str, int n)
        {
            ///
            ///格式化字符串长度，超出部分显示省略号,区分汉字跟字母。汉字2个字节，字母数字一个字节
            ///
            string temp = string.Empty;
            if (System.Text.Encoding.Default.GetByteCount(str) <= n)//如果长度比需要的长度n小,返回原字符串
            {
                return str;
            }
            else
            {
                int t = 0;
                char[] q = str.ToCharArray();
                for (int i = 0; i < q.Length && t < n; i++)
                {
                    if ((int)q[i] >= 0x4E00 && (int)q[i] <= 0x9FA5)//是否汉字
                    {
                        temp += q[i];
                        t += 2;
                    }
                    else
                    {
                        temp += q[i];
                        t++;
                    }
                }
                return (temp + "...");
            }
        }
        #endregion
        #region 获取文件MD5信息
        /// <summary>
        /// 获取文件的MD5信息
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetMD5HashFromFile(string fileName)
        {
            try
            {
                FileStream file = new FileStream(fileName, FileMode.Open);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
            }
        }
        #endregion
        #region 繁体转简体
        /// <summary>
        /// 繁体转简体
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Traditional2Simplified(string str)
        {
            return Strings.StrConv(str, VbStrConv.SimplifiedChinese, 0);
        }
        #endregion
        #region 计算密码强度
        /// <summary>
        /// 计算密码强度
        /// </summary>
        /// <param name="password">密码字符串</param>
        /// <returns></returns>
        public static YCS.Common.EnumList.Strength PasswordStrength(string password)
        {
            //空字符串强度值为0
            if (password == "") return YCS.Common.EnumList.Strength.Invalid;
            //字符统计
            int iNum = 0, iLtt = 0, iSym = 0;
            foreach (char c in password)
            {
                if (c >= '0' && c <= '9') iNum++;
                else if (c >= 'a' && c <= 'z') iLtt++;
                else if (c >= 'A' && c <= 'Z') iLtt++;
                else iSym++;
            }
            if (iLtt == 0 && iSym == 0) return YCS.Common.EnumList.Strength.Weak; //纯数字密码
            if (iNum == 0 && iLtt == 0) return YCS.Common.EnumList.Strength.Weak; //纯符号密码
            if (iNum == 0 && iSym == 0) return YCS.Common.EnumList.Strength.Weak; //纯字母密码
            if (password.Length <= 6) return YCS.Common.EnumList.Strength.Weak; //长度不大于6的密码
            if (iLtt == 0) return YCS.Common.EnumList.Strength.Normal; //数字和符号构成的密码
            if (iSym == 0) return YCS.Common.EnumList.Strength.Normal; //数字和字母构成的密码
            if (iNum == 0) return YCS.Common.EnumList.Strength.Normal; //字母和符号构成的密码
            if (password.Length <= 10) return YCS.Common.EnumList.Strength.Normal; //长度不大于10的密码
            return YCS.Common.EnumList.Strength.Strong; //由数字、字母、符号构成的密码
        }
        #endregion
        #region 把DateTimeOffset转成格式化的字符串
        /// <summary>
        /// 把DateTimeOffset转成格式化的字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetDateTimeOffsetByDatetime(string str)
        {
            string dt = "-";
            if (!string.IsNullOrEmpty(str))
            {
                str = str.Substring(0, str.LastIndexOf('+'));
                DateTime newdt = Convert.ToDateTime(str);
                if (newdt.Year == 1 || newdt.Year == 1900)
                    dt = "";
                else
                    dt = newdt.ToString("yyyy-MM-dd HH:mm:ss");
            }
            return dt;
        }
        #endregion

        #region 倒计时时间叠加处理
        /// <summary>
        /// 把DateTimeOffset倒计时时间叠加处理
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime GetDateTimeOffsetByDatetimeConvertDate(string str)
        {
            string dt = "-";
            if (!string.IsNullOrEmpty(str))
            {
                str = str.Substring(0, str.LastIndexOf('+'));
                DateTime newdt = Convert.ToDateTime(str);
                if (newdt.Year == 1 || newdt.Year == 1900)
                    dt = "";
                else
                    dt = newdt.AddHours(Config.OrderNoPayTime.ToFloat()).ToString("yyyy-MM-dd HH:mm:ss");
            }
            return Convert.ToDateTime(dt);
        }
        #endregion
        #region 生成workId
        /// <summary>
        /// 格式：PPYYxxxxxxxxS，第一二位数是商品的页数，三四位为西元年末两码，5到12码是流水号，第13位是EAN-13 Barcode标准的检核码。
        /// </summary>
        /// <returns></returns>
        public static string GetWorkId()
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("80");//第一二位數是商品的頁數(非相片書的商品預設80)
            sb.Append(DateTime.Now.Year.ToString().Substring(2));//三四位為西元年末兩碼
            sb.Append(Config.GetRndString(0, 8));//5到12碼是流水號

            int eanBarCode = EanBarCode(sb.ToString());//第13位是EAN-13 Barcode标准的检核码
            string workId = sb.ToString() + eanBarCode;

            return workId;
        }

        /// <summary>
        /// 第13位是EAN-13 Barcode标准的检核码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static int EanBarCode(string input)
        {
            int iDigit = 0;
            int iSum = 0;

            for (int i = input.Length; i >= 1; i--)
            {
                iDigit = Convert.ToInt32(input.Substring(i - 1, 1));
                if (i % 2 == 0)
                { // odd
                    iSum += iDigit * 3;
                }
                else
                { // even
                    iSum += iDigit * 1;
                }
            }
            int iCheckSum = (10 - (iSum % 10)) % 10;

            return iCheckSum;
        }
        #endregion
        #region 下载文件路径
        /// <summary>
        /// 静态资源URL地址
        /// </summary>
        public static string FileNameURL
        {
            get
            {
                return ConfigurationManager.AppSettings["FileNameURL"].ToString();
            }
        }
        #endregion
    }
}
