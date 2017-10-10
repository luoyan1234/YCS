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
/// 編輯器模板-业务逻辑类
/// 创建人:
/// 日期:2017/4/7 18:48:05 +08:00
/// </summary>

public class  Module
{

private readonly ModuleDAL modDAL=new ModuleDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,long ID)
{
return modDAL.CheckInfo(trans,strFieldName, strFieldValue,ID);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, long ID)
{
return modDAL.GetValueByField(trans,strFieldName, ID);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public ModuleModel GetInfo(SqlTransaction trans,long ID)
{
return modDAL.GetInfo(trans,ID);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public ModuleModel GetCacheInfo(SqlTransaction trans,long ID)
{
string key="Cache_Module_Model_"+ID;
object value = CacheHelper.GetCache(key);
if (value != null)
return (ModuleModel)value;
else
{
ModuleModel modModel = modDAL.GetInfo(trans,ID);
CacheHelper.AddCache(key, modModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return modModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,ModuleModel modModel)
{
return modDAL.InsertInfo(trans,modModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,ModuleModel modModel,long ID)
{
string key="Cache_Module_Model_"+ID;
CacheHelper.RemoveCache(key);
return modDAL.UpdateInfo(trans,modModel,ID);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,long ID)
{
string key="Cache_Module_Model_"+ID;
CacheHelper.RemoveCache(key);
return modDAL.DeleteInfo(trans,ID);
}
#endregion

}
}
