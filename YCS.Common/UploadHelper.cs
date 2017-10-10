using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace YCS.Common
{
    /// <summary>
    /// 文件上传类
    /// </summary>
    public class UploadHelper
    {
        #region 文件上传(HttpPostedFile)
        /// <summary>
        /// 文件上传(HttpPostedFile)
        /// </summary>
        /// <param name="file"></param>
        /// <param name="strFolderPath"></param>
        /// <returns></returns>
        public static int FileUpload(HttpPostedFile file, int intMaxSize, string strAllowExt, string strFolderPath, out string strFilePath)
        {
            IOHelper.FolderCheck(strFolderPath);
            if (file == null || file.ContentLength == 0 || string.IsNullOrEmpty(file.FileName))
            {
                strFilePath = "";
                return 1;//未选择上传文件
            }
            else
            {
                string strFileExt = IOHelper.FileNameReplace(Path.GetExtension(file.FileName)).ToLower();
                string strFileName = IOHelper.FileNameReplace(Path.GetFileNameWithoutExtension(file.FileName)).ToLower();
                Random rnd = new Random(Config.GetRandomSeed());
                string strSaveFilePath = strFolderPath + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + rnd.Next(1000, 9999) + strFileExt;

                if (intMaxSize != 0 && file.ContentLength > intMaxSize)
                {
                    strFilePath = "";
                    return 2;//超出限制大小
                }
                else if (!string.IsNullOrEmpty(strAllowExt) && (strAllowExt.IndexOf(strFileExt) == -1 || strSaveFilePath.IndexOf(".asp") > -1 || strSaveFilePath.IndexOf(".asa") > -1 || strSaveFilePath.IndexOf(".asax") > -1 || strSaveFilePath.IndexOf(".aspx") > -1 || strSaveFilePath.IndexOf(".ashx") > -1 || strSaveFilePath.IndexOf(".jsp") > -1 || strSaveFilePath.IndexOf(".php") > -1))
                {
                    strFilePath = "";
                    return 3;//文件格式不正确
                }
                else
                {
                    try
                    {
                        file.SaveAs(Config.GetMapPath(strSaveFilePath));
                        strFilePath = strSaveFilePath;
                        return 0;
                    }
                    catch (Exception ex)
                    {
                        Config.Err(ex);
                        strFilePath = "";
                        return -1;//系统错误
                    }
                }
            }
        }
        #endregion

        #region 文件上传(HttpPostedFileBase)
        /// <summary>
        /// 文件上传(HttpPostedFileBase)
        /// </summary>
        /// <param name="file"></param>
        /// <param name="strFolderPath"></param>
        /// <returns></returns>
        public static int FileUpload(HttpPostedFileBase file, int intMaxSize, string strAllowExt, string strFolderPath, string strFileKey, out string strFilePath, out string outFileName)
        {
            strFilePath = "";
            outFileName = "";
            IOHelper.FolderCheck(Config.GetMapPath(strFolderPath));
            if (file == null || file.ContentLength == 0 || string.IsNullOrEmpty(file.FileName))
            {
                return 1;//未选择上传文件
            }
            else
            {
                string strFileExt = IOHelper.FileNameReplace(Path.GetExtension(file.FileName)).ToLower();
                string strFileName = IOHelper.FileNameReplace(Path.GetFileNameWithoutExtension(file.FileName)).ToLower();
                Random rnd = new Random(Config.GetRandomSeed());
                string strSaveFilePath = strFolderPath + strFileKey + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + rnd.Next(1000, 9999) + strFileExt;

                if (intMaxSize != 0 && file.ContentLength > intMaxSize)
                {
                    return 2;//超出限制大小
                }
                else if (!string.IsNullOrEmpty(strAllowExt) && (strAllowExt.IndexOf(strFileExt) == -1 || strSaveFilePath.IndexOf(".asp") > -1 || strSaveFilePath.IndexOf(".asa") > -1 || strSaveFilePath.IndexOf(".asax") > -1 || strSaveFilePath.IndexOf(".aspx") > -1 || strSaveFilePath.IndexOf(".ashx") > -1 || strSaveFilePath.IndexOf(".jsp") > -1 || strSaveFilePath.IndexOf(".php") > -1))
                {
                    return 3;//文件格式不正确
                }
                else
                {
                    try
                    {
                        file.SaveAs(Config.GetMapPath(strSaveFilePath));
                        strFilePath = strSaveFilePath;
                        outFileName = file.FileName;
                        return 0;
                    }
                    catch (Exception ex)
                    {
                        Config.Err(ex);
                        return -1;//系统错误
                    }
                }
            }
        }
        #endregion

        #region 文件上传(HttpPostedFileBase)
        /// <summary>
        /// 文件上传(HttpPostedFileBase)
        /// </summary>
        /// <param name="file"></param>
        /// <param name="strFolderPath"></param>
        /// <returns></returns>
        public static int FileUpload(HttpPostedFileBase file, string strFileNameNoExt, int intMaxSize, string strAllowExt, string strFolderPath, string strFileKey, out string strFilePath, out string outFileExt)
        {
            strFilePath = "";
            outFileExt = "";
            IOHelper.FolderCheck(Config.GetMapPath(strFolderPath));
            if (file == null || file.ContentLength == 0 || string.IsNullOrEmpty(file.FileName))
            {
                return 1;//未选择上传文件
            }
            else
            {
                string strFileExt = Path.GetExtension(file.FileName).ToLower();
                strFileNameNoExt = IOHelper.FileNameReplace(strFileNameNoExt);
                string strSaveFilePath = strFolderPath + strFileKey + strFileNameNoExt + strFileExt;

                if (intMaxSize != 0 && file.ContentLength / 1024 / 1024 > intMaxSize)
                {
                    return 2;//超出限制大小
                }
                else if (!string.IsNullOrEmpty(strAllowExt) && (strAllowExt.IndexOf(strFileExt) == -1 || strSaveFilePath.IndexOf(".asp") > -1 || strSaveFilePath.IndexOf(".asa") > -1 || strSaveFilePath.IndexOf(".asax") > -1 || strSaveFilePath.IndexOf(".aspx") > -1 || strSaveFilePath.IndexOf(".ashx") > -1 || strSaveFilePath.IndexOf(".jsp") > -1 || strSaveFilePath.IndexOf(".php") > -1))
                {
                    return 3;//文件格式不正确
                }
                else
                {
                    try
                    {
                        file.SaveAs(Config.GetMapPath(strSaveFilePath));
                        strFilePath = strSaveFilePath;
                        outFileExt = strFileExt;
                        return 0;
                    }
                    catch (Exception ex)
                    {
                        Config.Err(ex);
                        return -1;//系统错误
                    }
                }
            }
        }
        #endregion
    }
}
