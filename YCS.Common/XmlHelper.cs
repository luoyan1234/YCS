using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace YCS.Common
{
    /// <summary>
    /// XML操作类
    /// </summary>
    public class XmlHelper
    {
        #region 从字符串中取XML根元素
        /// <summary>
        /// 从字符串中XML根元素
        /// </summary>
        /// <param name="context"></param>
        /// <param name="strXml"></param>
        /// <returns></returns>
        public static XmlElement GetXmlElement(string strXml)
        {
            if (!string.IsNullOrEmpty(strXml))
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(strXml);
                XmlElement element = doc.DocumentElement;
                return element;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 从文档取XML根元素
        /// <summary>
        /// 从文档取XML根元素
        /// </summary>
        /// <param name="strFilePath"></param>
        /// <returns></returns>
        public static XmlElement GetXmlElementByFile(string strFilePath)
        {
            if (File.Exists(strFilePath))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(strFilePath);
                XmlElement element = doc.DocumentElement;
                return element;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 取XML节点文本
        /// <summary>
        /// 取XML节点文本
        /// </summary>
        /// <param name="element"></param>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public static string GetXmlNodeText(XmlElement element, string xpath)
        {
            XmlNode node = element.SelectSingleNode(xpath);
            return node != null ? node.InnerText : "";
        }
        #endregion

        #region 取XML元素属性值
        /// <summary>
        /// 取XML元素属性值
        /// </summary>
        /// <param name="element"></param>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public static string GetAttributesValue(XmlElement element, string key)
        {
            XmlAttribute attr = element.Attributes[key];
            return attr != null ? attr.Value : "";
        }
        #endregion

        #region 取XML节点属性值
        /// <summary>
        /// 取XML节点属性值
        /// </summary>
        /// <param name="node"></param>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public static string GetAttributesValue(XmlNode node, string key)
        {
            XmlAttribute attr = node.Attributes[key];
            return attr != null ? attr.Value : "";
        }
        #endregion    
    }
}
