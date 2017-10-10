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
/// 後台模塊權限列舉表-业务逻辑类
/// 创建人:
/// 日期:2017/4/7 18:48:07 +08:00
/// </summary>

public class  SysLimit
{

private readonly SysLimitDAL sysDAL=new SysLimitDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,int SysLimitId)
{
return sysDAL.CheckInfo(trans,strFieldName, strFieldValue,SysLimitId);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, int SysLimitId)
{
return sysDAL.GetValueByField(trans,strFieldName, SysLimitId);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public SysLimitModel GetInfo(SqlTransaction trans,int SysLimitId)
{
return sysDAL.GetInfo(trans,SysLimitId);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public SysLimitModel GetCacheInfo(SqlTransaction trans,int SysLimitId)
{
string key="Cache_SysLimit_Model_"+SysLimitId;
object value = CacheHelper.GetCache(key);
if (value != null)
return (SysLimitModel)value;
else
{
SysLimitModel sysModel = sysDAL.GetInfo(trans,SysLimitId);
CacheHelper.AddCache(key, sysModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return sysModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,SysLimitModel sysModel)
{
return sysDAL.InsertInfo(trans,sysModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,SysLimitModel sysModel,int SysLimitId)
{
string key="Cache_SysLimit_Model_"+SysLimitId;
CacheHelper.RemoveCache(key);
return sysDAL.UpdateInfo(trans,sysModel,SysLimitId);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,int SysLimitId)
{
string key="Cache_SysLimit_Model_"+SysLimitId;
CacheHelper.RemoveCache(key);
return sysDAL.DeleteInfo(trans,SysLimitId);
}
#endregion

}
}
