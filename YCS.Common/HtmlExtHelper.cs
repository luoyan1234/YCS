using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace YCS.Common
{
    /// <summary>
    /// Html辅助扩展类
    /// </summary>
    public static class HtmlExtHelper
    {
        #region 单选框通用方法（枚举）
        /// <summary>
        /// 单选框通用方法（枚举）
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="value">选中值</param>
        /// <param name="objEnum">枚举</param>
        /// <param name="strName">控件名</param>
        /// <param name="bolSel">是否是查询</param>
        /// <returns></returns>
        public static MvcHtmlString RadioStatus(this HtmlHelper helper, int value, Type objEnum, string strName, bool bolSel)
        {
            StringBuilder strHtml = new StringBuilder();
            if (bolSel)
            {
                strHtml.AppendFormat("{0}", Config.Radio(strName, "不限", "", "checked=\"checked\""));
            }
            foreach (var item in Config.GetEnumList(objEnum).OrderBy(f => f.Value))
            {
                string strChecked = item.Value == value ? "checked=\"checked\"" : "";
                strHtml.AppendFormat("{0}", Config.Radio(strName, item.Key, item.Value, strChecked));
            }
            return MvcHtmlString.Create(strHtml.ToString());
        }
        /// <summary>
        /// 单选框通用方法（枚举）
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="value">选中值</param>
        /// <param name="objEnum">枚举</param>
        /// <param name="strName">控件名</param>
        /// <param name="bolSel">是否是查询</param>
        /// <returns></returns>
        public static MvcHtmlString RadioStatus(this HtmlHelper helper, bool value, Type objEnum, string strName, bool bolSel)
        {
            StringBuilder strHtml = new StringBuilder();
            if (bolSel)
            {
                strHtml.AppendFormat("{0}", Config.Radio(strName, "不限", "", "checked=\"checked\""));
            }
            foreach (var item in Config.GetEnumList(objEnum).OrderBy(f => f.Value))
            {
                string strChecked = Convert.ToBoolean(item.Value) == value ? "checked=\"checked\"" : "";
                strHtml.AppendFormat("{0}", Config.RadioForBool(strName, item.Key, item.Value, strChecked));
            }
            return MvcHtmlString.Create(strHtml.ToString());
        }
        #endregion

        #region 复选框通用方法（枚举）
        /// <summary>
        /// 复选框通用方法（枚举）
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="value">选中值</param>
        /// <param name="objEnum">枚举</param>
        /// <param name="strName">控件名</param>
        /// <returns></returns>
        public static MvcHtmlString CheckBoxStatus(this HtmlHelper helper, int value, Type objEnum, string strName)
        {
            StringBuilder strHtml = new StringBuilder();
            foreach (var item in Config.GetEnumList(objEnum).OrderBy(f => f.Value))
            {
                string strChecked = item.Value == value ? "checked=\"checked\"" : "";
                strHtml.AppendFormat("{0}", Config.CheckBox(strName, item.Key, item.Value, strChecked));
            }
            return MvcHtmlString.Create(strHtml.ToString());
        }

        /// <summary>
        /// 复选框通用方法（枚举）
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="value">选中值</param>
        /// <param name="objEnum">枚举</param>
        /// <param name="strName">控件名</param>
        /// <returns></returns>
        public static MvcHtmlString CheckBoxStatus(this HtmlHelper helper, string values, Type objEnum, string strName)
        {
            StringBuilder strHtml = new StringBuilder();
            foreach (var item in Config.GetEnumList(objEnum).OrderBy(f => f.Value))
            {
                string strChecked = values.Contains("," + item.Value.ToString() + ",") ? "checked=\"checked\"" : "";
                strHtml.AppendFormat("{0}", Config.CheckBox(strName, item.Key, item.Value, strChecked));
            }
            return MvcHtmlString.Create(strHtml.ToString());
        }
        #endregion
    }
}