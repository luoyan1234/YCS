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
/// 後台模塊表-业务逻辑类
/// 创建人:
/// 日期:2017/4/7 18:48:08 +08:00
/// </summary>

public class  SysModule
{

private readonly SysModuleDAL sysDAL=new SysModuleDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,int SysModuleId)
{
return sysDAL.CheckInfo(trans,strFieldName, strFieldValue,SysModuleId);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, int SysModuleId)
{
return sysDAL.GetValueByField(trans,strFieldName, SysModuleId);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public SysModuleModel GetInfo(SqlTransaction trans,int SysModuleId)
{
return sysDAL.GetInfo(trans,SysModuleId);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public SysModuleModel GetCacheInfo(SqlTransaction trans,int SysModuleId)
{
string key="Cache_SysModule_Model_"+SysModuleId;
object value = CacheHelper.GetCache(key);
if (value != null)
return (SysModuleModel)value;
else
{
SysModuleModel sysModel = sysDAL.GetInfo(trans,SysModuleId);
CacheHelper.AddCache(key, sysModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return sysModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,SysModuleModel sysModel)
{
return sysDAL.InsertInfo(trans,sysModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,SysModuleModel sysModel,int SysModuleId)
{
string key="Cache_SysModule_Model_"+SysModuleId;
CacheHelper.RemoveCache(key);
return sysDAL.UpdateInfo(trans,sysModel,SysModuleId);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,int SysModuleId)
{
string key="Cache_SysModule_Model_"+SysModuleId;
CacheHelper.RemoveCache(key);
return sysDAL.DeleteInfo(trans,SysModuleId);
}
#endregion

}
}
