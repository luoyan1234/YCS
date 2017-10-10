using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web.Mvc;

namespace YCS.Common
{
    public class StaticPageHelper
    {
        /// <summary>
        /// 根据View视图生成静态页面
        /// </summary>
        /// <param name="htmlPath">存放静态页面所在绝对路径</param>
        /// <param name="context">ControllerContext</param>
        /// <param name="ViewName">视图名称</param>
        /// <param name="MasterName">模板视图名称</param>
        /// <param name="Message">提示信息</param>
        /// <param name="outMessage">返回信息</param>
        /// <param name="isPartial">是否分布视图</param>
        /// <returns>生成成功返回true,失败false</returns>
        public static bool GenerateStaticPage(string htmlPath, ControllerContext context, string ViewName, string MasterName, string Message, out string outMessage, bool isPartial = false)
        {
            bool isSuccess = false;
            try
            {
                //创建存放静态页面目录                            
                if (!Directory.Exists(Path.GetDirectoryName(htmlPath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(htmlPath));
                }
                //删除已有的静态页面
                if (File.Exists(htmlPath))
                {
                    File.Delete(htmlPath);
                }

                ViewEngineResult result = null;
                if (isPartial)
                {
                    result = ViewEngines.Engines.FindPartialView(context, ViewName);
                }
                else
                {
                    result = ViewEngines.Engines.FindView(context, ViewName, MasterName);
                }

                /*
                 * 设置临时数据字典作为静态化标识
                 * 可以在视图上使用TempData["IsStatic"]来控制某些元素显示。
                 */
                if (!context.Controller.TempData.ContainsKey("IsStatic"))
                {
                    context.Controller.TempData.Add("IsStatic", true);
                }

                if (result.View != null)
                {
                    using (var sw = new StringWriter())
                    {
                        string strResultHtml = string.Empty;
                        //填充数据模型到视图中，并获取完整的页面
                        ViewContext viewContext = new ViewContext(context, result.View, context.Controller.ViewData, context.Controller.TempData, sw);
                        result.View.Render(viewContext, sw);
                        strResultHtml = sw.ToString();
                        //通过IO操作将页面内容生成静态页面
                        File.WriteAllText(htmlPath, strResultHtml);
                        outMessage = string.Format("生成{0}成功！存放路径：{1}", Message, htmlPath);
                        isSuccess = true;
                    }
                }
                else
                {
                    isSuccess = false;
                    outMessage = string.Format("生成{0}失败,未找到视图！", Message);
                }

            }
            catch (IOException ex)
            {
                outMessage = ex.Message;
                isSuccess = false;
            }

            return isSuccess;
        }
    }
}
