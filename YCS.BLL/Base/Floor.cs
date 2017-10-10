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
/// 楼层-业务逻辑类
/// 创建人:
/// 日期:2017/4/7 18:48:04 +08:00
/// </summary>

public class  Floor
{

private readonly FloorDAL floDAL=new FloorDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,int FloorId)
{
return floDAL.CheckInfo(trans,strFieldName, strFieldValue,FloorId);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, int FloorId)
{
return floDAL.GetValueByField(trans,strFieldName, FloorId);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public FloorModel GetInfo(SqlTransaction trans,int FloorId)
{
return floDAL.GetInfo(trans,FloorId);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public FloorModel GetCacheInfo(SqlTransaction trans,int FloorId)
{
string key="Cache_Floor_Model_"+FloorId;
object value = CacheHelper.GetCache(key);
if (value != null)
return (FloorModel)value;
else
{
FloorModel floModel = floDAL.GetInfo(trans,FloorId);
CacheHelper.AddCache(key, floModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return floModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,FloorModel floModel)
{
return floDAL.InsertInfo(trans,floModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,FloorModel floModel,int FloorId)
{
string key="Cache_Floor_Model_"+FloorId;
CacheHelper.RemoveCache(key);
return floDAL.UpdateInfo(trans,floModel,FloorId);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,int FloorId)
{
string key="Cache_Floor_Model_"+FloorId;
CacheHelper.RemoveCache(key);
return floDAL.DeleteInfo(trans,FloorId);
}
#endregion

}
}
