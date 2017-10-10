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
/// 訂單紀錄表-业务逻辑类
/// 创建人:
/// 日期:2017/4/7 18:48:05 +08:00
/// </summary>

public class  Order
{

private readonly OrderDAL ordDAL=new OrderDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,long OrderId)
{
return ordDAL.CheckInfo(trans,strFieldName, strFieldValue,OrderId);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, long OrderId)
{
return ordDAL.GetValueByField(trans,strFieldName, OrderId);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public OrderModel GetInfo(SqlTransaction trans,long OrderId)
{
return ordDAL.GetInfo(trans,OrderId);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public OrderModel GetCacheInfo(SqlTransaction trans,long OrderId)
{
string key="Cache_Order_Model_"+OrderId;
object value = CacheHelper.GetCache(key);
if (value != null)
return (OrderModel)value;
else
{
OrderModel ordModel = ordDAL.GetInfo(trans,OrderId);
CacheHelper.AddCache(key, ordModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return ordModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,OrderModel ordModel)
{
return ordDAL.InsertInfo(trans,ordModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,OrderModel ordModel,long OrderId)
{
string key="Cache_Order_Model_"+OrderId;
CacheHelper.RemoveCache(key);
return ordDAL.UpdateInfo(trans,ordModel,OrderId);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,long OrderId)
{
string key="Cache_Order_Model_"+OrderId;
CacheHelper.RemoveCache(key);
return ordDAL.DeleteInfo(trans,OrderId);
}
#endregion

}
}
