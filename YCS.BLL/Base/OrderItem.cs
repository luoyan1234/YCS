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
/// 订单产品-业务逻辑类
/// 创建人:杨小明
/// 日期:2017/5/10 14:40:16 +08:00
/// </summary>

public class  OrderItem
{

private readonly OrderItemDAL ordItemDAL=new OrderItemDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,long SN)
{
return ordItemDAL.CheckInfo(trans,strFieldName, strFieldValue,SN);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, long SN)
{
return ordItemDAL.GetValueByField(trans,strFieldName, SN);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public OrderItemModel GetInfo(SqlTransaction trans,long SN)
{
return ordItemDAL.GetInfo(trans,SN);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public OrderItemModel GetCacheInfo(SqlTransaction trans,long SN)
{
string key="Cache_OrderItem_Model_"+SN;
object value = CacheHelper.GetCache(key);
if (value != null)
return (OrderItemModel)value;
else
{
OrderItemModel ordItemModel = ordItemDAL.GetInfo(trans,SN);
CacheHelper.AddCache(key, ordItemModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return ordItemModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,OrderItemModel ordItemModel)
{
return ordItemDAL.InsertInfo(trans,ordItemModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,OrderItemModel ordItemModel,long SN)
{
string key="Cache_OrderItem_Model_"+SN;
CacheHelper.RemoveCache(key);
return ordItemDAL.UpdateInfo(trans,ordItemModel,SN);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,long SN)
{
string key="Cache_OrderItem_Model_"+SN;
CacheHelper.RemoveCache(key);
return ordItemDAL.DeleteInfo(trans,SN);
}
#endregion

}
}
