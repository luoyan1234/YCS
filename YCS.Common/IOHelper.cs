using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using System.Data;
using YCS.Common.OSS;

namespace YCS.Common
{
    /// <summary>
    /// 文件/文件夹操作类
    /// </summary>
    public class IOHelper
    {
        #region 检查文件夹是否存在,如不存在,则创建文件夹
        /// <summary>
        /// 检查文件夹是否存在,如不存在,则创建文件夹
        /// </summary>
        /// <param name="strFolderPath">文件夹路径</param>
        public static void FolderCheck(string strFolderPath)
        {
            //strFolderPath = Config.GetMapPath(strFolderPath);
            if (!Directory.Exists(strFolderPath))
            {
                Directory.CreateDirectory(strFolderPath);
            }
        }
        #endregion

        #region 替换文件夹名中的特殊字符
        /// <summary>
        /// 替换文件夹名中的特殊字符
        /// </summary>
        /// <param name="strFolderName">要检查的文件夹名</param>
        public static string FolderNameReplace(string strFolderName)
        {
            if (strFolderName != string.Empty)
            {
                strFolderName = strFolderName.Replace(",", "_");
                strFolderName = strFolderName.Replace(";", "_");
                strFolderName = strFolderName.Replace(".", "_");
                strFolderName = strFolderName.Replace("//", "/");
            }
            return strFolderName;
        }
        #endregion

        #region 替换文件名中的特殊字符
        /// <summary>
        /// 替换文件名中的特殊字符
        /// </summary>
        /// <param name="strFileName">要检查的文件名</param>
        public static string FileNameReplace(string strFileName)
        {
            if (strFileName != string.Empty)
            {
                string[] strRep = new string[] { "\\", "/", ":", "*", "?", "\"", "<", ">", "|" };
                foreach (string item in strRep)
                {
                    strFileName = strFileName.Replace(item, " ");
                }
            }
            return strFileName;
        }
        #endregion

        #region 复制文件夹
        /// <summary>
        /// 复制文件夹
        /// </summary>
        /// <param name="strFromDirectory"></param>
        /// <param name="strToDirectory"></param>
        public static void FolderCopy(string strFromDirectory, string strToDirectory)
        {
            strFromDirectory = Config.GetMapPath(strFromDirectory);
            strToDirectory = Config.GetMapPath(strToDirectory);
            FolderCheck(strToDirectory);
            if (!Directory.Exists(strFromDirectory))
            {
                return;
            }

            string[] directories = Directory.GetDirectories(strFromDirectory);//取文件夹下所有文件夹名，放入数组； 
            if (directories.Length > 0)
            {
                foreach (string d in directories)
                {
                    FolderCopy(d, strToDirectory + d.Substring(d.LastIndexOf("\\")));//递归拷贝文件和文件夹 
                }
            }

            string[] files = Directory.GetFiles(strFromDirectory);//取文件夹下所有文件名，放入数组； 
            if (files.Length > 0)
            {
                foreach (string s in files)
                {
                    File.Copy(s, strToDirectory + s.Substring(s.LastIndexOf("\\")));
                }
            }
        }
        #endregion

        #region 删除无子目录的文件夹
        /// <summary>
        /// 删除无子目录和文件的文件夹
        /// </summary>
        /// <param name="strFolderPath"></param>
        public static void FolderDel(string strFolderPath)
        {
            strFolderPath = Config.GetMapPath(strFolderPath);
            try
            {
                if (Directory.Exists(strFolderPath))
                {
                    DirectoryInfo dir = new DirectoryInfo(strFolderPath);
                    FileSystemInfo[] arrDir = dir.GetFileSystemInfos();
                    dir.Delete();
                }
            }
            catch (Exception ex)
            {
                Config.Err(ex);
            }
        }
        #endregion

        #region 文件夹和文件的数量
        /// <summary>
        /// 文件夹和文件的数量
        /// </summary>
        /// <param name="strFolderPath">要检查的文件夹</param>
        /// <returns></returns>
        public static int FolderAndFileTotal(string strFolderPath)
        {
            strFolderPath = Config.GetMapPath(strFolderPath);
            if (Directory.Exists(strFolderPath))
            {
                DirectoryInfo dir = new DirectoryInfo(strFolderPath);
                FileSystemInfo[] arrDir = dir.GetFileSystemInfos();
                return arrDir.Length;
            }
            else
            {
                return 0;
            }
        }
        #endregion

        #region 生成文件
        /// <summary>
        /// 生成文件
        /// </summary>
        /// <param name="strFilePath"></param>
        /// <param name="strFileContent"></param>
        public static void FileCreate(string strFilePath, string strFileContent)
        {
            strFilePath = Config.GetMapPath(strFilePath);
            File.WriteAllText(strFilePath, strFileContent, Encoding.UTF8);
        }
        #endregion

