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
/// 短信模板表-业务逻辑类
/// 创建人:
/// 日期:2017/4/7 18:48:07 +08:00
/// </summary>

public class  SMSTemplate
{

private readonly SMSTemplateDAL smsDAL=new SMSTemplateDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,int SMSTemplateId)
{
return smsDAL.CheckInfo(trans,strFieldName, strFieldValue,SMSTemplateId);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, int SMSTemplateId)
{
return smsDAL.GetValueByField(trans,strFieldName, SMSTemplateId);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public SMSTemplateModel GetInfo(SqlTransaction trans,int SMSTemplateId)
{
return smsDAL.GetInfo(trans,SMSTemplateId);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public SMSTemplateModel GetCacheInfo(SqlTransaction trans,int SMSTemplateId)
{
string key="Cache_SMSTemplate_Model_"+SMSTemplateId;
object value = CacheHelper.GetCache(key);
if (value != null)
return (SMSTemplateModel)value;
else
{
SMSTemplateModel smsModel = smsDAL.GetInfo(trans,SMSTemplateId);
CacheHelper.AddCache(key, smsModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return smsModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,SMSTemplateModel smsModel)
{
return smsDAL.InsertInfo(trans,smsModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,SMSTemplateModel smsModel,int SMSTemplateId)
{
string key="Cache_SMSTemplate_Model_"+SMSTemplateId;
CacheHelper.RemoveCache(key);
return smsDAL.UpdateInfo(trans,smsModel,SMSTemplateId);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,int SMSTemplateId)
{
string key="Cache_SMSTemplate_Model_"+SMSTemplateId;
CacheHelper.RemoveCache(key);
return smsDAL.DeleteInfo(trans,SMSTemplateId);
}
#endregion

}
}
