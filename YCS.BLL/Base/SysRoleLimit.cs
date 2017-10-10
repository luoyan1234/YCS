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
/// 後台角色個別模塊權限表-业务逻辑类
/// 创建人:
/// 日期:2017/4/7 18:48:08 +08:00
/// </summary>

public class  SysRoleLimit
{

private readonly SysRoleLimitDAL sysDAL=new SysRoleLimitDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,long SysRoleLimitId)
{
return sysDAL.CheckInfo(trans,strFieldName, strFieldValue,SysRoleLimitId);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, long SysRoleLimitId)
{
return sysDAL.GetValueByField(trans,strFieldName, SysRoleLimitId);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public SysRoleLimitModel GetInfo(SqlTransaction trans,long SysRoleLimitId)
{
return sysDAL.GetInfo(trans,SysRoleLimitId);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public SysRoleLimitModel GetCacheInfo(SqlTransaction trans,long SysRoleLimitId)
{
string key="Cache_SysRoleLimit_Model_"+SysRoleLimitId;
object value = CacheHelper.GetCache(key);
if (value != null)
return (SysRoleLimitModel)value;
else
{
SysRoleLimitModel sysModel = sysDAL.GetInfo(trans,SysRoleLimitId);
CacheHelper.AddCache(key, sysModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return sysModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,SysRoleLimitModel sysModel)
{
return sysDAL.InsertInfo(trans,sysModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,SysRoleLimitModel sysModel,long SysRoleLimitId)
{
string key="Cache_SysRoleLimit_Model_"+SysRoleLimitId;
CacheHelper.RemoveCache(key);
return sysDAL.UpdateInfo(trans,sysModel,SysRoleLimitId);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,long SysRoleLimitId)
{
string key="Cache_SysRoleLimit_Model_"+SysRoleLimitId;
CacheHelper.RemoveCache(key);
return sysDAL.DeleteInfo(trans,SysRoleLimitId);
}
#endregion

}
}
