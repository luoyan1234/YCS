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
/// 訂單歷史紀錄表			-业务逻辑类
/// 创建人:
/// 日期:2017/4/7 18:48:05 +08:00
/// </summary>

public class  OrderHistory
{

private readonly OrderHistoryDAL ordDAL=new OrderHistoryDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,long SN)
{
return ordDAL.CheckInfo(trans,strFieldName, strFieldValue,SN);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, long SN)
{
return ordDAL.GetValueByField(trans,strFieldName, SN);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public OrderHistoryModel GetInfo(SqlTransaction trans,long SN)
{
return ordDAL.GetInfo(trans,SN);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public OrderHistoryModel GetCacheInfo(SqlTransaction trans,long SN)
{
string key="Cache_OrderHistory_Model_"+SN;
object value = CacheHelper.GetCache(key);
if (value != null)
return (OrderHistoryModel)value;
else
{
OrderHistoryModel ordModel = ordDAL.GetInfo(trans,SN);
CacheHelper.AddCache(key, ordModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return ordModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,OrderHistoryModel ordModel)
{
return ordDAL.InsertInfo(trans,ordModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,OrderHistoryModel ordModel,long SN)
{
string key="Cache_OrderHistory_Model_"+SN;
CacheHelper.RemoveCache(key);
return ordDAL.UpdateInfo(trans,ordModel,SN);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,long SN)
{
string key="Cache_OrderHistory_Model_"+SN;
CacheHelper.RemoveCache(key);
return ordDAL.DeleteInfo(trans,SN);
}
#endregion

}
}
