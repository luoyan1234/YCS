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
/// 购物车-业务逻辑类
/// 创建人:
/// 日期:2017/4/7 18:48:03 +08:00
/// </summary>

public class  Cart
{

private readonly CartDAL carDAL=new CartDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,int CartId)
{
return carDAL.CheckInfo(trans,strFieldName, strFieldValue,CartId);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, int CartId)
{
return carDAL.GetValueByField(trans,strFieldName, CartId);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public CartModel GetInfo(SqlTransaction trans,int CartId)
{
return carDAL.GetInfo(trans,CartId);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public CartModel GetCacheInfo(SqlTransaction trans,int CartId)
{
string key="Cache_Cart_Model_"+CartId;
object value = CacheHelper.GetCache(key);
if (value != null)
return (CartModel)value;
else
{
CartModel carModel = carDAL.GetInfo(trans,CartId);
CacheHelper.AddCache(key, carModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return carModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,CartModel carModel)
{
return carDAL.InsertInfo(trans,carModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,CartModel carModel,int CartId)
{
string key="Cache_Cart_Model_"+CartId;
CacheHelper.RemoveCache(key);
return carDAL.UpdateInfo(trans,carModel,CartId);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,int CartId)
{
string key="Cache_Cart_Model_"+CartId;
CacheHelper.RemoveCache(key);
return carDAL.DeleteInfo(trans,CartId);
}
#endregion

}
}
