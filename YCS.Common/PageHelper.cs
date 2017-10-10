using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YCS.Common
{
    /// <summary>
    /// 分页基类
    /// Create by Jimy
    /// </summary>
    public class PageHelper
    {
        /// <summary>
        /// 排序语句
        /// </summary>
        public string FieldOrder { get; set; }
        /// <summary>
        /// 分页数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 当前分页
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// 分页链接
        /// </summary>
        public string PageUrl { get; set; }

        /// <summary>
        /// 分页字符类型
        /// </summary>
        public EnumList.PageStrType PageStrType { get; set; }

        #region 分页字符
        /// <summary>
        /// 分页-简单
        /// </summary>
        public static StringBuilder PageStrSimple(int PageSize, int CurrentPage, string PageUrl, int AllCount, int PageCount)
        {
            StringBuilder strPage = new StringBuilder();
            if (AllCount > 0)
            {
                if (CurrentPage < 1) CurrentPage = 1;
                if (CurrentPage > PageCount) CurrentPage = PageCount;

                if (PageCount > 1)
                {
                    strPage.Append("<div class=\"pagesPN\">");
                    if (CurrentPage == 1)
                    {
                        strPage.Append("<a class=\"prev\">&lt;</a> \n");
                    }
                    else
                    {
                        strPage.Append("<a class=\"prev\" href=\"" + PageUrl + "page=" + (CurrentPage - 1).ToString() + "\">&lt;</a> \n");
                    }
                    strPage.Append("<em>" + CurrentPage + "</em>/" + PageCount + " \n");
                    if (CurrentPage == PageCount)
                    {
                        strPage.Append("<a class=\"next\" href=\"javascript:;\">&gt;</a>\n");
                    }
                    else
                    {
                        strPage.Append("<a class=\"next\" href=\"" + PageUrl + "page=" + (CurrentPage + 1).ToString() + "\">&gt;</a>\n");
                    }
                    strPage.Append("</div>");
                }
            }
            return strPage;
        }
        /// <summary>
        /// 分页-后台
        /// </summary>
        public static StringBuilder PageStrMa(int PageSize, int CurrentPage, string PageUrl, int AllCount, int PageCount)
        {
            StringBuilder strPage = new StringBuilder();
            //if (AllCount == 0)
            //{
            //    strPage.Append("<p>查询无相关记录!</p>");
            //}
            //else
            //{
            //    if (CurrentPage < 1) CurrentPage = 1;
            //    if (CurrentPage > PageCount) CurrentPage = PageCount;

            //    strPage.Append("共<b>" + PageCount + "</b>页 第<b>" + CurrentPage + "</b>页 <b>" + PageSize + "</b>条/页 共<b>" + AllCount + "</b>条记录\n");
            //    if (CurrentPage == 1)
            //    {
            //        strPage.Append("<span>首页 上一页</span>\n");
            //    }
            //    else
            //    {
            //        strPage.Append("<a href=\"" + PageUrl + "page=1\">首页</a>\n");
            //        strPage.Append("<a href=\"" + PageUrl + "page=" + (CurrentPage - 1).ToString() + "\">上一页</a>\n");
            //    }
            //    if (CurrentPage == PageCount)
            //    {
            //        strPage.Append("<span>下一页 尾页</span>\n");
            //    }
            //    else
            //    {
            //        strPage.Append("<a href=\"" + PageUrl + "page=" + (CurrentPage + 1).ToString() + "\">下一页</a>\n");
            //        strPage.Append("<a href=\"" + PageUrl + "page=" + PageCount + "\">尾页</a>\n");
            //    }
            //    strPage.Append("跳到<select onchange=\"javascript:location.href='" + PageUrl + "page='+this.options[this.selectedIndex].value\">\n");
            //    for (int i = 1; i <= PageCount; i++)
            //    {
            //        string sel = "";
            //        if (i == CurrentPage) sel = "selected";
            //        strPage.Append("<option value=\"" + i + "\" " + sel + ">" + i + "</option>\n");
            //    }
            //    strPage.Append("</select>页\n");
            //}

            if (AllCount == 0)
            {
                strPage.Append("<p>查询无相关记录!</p>");
            }
            else
            {
                if (CurrentPage < 1) CurrentPage = 1;
                if (CurrentPage > PageCount) CurrentPage = PageCount;

                int zoomsize = 10;
                int zoom, downlimit, uplimit;
                zoom = (CurrentPage - CurrentPage % zoomsize) / zoomsize;
                if (CurrentPage % zoomsize == 0)
                {
                    zoom = zoom - 1;
                }
                downlimit = zoom * zoomsize + 1;
                uplimit = downlimit + zoomsize - 1;

                if (uplimit > PageCount)
                {
                    uplimit = PageCount;
                }
                strPage.Append("<span class=\"page-info\">共" + AllCount + "条数据 / 共" + PageCount + "页 每页<em class=\"c-ylo\">" + PageSize + "</em>条</span> ");
                if (CurrentPage != 1)
                {
                    strPage.Append("<a href=\"" + PageUrl + "page=1\"  class=\"prev\">首页</a>\n");
                    strPage.Append("<a href=\"" + PageUrl + "page=" + (CurrentPage - 1).ToString() + "\"  class=\"prev\">上一页</a>\n");
                }
                else
                {
                    strPage.Append("<a href=\"javascript:;\"  class=\"prev gray\">首页</a>\n");
                    strPage.Append("<a href=\"javascript:;\"  class=\"prev gray\">上一页</a>\n");
                }
                if (downlimit - 1 >= zoomsize)
                {
                    strPage.Append("<a href=\"" + PageUrl + "page=" + (downlimit - 1).ToString() + "\"  class=\"prev\">...</a>\n");
                }
                for (int i = downlimit; i <= uplimit; i++)
                {
                    if (i == CurrentPage)
                    {
                        strPage.Append("<a href=\"" + PageUrl + "page=" + i + "\" class=\"cur\">" + i + "</a>");
                    }
                    else
                    {
                        strPage.Append("<a href=\"" + PageUrl + "page=" + i + "\">" + i + "</a>");
                    }
                }
                if (uplimit + 1 <= PageCount)
                {
                    strPage.Append("<a href=\"" + PageUrl + "page=" + (uplimit + 1).ToString() + "\"  class=\"next\">...</a>\n");
                }
                if (CurrentPage != PageCount)
                {
                    strPage.Append("<a href=\"" + PageUrl + "page=" + (CurrentPage + 1).ToString() + "\"  class=\"next\">下一页</a>\n");
                    strPage.Append("<a href=\"" + PageUrl + "page=" + PageCount.ToString() + "\"  class=\"next\">尾页</a>\n");
                }
                else
                {
                    strPage.Append("<a href=\"javascript:;\"  class=\"next gray\">下一页</a>\n");
                    strPage.Append("<a href=\"javascript:;\"  class=\"next gray\">尾页</a>\n");
                }
            }
            return strPage;
        }


        /// <summary>
        /// 分页-后台
        /// </summary>
        public static StringBuilder PageStrRefsh(int PageSize, int CurrentPage, string PageUrl, int AllCount, int PageCount)
        {
            StringBuilder strPage = new StringBuilder();
            if (AllCount == 0)
            {
                strPage.Append("<p>查询无相关记录!</p>");
            }
            else
            {
                if (CurrentPage < 1) CurrentPage = 1;
                if (CurrentPage > PageCount) CurrentPage = PageCount;

                int zoomsize = 10;
                int zoom, downlimit, uplimit;
                zoom = (CurrentPage - CurrentPage % zoomsize) / zoomsize;
                if (CurrentPage % zoomsize == 0)
                {
                    zoom = zoom - 1;
                }
                downlimit = zoom * zoomsize + 1;
                uplimit = downlimit + zoomsize - 1;

                if (uplimit > PageCount)
                {
                    uplimit = PageCount;
                }
                strPage.Append("<span class=\"page-info\">共" + AllCount + "条数据 / 共" + PageCount + "页 每页<em class=\"c-ylo\">" + PageSize + "</em>条</span> ");
                if (CurrentPage != 1)
                {
                    strPage.Append("<a onclick=\"RefreshTable(1)\"  class=\"prev\">首页</a>\n");
                    strPage.Append("<a onclick=\"RefreshTable(" + (CurrentPage - 1).ToString() + ")\"  class=\"prev\">上一页</a>\n");
                }
                else
                {
                    strPage.Append("<a href=\"javascript:;\"  class=\"prev gray\">首页</a>\n");
                    strPage.Append("<a href=\"javascript:;\"  class=\"prev gray\">上一页</a>\n");
                }
                if (downlimit - 1 >= zoomsize)
                {
                    strPage.Append("<a onclick=\"RefreshTable(" + (downlimit - 1).ToString() + ")\"  class=\"prev\">...</a>\n");
                }
                for (int i = downlimit; i <= uplimit; i++)
                {
                    if (i == CurrentPage)
                    {
                        strPage.Append("<a onclick=\"RefreshTable(" + i + ")\" class=\"cur\">" + i + "</a>");
                    }
                    else
                    {
                        strPage.Append("<a onclick=\"RefreshTable(" + i + ")\">" + i + "</a>");
                    }
                }
                if (uplimit + 1 <= PageCount)
                {
                    strPage.Append("<a onclick=\"RefreshTable(" + (uplimit + 1).ToString() + ")\"  class=\"next\">...</a>\n");
                }
                if (CurrentPage != PageCount)
                {
                    strPage.Append("<a onclick=\"RefreshTable(" + (CurrentPage + 1).ToString() + ")\"  class=\"next\">下一页</a>\n");
                    strPage.Append("<a onclick=\"RefreshTable(" + PageCount.ToString() + ")\"  class=\"next\">尾页</a>\n");
                }
                else
                {
                    strPage.Append("<a href=\"javascript:;\"  class=\"next gray\">下一页</a>\n");
                    strPage.Append("<a href=\"javascript:;\"  class=\"next gray\">尾页</a>\n");
                }
            }
            return strPage;
        }

        /// <summary>
        /// 分页-中文
        /// </summary>
        public static StringBuilder PageStrCn(int PageSize, int CurrentPage, string PageUrl, int AllCount, int PageCount)
        {
            StringBuilder strPage = new StringBuilder();
            if (AllCount == 0)
            {
                strPage.Append("<p>查询无相关记录!</p>");
            }
            else
            {
                if (CurrentPage < 1) CurrentPage = 1;
                if (CurrentPage > PageCount) CurrentPage = PageCount;

                strPage.Append("共<b>" + PageCount + "</b>页 第<b>" + CurrentPage + "</b>页 <b>" + PageSize + "</b>条/页 共<b>" + AllCount + "</b>条记录\n");
                if (CurrentPage == 1)
                {
                    strPage.Append("<span>首页 上一页</span>\n");
                }
                else
                {
                    strPage.Append("<a href=\"" + PageUrl + "page=1\">首页</a>\n");
                    strPage.Append("<a href=\"" + PageUrl + "page=" + (CurrentPage - 1).ToString() + "\">上一页</a>\n");
                }
                if (CurrentPage == PageCount)
                {
                    strPage.Append("<span>下一页 尾页</span>\n");
                }
                else
                {
                    strPage.Append("<a href=\"" + PageUrl + "page=" + (CurrentPage + 1).ToString() + "\">下一页</a>\n");
                    strPage.Append("<a href=\"" + PageUrl + "page=" + PageCount + "\">尾页</a>\n");
                }
                strPage.Append("跳到<select onchange=\"javascript:location.href='" + PageUrl + "page='+this.options[this.selectedIndex].value\">\n");
                for (int i = 1; i <= PageCount; i++)
                {
                    string sel = "";
                    if (i == CurrentPage) sel = "selected";
                    strPage.Append("<option value=\"" + i + "\" " + sel + ">" + i + "</option>\n");
                }
                strPage.Append("</select>页\n");
            }
            return strPage;
        }
        /// <summary>
        /// 分页-数字
        /// </summary>
        public static StringBuilder PageStrNum(int PageSize, int CurrentPage, string PageUrl, int AllCount, int PageCount)
        {
            StringBuilder strPage = new StringBuilder();
            if (AllCount == 0)
            {
                strPage.Append("<p>查询无相关记录!</p>");
            }
            else
            {
                if (CurrentPage < 1) CurrentPage = 1;
                if (CurrentPage > PageCount) CurrentPage = PageCount;

                int zoomsize = 5;
                int zoom, downlimit, uplimit;
                zoom = (CurrentPage - CurrentPage % zoomsize) / zoomsize;
                if (CurrentPage % zoomsize == 0)
                {
                    zoom = zoom - 1;
                }
                downlimit = zoom * zoomsize + 1;
                uplimit = downlimit + zoomsize - 1;

                if (uplimit > PageCount)
                {
                    uplimit = PageCount;
                }
                //strPage.Append("<h1>总计" + AllCount + "个记录</h1>");
                //strPage.Append("<a class=\"info\">" + CurrentPage + "/" + PageCount + "</a>");
                //strPage.Append("<ul style=\"float:left\">");
                if (CurrentPage != 1)
                {
                    //strPage.Append("<a href=\"" + PageUrl + "page=1\"  class=\"prev\">首页</a>\n");
                    strPage.Append("<a href=\"" + PageUrl + "page=" + (CurrentPage - 1).ToString() + "\"  class=\"prev\">上一页</a>\n");
                }
                else
                {
                    //strPage.Append("<a href=\"javascript:;\"  class=\"prev disable\">首页</a>\n");
                    strPage.Append("<a href=\"javascript:;\"  class=\"prev disable\">上一页</a>\n");
                }
                if (downlimit - 1 >= zoomsize)
                {
                    strPage.Append("<a href=\"" + PageUrl + "page=" + (downlimit - 1).ToString() + "\"  class=\"prev\">...</a>\n");
                }
                for (int i = downlimit; i <= uplimit; i++)
                {
                    if (i == CurrentPage)
                    {
                        //strPage.Append("<li><a href=\"" + PageUrl + "page=" + i + "\" class=\"selected\">" + i + "</a></li>");
                        strPage.Append("<a href=\"" + PageUrl + "page=" + i + "\" class=\"active\">" + i + "</a>");
                    }
                    else
                    {
                        //strPage.Append("<li><a href=\"" + PageUrl + "page=" + i + "\">" + i + "</a></li>");
                        strPage.Append("<a href=\"" + PageUrl + "page=" + i + "\">" + i + "</a>");
                    }
                }
                if (uplimit + 1 <= PageCount)
                {
                    strPage.Append("<a href=\"" + PageUrl + "page=" + (uplimit + 1).ToString() + "\"  class=\"next\">...</a>\n");
                }
                if (CurrentPage != PageCount)
                {
                    strPage.Append("<a href=\"" + PageUrl + "page=" + (CurrentPage + 1).ToString() + "\"  class=\"next\">下一页</a>\n");
                    //strPage.Append("<a href=\"" + PageUrl + "page=" + PageCount.ToString() + "\"  class=\"next\">尾页</a>\n");
                }
                else
                {
                    strPage.Append("<a href=\"javascript:;\"  class=\"next disable\">下一页</a>\n");
                    //strPage.Append("<a href=\"javascript:;\"  class=\"next disable\">尾页</a>\n");
                }
                //strPage.Append("</ul>");
                //strPage.Append(" <span>共<b>" + AllCount + "</b>条</span> 到第");
                //strPage.Append(" <input class=\"num\" type=\"text\" id=\"pageid\" name=\"page\" value=\"" + CurrentPage + "\">页");
                //strPage.Append(" <input class=\"btn\" type=\"button\" value=\"确认\" onclick=\"javascript:location.href='" + PageUrl + "page='+document.getElementById('pageid').value;\">");
                strPage.Append("<span>共" + PageCount + "页</span>");
                strPage.Append("<span>跳转至</span>");
                strPage.Append("<input class=\"pagesInput num\" type=\"text\" id=\"pageid\" name=\"page\" value=\"" + CurrentPage + "\">");
                strPage.Append("<button class=\"ok\"  type=\"button\" onclick=\"javascript:location.href='" + PageUrl + "page='+document.getElementById('pageid').value;\">确认</button>");
            }
            return strPage;
        }
        #endregion

        #region 泛型数据分页
        /// <summary>
        /// 泛型数据分页
        /// </summary>
        /// <returns></returns>
        public static List<T> GetListPage<T>(List<T> list, int PageSize, int CurrentPage, string PageUrl, EnumList.PageStrType PageStrType, out StringBuilder PageStr)
        {
            int AllCount = list.Count;
            int PageCount = AllCount % PageSize == 0 ? AllCount / PageSize : AllCount / PageSize + 1;
            if (CurrentPage < 1) CurrentPage = 1;
            if (CurrentPage > PageCount) CurrentPage = PageCount;
            List<T> temp_list = list.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
            StringBuilder strPage = new StringBuilder();
            switch (PageStrType)
            {
                case EnumList.PageStrType.Ma:
                    strPage = PageHelper.PageStrMa(PageSize, CurrentPage, PageUrl, AllCount, PageCount);
                    break;
                case EnumList.PageStrType.Cn:
                    strPage = PageHelper.PageStrCn(PageSize, CurrentPage, PageUrl, AllCount, PageCount);
                    break;
                case EnumList.PageStrType.Num:
                    strPage = PageHelper.PageStrNum(PageSize, CurrentPage, PageUrl, AllCount, PageCount);
                    break;
                default:
                    break;
            }
            PageStr = strPage;
            return temp_list;
        }
        #endregion
    }
}
