﻿using System;
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
/// 配送方式-业务逻辑类
/// 创建人:
/// 日期:2017/4/7 18:48:04 +08:00
/// </summary>

public class  Deliver
{

private readonly DeliverDAL delDAL=new DeliverDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,int DeliverId)
{
return delDAL.CheckInfo(trans,strFieldName, strFieldValue,DeliverId);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, int DeliverId)
{
return delDAL.GetValueByField(trans,strFieldName, DeliverId);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public DeliverModel GetInfo(SqlTransaction trans,int DeliverId)
{
return delDAL.GetInfo(trans,DeliverId);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public DeliverModel GetCacheInfo(SqlTransaction trans,int DeliverId)
{
string key="Cache_Deliver_Model_"+DeliverId;
object value = CacheHelper.GetCache(key);
if (value != null)
return (DeliverModel)value;
else
{
DeliverModel delModel = delDAL.GetInfo(trans,DeliverId);
CacheHelper.AddCache(key, delModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return delModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,DeliverModel delModel)
{
return delDAL.InsertInfo(trans,delModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,DeliverModel delModel,int DeliverId)
{
string key="Cache_Deliver_Model_"+DeliverId;
CacheHelper.RemoveCache(key);
return delDAL.UpdateInfo(trans,delModel,DeliverId);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,int DeliverId)
{
string key="Cache_Deliver_Model_"+DeliverId;
CacheHelper.RemoveCache(key);
return delDAL.DeleteInfo(trans,DeliverId);
}
#endregion

}
}
