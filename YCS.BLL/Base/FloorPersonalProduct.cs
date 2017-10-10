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
/// 楼层个性化商品设置-业务逻辑类
/// 创建人:
/// 日期:2017/4/7 18:48:04 +08:00
/// </summary>

public class  FloorPersonalProduct
{

private readonly FloorPersonalProductDAL floDAL=new FloorPersonalProductDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,int FloorPersonalProductId)
{
return floDAL.CheckInfo(trans,strFieldName, strFieldValue,FloorPersonalProductId);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, int FloorPersonalProductId)
{
return floDAL.GetValueByField(trans,strFieldName, FloorPersonalProductId);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public FloorPersonalProductModel GetInfo(SqlTransaction trans,int FloorPersonalProductId)
{
return floDAL.GetInfo(trans,FloorPersonalProductId);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public FloorPersonalProductModel GetCacheInfo(SqlTransaction trans,int FloorPersonalProductId)
{
string key="Cache_FloorPersonalProduct_Model_"+FloorPersonalProductId;
object value = CacheHelper.GetCache(key);
if (value != null)
return (FloorPersonalProductModel)value;
else
{
FloorPersonalProductModel floModel = floDAL.GetInfo(trans,FloorPersonalProductId);
CacheHelper.AddCache(key, floModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return floModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,FloorPersonalProductModel floModel)
{
return floDAL.InsertInfo(trans,floModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,FloorPersonalProductModel floModel,int FloorPersonalProductId)
{
string key="Cache_FloorPersonalProduct_Model_"+FloorPersonalProductId;
CacheHelper.RemoveCache(key);
return floDAL.UpdateInfo(trans,floModel,FloorPersonalProductId);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,int FloorPersonalProductId)
{
string key="Cache_FloorPersonalProduct_Model_"+FloorPersonalProductId;
CacheHelper.RemoveCache(key);
return floDAL.DeleteInfo(trans,FloorPersonalProductId);
}
#endregion

}
}
