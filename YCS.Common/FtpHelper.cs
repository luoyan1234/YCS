using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Configuration;
using System.Web;

namespace YCS.Common
{
    public class FtpHelper
    {
        /// <summary>
        /// ftp服务器
        /// </summary>
        public static string ftp_server
        {
            get
            {
                return ConfigurationManager.AppSettings["ftp_server"].ToString();
            }
        }

        /// <summary>
        /// ftp帐号
        /// </summary>
        public static string ftp_user
        {
            get
            {
                return ConfigurationManager.AppSettings["ftp_user"].ToString();
            }
        }

        /// <summary>
        /// ftp密码
        /// </summary>
        public static string ftp_password
        {
            get
            {
                return ConfigurationManager.AppSettings["ftp_password"].ToString();
            }
        }

        /// <summary>
        /// ftp本地路径
        /// </summary>
        public static string ftp_local_path
        {
            get
            {
                return ConfigurationManager.AppSettings["ftp_local_path"].ToString();
            }
        }

        /// <summary>
        /// ftp远程路径
        /// </summary>
        public static string ftp_remote_path
        {
            get
            {
                return ConfigurationManager.AppSettings["ftp_remote_path"].ToString();
            }
        }

        #region 上传文件
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>Bool类型，如果为ture，则上传成功，否则上传失败</returns>
        public static bool Upload(string localFilePath, string remoteFilePath)
        {
            bool check = true;
            try
            {
                string filePath = Config.GetMapPath(localFilePath);
                string uri = ftp_server + ftp_remote_path + remoteFilePath;
                FileInfo fileInf = new FileInfo(filePath);

                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
                request.Credentials = new NetworkCredential(ftp_user, ftp_password);
                request.KeepAlive = false;
                request.UsePassive = false;
                request.UseBinary = true;
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.ContentLength = fileInf.Length;

                int buffLength = 2048;
                byte[] buff = new byte[buffLength];
                FileStream fs = fileInf.OpenRead();
                int contentLen = fs.Read(buff, 0, buffLength);
                Stream sr = request.GetRequestStream();
                while (contentLen != 0)
                {
                    sr.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }
                sr.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                Config.Err(ex);
                check = false;
            }
            return check;
        }
        #endregion

        #region 下载文件
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>Bool类型，如果为ture，则下载成功，否则下载失败</returns>
        public static bool Download(string localSavePath, string fileName, string remoteFilePath)
        {
            bool check = true;
            try
            {
                IOHelper.FolderCheck(Config.GetMapPath(localSavePath));
                FileStream fs = new FileStream(Config.GetMapPath(localSavePath + fileName), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
                string uri = ftp_server + ftp_remote_path + remoteFilePath;

                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
                request.Credentials = new NetworkCredential(ftp_user, ftp_password);
                request.UsePassive = false;
                request.UseBinary = true;
                request.Method = WebRequestMethods.Ftp.DownloadFile;

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream sr = response.GetResponseStream();
                long cl = response.ContentLength;
                int bufferSize = 2048;
                byte[] buffer = new byte[bufferSize];
                int readCount = sr.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    fs.Write(buffer, 0, readCount);
                    readCount = sr.Read(buffer, 0, bufferSize);
                }
                sr.Close();
                fs.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                Config.Err(ex);
                check = false;
            }
            return check;
        }
        #endregion

        #region 创建目录
        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="dirName"></param>
        public static bool MakeDirectory(string remotePath)
        {
            bool check = true;
            try
            {
                string uri = ftp_server + ftp_remote_path + remotePath;
                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
                request.UseBinary = true;
                request.Credentials = new NetworkCredential(ftp_user, ftp_password);
                request.Method = WebRequestMethods.Ftp.MakeDirectory;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                response.Close();
            }
            catch (Exception ex)
            {
                Config.Err(ex);
                check = false;
            }
            return check;
        }
        #endregion

        #region 检测目录是否存在
        /// <summary>
        /// 检测目录是否存在
        /// </summary>
        /// <param name="pFtpServerIP"></param>
        /// <param name="pFtpUserID"></param>
        /// <param name="pFtpPW"></param>
        /// <returns>false不存在，true存在</returns>
        public static bool DirectoryIsExist(string remotePath)
        {
            string[] value = GetFileList(remotePath);
            if (value == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static string[] GetFileList(string remotePath)
        {
            StringBuilder result = new StringBuilder();
            try
            {
                string uri = ftp_server + ftp_remote_path + remotePath;
                FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftp_user, ftp_password);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

                WebResponse response = reqFTP.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }
                reader.Close();
                response.Close();
                return result.ToString().Split('\n');
            }
            catch (Exception ex)
            {
                Config.Err(ex);
                return null;
            }
        }
        #endregion
    }
}
