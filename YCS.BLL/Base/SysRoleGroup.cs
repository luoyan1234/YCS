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
/// 後台角色分組表-业务逻辑类
/// 创建人:
/// 日期:2017/4/7 18:48:08 +08:00
/// </summary>

public class  SysRoleGroup
{

private readonly SysRoleGroupDAL sysDAL=new SysRoleGroupDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,int SysRoleGroupId)
{
return sysDAL.CheckInfo(trans,strFieldName, strFieldValue,SysRoleGroupId);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, int SysRoleGroupId)
{
return sysDAL.GetValueByField(trans,strFieldName, SysRoleGroupId);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public SysRoleGroupModel GetInfo(SqlTransaction trans,int SysRoleGroupId)
{
return sysDAL.GetInfo(trans,SysRoleGroupId);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public SysRoleGroupModel GetCacheInfo(SqlTransaction trans,int SysRoleGroupId)
{
string key="Cache_SysRoleGroup_Model_"+SysRoleGroupId;
object value = CacheHelper.GetCache(key);
if (value != null)
return (SysRoleGroupModel)value;
else
{
SysRoleGroupModel sysModel = sysDAL.GetInfo(trans,SysRoleGroupId);
CacheHelper.AddCache(key, sysModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return sysModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,SysRoleGroupModel sysModel)
{
return sysDAL.InsertInfo(trans,sysModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,SysRoleGroupModel sysModel,int SysRoleGroupId)
{
string key="Cache_SysRoleGroup_Model_"+SysRoleGroupId;
CacheHelper.RemoveCache(key);
return sysDAL.UpdateInfo(trans,sysModel,SysRoleGroupId);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,int SysRoleGroupId)
{
string key="Cache_SysRoleGroup_Model_"+SysRoleGroupId;
CacheHelper.RemoveCache(key);
return sysDAL.DeleteInfo(trans,SysRoleGroupId);
}
#endregion

}
}
