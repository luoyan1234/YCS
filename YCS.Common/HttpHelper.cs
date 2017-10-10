using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace YCS.Common
{
    /// <summary>
    /// Http类
    /// </summary>
    public class HttpHelper
    {
        public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            //直接确认，否则打不开    
            return true;
        }

        #region http请求读取数据
        /// <summary>
        /// http请求读取数据
        /// </summary>
        /// <param name="strGetUrl"></param>
        /// <returns></returns>
        public static string HttpGetData(string strGetUrl)
        {
            //设置最大连接数
            ServicePointManager.DefaultConnectionLimit = 200;
            //设置https验证方式
            if (strGetUrl.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            }

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strGetUrl);
                request.Method = "GET";
                request.KeepAlive = true;
                request.ContentType = "application/json";
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (StreamReader read = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                        {
                            return read.ReadToEnd();
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                using (var sr =new StreamReader(ex.Response.GetResponseStream()))
                {
                    var content = sr.ReadToEnd();
                    return content;
                }
             
            }
            return "";
        }
        #endregion

        #region http请求读取数据
        /// <summary>
        /// http请求读取数据
        /// </summary>
        /// <param name="strGetUrl"></param>
        /// <returns></returns>
        public static bool HttpGetData(string strGetUrl, bool IsReturnBool)
        {
            //设置最大连接数
            ServicePointManager.DefaultConnectionLimit = 200;
            //设置https验证方式
            if (strGetUrl.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            }
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strGetUrl);
                request.Method = "GET";
                request.KeepAlive = true;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    return response.StatusCode == HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                Config.Err(ex);
            }
            return false;
        }
        #endregion

        #region http请求提交数据
        /// <summary>
        /// http请求提交数据
        /// </summary>
        /// <param name="strPostUrl"></param>
        /// <param name="strPostData"></param>
        /// <returns></returns>
        public static string HttpPostData(string strPostUrl, string strPostData)
        {
            //设置最大连接数
            ServicePointManager.DefaultConnectionLimit = 200;
            //设置https验证方式
            if (strPostUrl.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            }

            UTF8Encoding encoding = new UTF8Encoding();
            byte[] b = encoding.GetBytes(strPostData);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strPostUrl);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = b.Length;
            try
            {
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(b, 0, b.Length);
                }
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (StreamReader read = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                        {
                            return read.ReadToEnd();
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                using (var sr = new StreamReader(ex.Response.GetResponseStream()))
                {
                    var content = sr.ReadToEnd();
                    return content;
                }

            }
            return "";
        }
        #endregion

        #region http请求提交数据
        /// <summary>
        /// 返回布尔值
        /// </summary>
        /// <param name="strPostUrl"></param>
        /// <param name="strPostData"></param>
        /// <param name="IsReturnBool"></param>
        /// <returns></returns>
        public static bool HttpPostData(string strPostUrl, string strPostData, bool IsReturnBool)
        {
            //设置最大连接数
            ServicePointManager.DefaultConnectionLimit = 200;
            //设置https验证方式
            if (strPostUrl.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            }

            try
            {
                UTF8Encoding encoding = new UTF8Encoding();
                byte[] b = encoding.GetBytes(strPostData);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strPostUrl);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = b.Length;
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(b, 0, b.Length);
                }
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    return response.StatusCode == HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                Config.Err(ex);
            }
            return false;
        }
        #endregion

        #region http请求提交XML数据
        /// <summary>
        /// http请求提交XML数据
        /// </summary>
        /// <param name="strPostUrl"></param>
        /// <param name="strPostXml"></param>
        /// <returns></returns>
        public static string HttpPostXml(string strPostXml, string strPostUrl, bool isUseCert, int timeout)
        {
            //设置最大连接数
            ServicePointManager.DefaultConnectionLimit = 200;
            //设置https验证方式
            if (strPostUrl.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            }

            try
            {
                UTF8Encoding encoding = new UTF8Encoding();
                byte[] b = encoding.GetBytes(strPostXml);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strPostUrl);
                request.Method = "POST";
                request.Timeout = timeout * 1000;
                request.ContentType = "text/xml";
                request.ContentLength = b.Length;
                //是否使用证书
                if (isUseCert)
                {
                    //string path = HttpContext.Current.Request.PhysicalApplicationPath;
                    //X509Certificate2 cert = new X509Certificate2(path, password);
                    //request.ClientCertificates.Add(cert);
                }
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(b, 0, b.Length);
                }
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (StreamReader read = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                        {
                            return read.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Config.Err(ex);
            }
            return "";
        }
        #endregion

        #region http请求下载文件
        /// <summary>
        /// http请求下载文件
        /// </summary>
        /// <param name="strUrl"></param>
        /// <returns></returns>
        public static void HttpGetFile(string strUrl, string strSavePath, string strFileName)
        {
            //设置最大连接数
            ServicePointManager.DefaultConnectionLimit = 200;
            //设置https验证方式
            if (strUrl.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            }

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strUrl);
                request.Method = "GET";
                request.KeepAlive = true;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (Stream stream = response.GetResponseStream())
                        {
                            using (FileStream fs = File.Create(strSavePath + strFileName))
                            {
                                //建立字节组，并设置它的大小是多少字节
                                byte[] bytes = new byte[102400];
                                int n = 1;
                                while (n > 0)
                                {
                                    //一次从流中读多少字节，并把值赋给n，当读完后，n为0,并退出循环
                                    n = stream.Read(bytes, 0, 10240);
                                    fs.Write(bytes, 0, n);　//将指定字节的流信息写入文件流中
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Config.Err(ex);
            }
        }
        #endregion

        #region http请求下载图片
        /// <summary>
        /// http请求下载图片
        /// </summary>
        /// <param name="strUrl"></param>
        /// <returns></returns>
        public static void HttpGetPic(string strUrl, string strSavePath, string strFileNameNoExt)
        {
            //设置最大连接数
            ServicePointManager.DefaultConnectionLimit = 200;
            //设置https验证方式
            if (strUrl.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            }

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strUrl);
                request.Method = "GET";
                request.KeepAlive = true;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string strFileExt = strUrl.Substring(strUrl.Length - 3, 3);
                        Stream stream = response.GetResponseStream();
                        Image img = Image.FromStream(stream);
                        switch (strFileExt.ToLower())
                        {
                            case "gif":
                                img.Save(strSavePath + strFileNameNoExt + ".gif", ImageFormat.Gif);
                                break;
                            case "jpg":
                            case "jpeg":
                                img.Save(strSavePath + strFileNameNoExt + ".jpg", ImageFormat.Jpeg);
                                break;
                            case "png":
                                img.Save(strSavePath + strFileNameNoExt + ".png", ImageFormat.Png);
                                break;
                            case "icon":
                                img.Save(strSavePath + strFileNameNoExt + ".icon", ImageFormat.Icon);
                                break;
                            case "bmp":
                                img.Save(strSavePath + strFileNameNoExt + ".bmp", ImageFormat.Bmp);
                                break;
                        }
                        img.Dispose();
                        stream.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                Config.Err(ex);
            }
        }
        #endregion

        #region 获取POST返回的数据
        /// <summary>
        /// 获取POST返回的数据
        /// </summary>
        /// <param name="request"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string PostInput(HttpRequestBase request, Encoding encoding)
        {
            StringBuilder strPost = new StringBuilder();
            try
            {
                using (Stream stream = request.InputStream)
                {
                    int count = 0;
                    byte[] buffer = new byte[1024];
                    while ((count = stream.Read(buffer, 0, 1024)) > 0)
                    {
                        strPost.Append(encoding.GetString(buffer, 0, count));
                    }
                }
                return strPost.ToString();
            }
            catch (Exception ex)
            {
                Config.Err(ex);
                return "";
            }
        }
        #endregion

        #region POST返回数据
        /// <summary>
        /// POST返回数据
        /// </summary>
        /// <param name="response"></param>
        /// <param name="content"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public bool PostOutput(HttpResponseBase response, string content, Encoding encoding)
        {
            try
            {
                using (Stream stream = response.OutputStream)
                {
                    byte[] bys = encoding.GetBytes(content);
                    stream.Write(bys, 0, bys.Length);
                    stream.Flush();
                }
                return true;
            }
            catch (Exception ex)
            {
                Config.Err(ex);
                return false;
            }
        }
        #endregion
    }
}
