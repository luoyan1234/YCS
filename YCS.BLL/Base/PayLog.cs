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
/// 支付日志-业务逻辑类
/// 创建人:
/// 日期:2017/4/7 18:48:06 +08:00
/// </summary>

public class  PayLog
{

private readonly PayLogDAL payDAL=new PayLogDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,int PayLogId)
{
return payDAL.CheckInfo(trans,strFieldName, strFieldValue,PayLogId);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, int PayLogId)
{
return payDAL.GetValueByField(trans,strFieldName, PayLogId);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public PayLogModel GetInfo(SqlTransaction trans,int PayLogId)
{
return payDAL.GetInfo(trans,PayLogId);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public PayLogModel GetCacheInfo(SqlTransaction trans,int PayLogId)
{
string key="Cache_PayLog_Model_"+PayLogId;
object value = CacheHelper.GetCache(key);
if (value != null)
return (PayLogModel)value;
else
{
PayLogModel payModel = payDAL.GetInfo(trans,PayLogId);
CacheHelper.AddCache(key, payModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return payModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,PayLogModel payModel)
{
return payDAL.InsertInfo(trans,payModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,PayLogModel payModel,int PayLogId)
{
string key="Cache_PayLog_Model_"+PayLogId;
CacheHelper.RemoveCache(key);
return payDAL.UpdateInfo(trans,payModel,PayLogId);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,int PayLogId)
{
string key="Cache_PayLog_Model_"+PayLogId;
CacheHelper.RemoveCache(key);
return payDAL.DeleteInfo(trans,PayLogId);
}
#endregion

}
}
