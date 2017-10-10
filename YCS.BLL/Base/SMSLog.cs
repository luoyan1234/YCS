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
/// 短信日志-业务逻辑类
/// 创建人:
/// 日期:2017/4/7 18:48:07 +08:00
/// </summary>

public class  SMSLog
{

private readonly SMSLogDAL smsDAL=new SMSLogDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,int SMSLogId)
{
return smsDAL.CheckInfo(trans,strFieldName, strFieldValue,SMSLogId);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, int SMSLogId)
{
return smsDAL.GetValueByField(trans,strFieldName, SMSLogId);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public SMSLogModel GetInfo(SqlTransaction trans,int SMSLogId)
{
return smsDAL.GetInfo(trans,SMSLogId);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public SMSLogModel GetCacheInfo(SqlTransaction trans,int SMSLogId)
{
string key="Cache_SMSLog_Model_"+SMSLogId;
object value = CacheHelper.GetCache(key);
if (value != null)
return (SMSLogModel)value;
else
{
SMSLogModel smsModel = smsDAL.GetInfo(trans,SMSLogId);
CacheHelper.AddCache(key, smsModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return smsModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,SMSLogModel smsModel)
{
return smsDAL.InsertInfo(trans,smsModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,SMSLogModel smsModel,int SMSLogId)
{
string key="Cache_SMSLog_Model_"+SMSLogId;
CacheHelper.RemoveCache(key);
return smsDAL.UpdateInfo(trans,smsModel,SMSLogId);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,int SMSLogId)
{
string key="Cache_SMSLog_Model_"+SMSLogId;
CacheHelper.RemoveCache(key);
return smsDAL.DeleteInfo(trans,SMSLogId);
}
#endregion

}
}
