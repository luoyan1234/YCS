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
/// 广告-业务逻辑类
/// 创建人:
/// 日期:2017/4/7 18:48:03 +08:00
/// </summary>

public class  Ad
{

private readonly AdDAL adDAL=new AdDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,int AdId)
{
return adDAL.CheckInfo(trans,strFieldName, strFieldValue,AdId);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, int AdId)
{
return adDAL.GetValueByField(trans,strFieldName, AdId);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public AdModel GetInfo(SqlTransaction trans,int AdId)
{
return adDAL.GetInfo(trans,AdId);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public AdModel GetCacheInfo(SqlTransaction trans,int AdId)
{
string key="Cache_Ad_Model_"+AdId;
object value = CacheHelper.GetCache(key);
if (value != null)
return (AdModel)value;
else
{
AdModel adModel = adDAL.GetInfo(trans,AdId);
CacheHelper.AddCache(key, adModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return adModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,AdModel adModel)
{
return adDAL.InsertInfo(trans,adModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,AdModel adModel,int AdId)
{
string key="Cache_Ad_Model_"+AdId;
CacheHelper.RemoveCache(key);
return adDAL.UpdateInfo(trans,adModel,AdId);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,int AdId)
{
string key="Cache_Ad_Model_"+AdId;
CacheHelper.RemoveCache(key);
return adDAL.DeleteInfo(trans,AdId);
}
#endregion

}
}
