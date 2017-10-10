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
/// 後台管理員日誌表-业务逻辑类
/// 创建人:
/// 日期:2017/4/7 18:48:03 +08:00
/// </summary>

public class  AdminLog
{

private readonly AdminLogDAL admDAL=new AdminLogDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,long AdminLogId)
{
return admDAL.CheckInfo(trans,strFieldName, strFieldValue,AdminLogId);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, long AdminLogId)
{
return admDAL.GetValueByField(trans,strFieldName, AdminLogId);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public AdminLogModel GetInfo(SqlTransaction trans,long AdminLogId)
{
return admDAL.GetInfo(trans,AdminLogId);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public AdminLogModel GetCacheInfo(SqlTransaction trans,long AdminLogId)
{
string key="Cache_AdminLog_Model_"+AdminLogId;
object value = CacheHelper.GetCache(key);
if (value != null)
return (AdminLogModel)value;
else
{
AdminLogModel admModel = admDAL.GetInfo(trans,AdminLogId);
CacheHelper.AddCache(key, admModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return admModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,AdminLogModel admModel)
{
return admDAL.InsertInfo(trans,admModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,AdminLogModel admModel,long AdminLogId)
{
string key="Cache_AdminLog_Model_"+AdminLogId;
CacheHelper.RemoveCache(key);
return admDAL.UpdateInfo(trans,admModel,AdminLogId);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,long AdminLogId)
{
string key="Cache_AdminLog_Model_"+AdminLogId;
CacheHelper.RemoveCache(key);
return admDAL.DeleteInfo(trans,AdminLogId);
}
#endregion

}
}
