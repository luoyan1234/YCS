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
/// 後台角色表-业务逻辑类
/// 创建人:
/// 日期:2017/4/7 18:48:08 +08:00
/// </summary>

public class  SysRole
{

private readonly SysRoleDAL sysDAL=new SysRoleDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,int SysRoleId)
{
return sysDAL.CheckInfo(trans,strFieldName, strFieldValue,SysRoleId);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, int SysRoleId)
{
return sysDAL.GetValueByField(trans,strFieldName, SysRoleId);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public SysRoleModel GetInfo(SqlTransaction trans,int SysRoleId)
{
return sysDAL.GetInfo(trans,SysRoleId);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public SysRoleModel GetCacheInfo(SqlTransaction trans,int SysRoleId)
{
string key="Cache_SysRole_Model_"+SysRoleId;
object value = CacheHelper.GetCache(key);
if (value != null)
return (SysRoleModel)value;
else
{
SysRoleModel sysModel = sysDAL.GetInfo(trans,SysRoleId);
CacheHelper.AddCache(key, sysModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return sysModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,SysRoleModel sysModel)
{
return sysDAL.InsertInfo(trans,sysModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,SysRoleModel sysModel,int SysRoleId)
{
string key="Cache_SysRole_Model_"+SysRoleId;
CacheHelper.RemoveCache(key);
return sysDAL.UpdateInfo(trans,sysModel,SysRoleId);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,int SysRoleId)
{
string key="Cache_SysRole_Model_"+SysRoleId;
CacheHelper.RemoveCache(key);
return sysDAL.DeleteInfo(trans,SysRoleId);
}
#endregion

}
}
