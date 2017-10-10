using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;

namespace YCS.Common
{
    /// <summary>
    /// Excel处理类
    /// </summary>
    public class ControllerHelper : Controller
    {
        /// <summary>
        /// 导出Excel  不加大标题
        /// </summary>
        /// <param name="FileName">文件名称不带后缀</param>
        /// <param name="lstTitle">表头</param>
        /// <param name="dst">数据集合</param>
        /// <returns></returns>
        public FileResult ExportExcel(string FileName, List<string> lstTitle, List<string> lstFileName, DataTable dt = null)
        {
            var sbHtml = new StringBuilder();
            //防止中文乱码
            sbHtml.Append("<html><head><meta http-equiv=Content-Type content=\"text/html; charset=utf-8\">");
            sbHtml.Append("<table border='1' cellspacing='0' cellpadding='0'>");
            sbHtml.Append("<tr>");
            //var lstTitle = new List<string> { "编号", "姓名", "年龄", "创建时间" };
            foreach (var item in lstTitle)
            {
                sbHtml.AppendFormat("<td style='font-size: 14px;text-align:center;background-color: #DCE0E2; font-weight:bold;' height='25'>{0}</td>", item);
            }
            sbHtml.Append("</tr>");
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sbHtml.Append("<tr>");
                    foreach (var item in lstFileName)
                    {
                        sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", dt.Rows[i][item].ToString() + "&nbsp;");
                    }
                    sbHtml.Append("</tr>");
                }
            }
            sbHtml.Append("</table>");


            //第一种:使用FileContentResult
            byte[] fileContents = Encoding.GetEncoding("utf-8").GetBytes(sbHtml.ToString()); //Default,utf8有时中文乱码
            //string Name = FileName + ".xls";
            //return File(fileContents, "application/ms-excel", Name);

            ////第二种:使用FileStreamResult
            var fileStream = new MemoryStream(fileContents);
            string Name = FileName + ".xls";
            return File(fileStream, "application/ms-excel", Name);

            ////第三种:使用FilePathResult
            ////服务器上首先必须要有这个Excel文件,然会通过Server.MapPath获取路径返回.
            //var fileName = System.Web.HttpContext.Current.Server.MapPath("~/Files/fileName.xls");
            //return File(fileName, "application/ms-excel", "fileName.xls");
        }

        /// <summary>
        /// 导出Excel  加大标题
        /// </summary>
        /// <param name="FileName">文件名称不带后缀</param>
        /// <param name="lstTitle">表头</param>
        /// <param name="dst">数据集合</param>
        /// <returns></returns>
        public FileResult ExporExcel(string FileName, List<string> lstTitle, List<string> lstFileName, DataTable dt = null, NameValueCollection bTitleCollection = null)
        {
            var sbHtml = new StringBuilder();
            //防止中文乱码
            sbHtml.Append("<html><head><meta http-equiv=Content-Type content=\"text/html; charset=utf-8\">");
            sbHtml.Append("<table border='1' cellspacing='0' cellpadding='0'>");
            //添加顶部大标题
            if (bTitleCollection != null)
            {
                sbHtml.Append("<tr>");
                foreach (string key in bTitleCollection)
                    sbHtml.AppendFormat("<td colspan={0}>{1}</td>", bTitleCollection[key], key);
                sbHtml.Append("</tr>");
            }
            sbHtml.Append("<tr>");
            //var lstTitle = new List<string> { "编号", "姓名", "年龄", "创建时间" };
            foreach (var item in lstTitle)
            {
                sbHtml.AppendFormat("<td style='font-size: 14px;text-align:center;background-color: #DCE0E2; font-weight:bold;' height='25'>{0}</td>", item);
            }
            sbHtml.Append("</tr>");
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sbHtml.Append("<tr>");
                    foreach (var item in lstFileName)
                    {
                        sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", dt.Rows[i][item].ToString() + "&nbsp;");
                    }
                    sbHtml.Append("</tr>");
                }
            }
            sbHtml.Append("</table>");
            sbHtml.Append("</body></html>");

            //第一种:使用FileContentResult
            byte[] fileContents = Encoding.GetEncoding("utf-8").GetBytes(sbHtml.ToString()); //Default,utf8有时中文乱码
            string Name = FileName + ".xls";
            return File(fileContents, "application/ms-excel", Name);

            ////第二种:使用FileStreamResult
            //var fileStream = new MemoryStream(fileContents);
            //return File(fileStream, "application/ms-excel", "fileStream.xls");

            ////第三种:使用FilePathResult
            ////服务器上首先必须要有这个Excel文件,然会通过Server.MapPath获取路径返回.
            //var fileName = System.Web.HttpContext.Current.Server.MapPath("~/Files/fileName.xls");
            //return File(fileName, "application/ms-excel", "fileName.xls");
        }

        /// <summary>
        /// 把List转成Datatable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public DataTable ListToDataTable<T>(List<T> entitys)
        {

            //检查实体集合不能为空
            if (entitys == null || entitys.Count < 1)
            {
                return new DataTable();
            }

            //取出第一个实体的所有Propertie
            Type entityType = entitys[0].GetType();
            PropertyInfo[] entityProperties = entityType.GetProperties();

            //生成DataTable的structure
            //生产代码中，应将生成的DataTable结构Cache起来，此处略
            DataTable dt = new DataTable("dt");
            for (int i = 0; i < entityProperties.Length; i++)
            {
                //dt.Columns.Add(entityProperties[i].Name, entityProperties[i].PropertyType);
                dt.Columns.Add(entityProperties[i].Name);
            }

            //将所有entity添加到DataTable中
            foreach (object entity in entitys)
            {
                //检查所有的的实体都为同一类型
                if (entity.GetType() != entityType)
                {
                    throw new Exception("要转换的集合元素类型不一致");
                }
                object[] entityValues = new object[entityProperties.Length];
                for (int i = 0; i < entityProperties.Length; i++)
                {
                    entityValues[i] = entityProperties[i].GetValue(entity, null);

                }
                dt.Rows.Add(entityValues);
            }
            return dt;
        }
        /// <summary>
        /// 把datatable装成List<T> 字段名、字段类型必须要和Model字段名、类型一致
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public IList<T> GetList<T>(DataTable table)
        {
            IList<T> list = new List<T>();
            T t = default(T);
            PropertyInfo[] propertypes = null;
            string tempName = string.Empty;
            foreach (DataRow row in table.Rows)
            {
                t = Activator.CreateInstance<T>();
                propertypes = t.GetType().GetProperties();
                foreach (PropertyInfo pro in propertypes)
                {
                    tempName = pro.Name;
                    if (table.Columns.Contains(tempName))
                    {
                        object value = row[tempName];
                        pro.SetValue(t, value, null);
                    }
                }
                list.Add(t);
            }
            return list;
        }
    }
}
