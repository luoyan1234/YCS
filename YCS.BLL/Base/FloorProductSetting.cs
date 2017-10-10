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
/// 楼层热卖推荐商品设置-业务逻辑类
/// 创建人:
/// 日期:2017/4/7 18:48:04 +08:00
/// </summary>

public class  FloorProductSetting
{

private readonly FloorProductSettingDAL floDAL=new FloorProductSettingDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,int FloorProductSettingId)
{
return floDAL.CheckInfo(trans,strFieldName, strFieldValue,FloorProductSettingId);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, int FloorProductSettingId)
{
return floDAL.GetValueByField(trans,strFieldName, FloorProductSettingId);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public FloorProductSettingModel GetInfo(SqlTransaction trans,int FloorProductSettingId)
{
return floDAL.GetInfo(trans,FloorProductSettingId);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public FloorProductSettingModel GetCacheInfo(SqlTransaction trans,int FloorProductSettingId)
{
string key="Cache_FloorProductSetting_Model_"+FloorProductSettingId;
object value = CacheHelper.GetCache(key);
if (value != null)
return (FloorProductSettingModel)value;
else
{
FloorProductSettingModel floModel = floDAL.GetInfo(trans,FloorProductSettingId);
CacheHelper.AddCache(key, floModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return floModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,FloorProductSettingModel floModel)
{
return floDAL.InsertInfo(trans,floModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,FloorProductSettingModel floModel,int FloorProductSettingId)
{
string key="Cache_FloorProductSetting_Model_"+FloorProductSettingId;
CacheHelper.RemoveCache(key);
return floDAL.UpdateInfo(trans,floModel,FloorProductSettingId);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,int FloorProductSettingId)
{
string key="Cache_FloorProductSetting_Model_"+FloorProductSettingId;
CacheHelper.RemoveCache(key);
return floDAL.DeleteInfo(trans,FloorProductSettingId);
}
#endregion

}
}
