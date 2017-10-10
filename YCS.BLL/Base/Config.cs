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
/// 配置类-业务逻辑类
/// 创建人:luoy
/// 日期:2017/4/17 10:17:56 +08:00
/// </summary>

public class  Config
{

private readonly ConfigDAL conDAL=new ConfigDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,int ConfigId)
{
return conDAL.CheckInfo(trans,strFieldName, strFieldValue,ConfigId);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, int ConfigId)
{
return conDAL.GetValueByField(trans,strFieldName, ConfigId);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public ConfigModel GetInfo(SqlTransaction trans,int ConfigId)
{
return conDAL.GetInfo(trans,ConfigId);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public ConfigModel GetCacheInfo(SqlTransaction trans,int ConfigId)
{
string key="Cache_Config_Model_"+ConfigId;
object value = CacheHelper.GetCache(key);
if (value != null)
return (ConfigModel)value;
else
{
ConfigModel conModel = conDAL.GetInfo(trans,ConfigId);
CacheHelper.AddCache(key, conModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return conModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,ConfigModel conModel)
{
return conDAL.InsertInfo(trans,conModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,ConfigModel conModel,int ConfigId)
{
string key="Cache_Config_Model_"+ConfigId;
CacheHelper.RemoveCache(key);
return conDAL.UpdateInfo(trans,conModel,ConfigId);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,int ConfigId)
{
string key="Cache_Config_Model_"+ConfigId;
CacheHelper.RemoveCache(key);
return conDAL.DeleteInfo(trans,ConfigId);
}
#endregion

}
}
