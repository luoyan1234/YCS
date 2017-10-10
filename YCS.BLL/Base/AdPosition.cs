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
/// 广告位-业务逻辑类
/// 创建人:
/// 日期:2017/4/7 18:48:03 +08:00
/// </summary>

public class  AdPosition
{

private readonly AdPositionDAL adpDAL=new AdPositionDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,int AdPositionId)
{
return adpDAL.CheckInfo(trans,strFieldName, strFieldValue,AdPositionId);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, int AdPositionId)
{
return adpDAL.GetValueByField(trans,strFieldName, AdPositionId);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public AdPositionModel GetInfo(SqlTransaction trans,int AdPositionId)
{
return adpDAL.GetInfo(trans,AdPositionId);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public AdPositionModel GetCacheInfo(SqlTransaction trans,int AdPositionId)
{
string key="Cache_AdPosition_Model_"+AdPositionId;
object value = CacheHelper.GetCache(key);
if (value != null)
return (AdPositionModel)value;
else
{
AdPositionModel adpModel = adpDAL.GetInfo(trans,AdPositionId);
CacheHelper.AddCache(key, adpModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return adpModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,AdPositionModel adpModel)
{
return adpDAL.InsertInfo(trans,adpModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,AdPositionModel adpModel,int AdPositionId)
{
string key="Cache_AdPosition_Model_"+AdPositionId;
CacheHelper.RemoveCache(key);
return adpDAL.UpdateInfo(trans,adpModel,AdPositionId);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,int AdPositionId)
{
string key="Cache_AdPosition_Model_"+AdPositionId;
CacheHelper.RemoveCache(key);
return adpDAL.DeleteInfo(trans,AdPositionId);
}
#endregion

}
}
