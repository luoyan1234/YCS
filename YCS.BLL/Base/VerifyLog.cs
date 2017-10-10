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
/// 验证日志-业务逻辑类
/// 创建人:
/// 日期:2017/4/7 18:48:08 +08:00
/// </summary>

public class  VerifyLog
{

private readonly VerifyLogDAL verDAL=new VerifyLogDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,int VerifyLogId)
{
return verDAL.CheckInfo(trans,strFieldName, strFieldValue,VerifyLogId);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, int VerifyLogId)
{
return verDAL.GetValueByField(trans,strFieldName, VerifyLogId);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public VerifyLogModel GetInfo(SqlTransaction trans,int VerifyLogId)
{
return verDAL.GetInfo(trans,VerifyLogId);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public VerifyLogModel GetCacheInfo(SqlTransaction trans,int VerifyLogId)
{
string key="Cache_VerifyLog_Model_"+VerifyLogId;
object value = CacheHelper.GetCache(key);
if (value != null)
return (VerifyLogModel)value;
else
{
VerifyLogModel verModel = verDAL.GetInfo(trans,VerifyLogId);
CacheHelper.AddCache(key, verModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return verModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,VerifyLogModel verModel)
{
return verDAL.InsertInfo(trans,verModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,VerifyLogModel verModel,int VerifyLogId)
{
string key="Cache_VerifyLog_Model_"+VerifyLogId;
CacheHelper.RemoveCache(key);
return verDAL.UpdateInfo(trans,verModel,VerifyLogId);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,int VerifyLogId)
{
string key="Cache_VerifyLog_Model_"+VerifyLogId;
CacheHelper.RemoveCache(key);
return verDAL.DeleteInfo(trans,VerifyLogId);
}
#endregion

}
}
