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
/// 編輯器模式組件-业务逻辑类
/// 创建人:
/// 日期:2017/4/7 18:48:06 +08:00
/// </summary>

public class  PatternComponent
{

private readonly PatternComponentDAL patDAL=new PatternComponentDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,long ID)
{
return patDAL.CheckInfo(trans,strFieldName, strFieldValue,ID);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, long ID)
{
return patDAL.GetValueByField(trans,strFieldName, ID);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public PatternComponentModel GetInfo(SqlTransaction trans,long ID)
{
return patDAL.GetInfo(trans,ID);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public PatternComponentModel GetCacheInfo(SqlTransaction trans,long ID)
{
string key="Cache_PatternComponent_Model_"+ID;
object value = CacheHelper.GetCache(key);
if (value != null)
return (PatternComponentModel)value;
else
{
PatternComponentModel patModel = patDAL.GetInfo(trans,ID);
CacheHelper.AddCache(key, patModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return patModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,PatternComponentModel patModel)
{
return patDAL.InsertInfo(trans,patModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,PatternComponentModel patModel,long ID)
{
string key="Cache_PatternComponent_Model_"+ID;
CacheHelper.RemoveCache(key);
return patDAL.UpdateInfo(trans,patModel,ID);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,long ID)
{
string key="Cache_PatternComponent_Model_"+ID;
CacheHelper.RemoveCache(key);
return patDAL.DeleteInfo(trans,ID);
}
#endregion

}
}