        #region 读取文件内容
        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="strFilePath"></param>
        /// <returns></returns>
        public static string FileRead(string strFilePath)
        {
            strFilePath = Config.GetMapPath(strFilePath);
            if (File.Exists(strFilePath))
            {
                return File.ReadAllText(strFilePath, Encoding.UTF8);
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region 复制文件
        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="strFromDirectory"></param>
        /// <param name="strToDirectory"></param>
        /// <param name="diyid">对应数据项的id，作为生产的文件名的标记前缀,by michael in 2017-01-10 10:37</param>
        public static string FileCopy(string strFromFile, string strToDirectory, string id, string fileName = "", string idTag = "")
        {
            if (string.IsNullOrEmpty(strFromFile) || !File.Exists(Config.GetMapPath(strFromFile)))
            {
                return "";
            }
            else
            {
                strFromFile = Config.GetMapPath(strFromFile);
                string strFileExt = Path.GetExtension(strFromFile);
                string strFileName = Path.GetFileNameWithoutExtension(strFromFile);

                FolderCheck(Config.GetMapPath(strToDirectory));
                Random rnd = new Random(Config.GetRandomSeed());
                if (string.IsNullOrEmpty(fileName))
                {
                    //对应数据项的id，作为生产的文件名的标记前缀,by michael in 2017-01-10 10:37
                    fileName = idTag + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + rnd.Next(1000, 9999) + id + strFileExt;
                }
                string strSaveFilePath = strToDirectory + fileName;
                File.Copy(strFromFile, Config.GetMapPath(strSaveFilePath));

                return strSaveFilePath;
            }
        }
        #endregion

        #region 删除文件
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="strFromDirectory"></param>
        /// <param name="strToDirectory"></param>
        public static bool FileDelete(string strFilePath)
        {
            try
            {
                if (!string.IsNullOrEmpty(strFilePath))
                {
                    string strFile = Config.GetMapPath(strFilePath);
                    if (File.Exists(strFile))
                    {
                        File.Delete(strFile);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Config.Err(ex);
            }
            return false;
        }
        #endregion
        #region 判断是否是标准格式的文件

        /// 将指定字符串写入指定单元格中
        /// </summary>
        /// <param name="data">要写入的数据源</param> 
        /// <param name="toPath">excel文件路径</param>
        /// <param name="fromPath">excel文件路径</param>
        public static bool IsAllowedExtension(string fileName, string fileType)
        {
            System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.IO.BinaryReader r = new System.IO.BinaryReader(fs);
            string fileclass = "";
            byte buffer;
            try
            {
                buffer = r.ReadByte();
                fileclass = buffer.ToString();
                buffer = r.ReadByte();
                fileclass += buffer.ToString();
            }
            catch
            {
                return false;
            }
            r.Close();
            fs.Close();
            Dictionary<string, string> ftDic = new Dictionary<string, string>();
            ftDic.Add("gif", "7173");
            ftDic.Add("jpg", "255216");
            ftDic.Add("png", "13780");
            ftDic.Add("bmp", "6677");
            ftDic.Add("txt", "239187");
            ftDic.Add("aspx", "239187");
            ftDic.Add("asp", "239187");
            ftDic.Add("sql", "239187");
            ftDic.Add("xls", "208207");
            ftDic.Add("doc", "208207");
            ftDic.Add("ppt", "208207");
            ftDic.Add("xml", "6063");
            ftDic.Add("htm", "6033");
            ftDic.Add("html", "6033");
            ftDic.Add("js", "4742");
            ftDic.Add("xlsx", "8075");
            ftDic.Add("pptx", "8075");
            ftDic.Add("mmap", "8075");
            ftDic.Add("zip", "8075");
            ftDic.Add("rar", "8297");
            ftDic.Add("accdb", "01");
            ftDic.Add("mdb", "01");

            ftDic.Add("exe", "7790");
            ftDic.Add("dll", "7790");
            ftDic.Add("psd", "5666");

            ftDic.Add("rdp", "255254");
            ftDic.Add("bt种子", "10056");
            ftDic.Add("bat", "64101");

            if (fileclass == ftDic[fileType])
                return true;
            return false;

        }
        #endregion
        #region 创建等比例缩略图
        /// <summary>
        /// 创建等比例缩略图
        /// </summary>
        /// <param name="imagePath"></param>
        /// <param name="folderName"> "DiyImage/"</param>
        /// <param name="width">(width * percent).ToInt()</param>
        /// <param name="height">(heigth * percent).ToInt()</param>
        /// <param name="format">ImageFormat.Png</param>
        /// <param name="idTag">标记用，如"381_"</param>
        /// <returns></returns>
        public static string CreateThumb(string imagePath, string folderName, int width, int height, ImageFormat format, string idTag = "")
        {
            if (!string.IsNullOrEmpty(imagePath))
            {
                string originalImagePath = imagePath;
                if (File.Exists(originalImagePath))
                {
                    string fileName = idTag + Path.GetFileNameWithoutExtension(originalImagePath);
                    string fileExt = Path.GetExtension(originalImagePath);
                    string saveThumbFolder = Config.FileUploadPath + folderName + "/Thumb/";
                    IOHelper.FolderCheck(Config.GetMapPath(saveThumbFolder));
                    string thumbnailPath = saveThumbFolder + fileName + ".thumb" + fileExt;

                    ImageHelper.MakeThumbnail(originalImagePath, thumbnailPath, width, height, "CutScale", format);

                    OSSUploadHelper.UploadLocalFile(thumbnailPath.TrimStart('/'), Config.GetMapPath(thumbnailPath));

                    return thumbnailPath;
                }
            }
            return imagePath;
        }
        #endregion

        #region  获取文件的文件名称和后缀名
        public static string GetFileName(string FileName)
        {
            string str = "暂无";
            if (!string.IsNullOrEmpty(FileName))
            {
                FileInfo fi = new FileInfo(FileName);
                str = fi.Name;
            }
            return str;
        }
        #endregion
    }
}
