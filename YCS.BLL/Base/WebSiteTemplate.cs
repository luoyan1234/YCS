using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;
using System.Web.Caching;
using YCS.Common;
using YCS.Model;
using YCS.DAL;

namespace YCS.BLL.Base
{
/// <summary>
/// 网站版型风格档-业务逻辑类
/// 创建人:
/// 日期:2017/4/7 18:48:08 +08:00
/// </summary>

public class  WebSiteTemplate
{

private readonly WebSiteTemplateDAL webDAL=new WebSiteTemplateDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,long SN)
{
return webDAL.CheckInfo(trans,strFieldName, strFieldValue,SN);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, long SN)
{
return webDAL.GetValueByField(trans,strFieldName, SN);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public WebSiteTemplateModel GetInfo(SqlTransaction trans,long SN)
{
return webDAL.GetInfo(trans,SN);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public WebSiteTemplateModel GetCacheInfo(SqlTransaction trans,long SN)
{
string key="Cache_WebSiteTemplate_Model_"+SN;
object value = CacheHelper.GetCache(key);
if (value != null)
return (WebSiteTemplateModel)value;
else
{
WebSiteTemplateModel webModel = webDAL.GetInfo(trans,SN);
CacheHelper.AddCache(key, webModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return webModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,WebSiteTemplateModel webModel)
{
return webDAL.InsertInfo(trans,webModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,WebSiteTemplateModel webModel,long SN)
{
string key="Cache_WebSiteTemplate_Model_"+SN;
CacheHelper.RemoveCache(key);
return webDAL.UpdateInfo(trans,webModel,SN);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,long SN)
{
string key="Cache_WebSiteTemplate_Model_"+SN;
CacheHelper.RemoveCache(key);
return webDAL.DeleteInfo(trans,SN);
}
#endregion

}
}
