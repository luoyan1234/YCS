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
/// 配送费用-业务逻辑类
/// 创建人:
/// 日期:2017/4/7 18:48:04 +08:00
/// </summary>

public class  DeliverCost
{

private readonly DeliverCostDAL delDAL=new DeliverCostDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,int DeliverCostId)
{
return delDAL.CheckInfo(trans,strFieldName, strFieldValue,DeliverCostId);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, int DeliverCostId)
{
return delDAL.GetValueByField(trans,strFieldName, DeliverCostId);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public DeliverCostModel GetInfo(SqlTransaction trans,int DeliverCostId)
{
return delDAL.GetInfo(trans,DeliverCostId);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public DeliverCostModel GetCacheInfo(SqlTransaction trans,int DeliverCostId)
{
string key="Cache_DeliverCost_Model_"+DeliverCostId;
object value = CacheHelper.GetCache(key);
if (value != null)
return (DeliverCostModel)value;
else
{
DeliverCostModel delModel = delDAL.GetInfo(trans,DeliverCostId);
CacheHelper.AddCache(key, delModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return delModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,DeliverCostModel delModel)
{
return delDAL.InsertInfo(trans,delModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,DeliverCostModel delModel,int DeliverCostId)
{
string key="Cache_DeliverCost_Model_"+DeliverCostId;
CacheHelper.RemoveCache(key);
return delDAL.UpdateInfo(trans,delModel,DeliverCostId);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,int DeliverCostId)
{
string key="Cache_DeliverCost_Model_"+DeliverCostId;
CacheHelper.RemoveCache(key);
return delDAL.DeleteInfo(trans,DeliverCostId);
}
#endregion

}
}
