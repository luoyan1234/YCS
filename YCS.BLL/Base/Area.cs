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
/// 地区-业务逻辑类
/// 创建人:
/// 日期:2017/4/7 18:48:03 +08:00
/// </summary>

public class  Area
{

private readonly AreaDAL areDAL=new AreaDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,int AreaId)
{
return areDAL.CheckInfo(trans,strFieldName, strFieldValue,AreaId);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, int AreaId)
{
return areDAL.GetValueByField(trans,strFieldName, AreaId);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public AreaModel GetInfo(SqlTransaction trans,int AreaId)
{
return areDAL.GetInfo(trans,AreaId);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public AreaModel GetCacheInfo(SqlTransaction trans,int AreaId)
{
string key="Cache_Area_Model_"+AreaId;
object value = CacheHelper.GetCache(key);
if (value != null)
return (AreaModel)value;
else
{
AreaModel areModel = areDAL.GetInfo(trans,AreaId);
CacheHelper.AddCache(key, areModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return areModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,AreaModel areModel)
{
return areDAL.InsertInfo(trans,areModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,AreaModel areModel,int AreaId)
{
string key="Cache_Area_Model_"+AreaId;
CacheHelper.RemoveCache(key);
return areDAL.UpdateInfo(trans,areModel,AreaId);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,int AreaId)
{
string key="Cache_Area_Model_"+AreaId;
CacheHelper.RemoveCache(key);
return areDAL.DeleteInfo(trans,AreaId);
}
#endregion

}
}
