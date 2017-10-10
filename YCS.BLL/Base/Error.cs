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
/// 错误日志-业务逻辑类
/// 创建人:
/// 日期:2017/4/7 18:48:04 +08:00
/// </summary>

public class  Error
{

private readonly ErrorDAL errDAL=new ErrorDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,int ErrorId)
{
return errDAL.CheckInfo(trans,strFieldName, strFieldValue,ErrorId);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, int ErrorId)
{
return errDAL.GetValueByField(trans,strFieldName, ErrorId);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public ErrorModel GetInfo(SqlTransaction trans,int ErrorId)
{
return errDAL.GetInfo(trans,ErrorId);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public ErrorModel GetCacheInfo(SqlTransaction trans,int ErrorId)
{
string key="Cache_Error_Model_"+ErrorId;
object value = CacheHelper.GetCache(key);
if (value != null)
return (ErrorModel)value;
else
{
ErrorModel errModel = errDAL.GetInfo(trans,ErrorId);
CacheHelper.AddCache(key, errModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return errModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,ErrorModel errModel)
{
return errDAL.InsertInfo(trans,errModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,ErrorModel errModel,int ErrorId)
{
string key="Cache_Error_Model_"+ErrorId;
CacheHelper.RemoveCache(key);
return errDAL.UpdateInfo(trans,errModel,ErrorId);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,int ErrorId)
{
string key="Cache_Error_Model_"+ErrorId;
CacheHelper.RemoveCache(key);
return errDAL.DeleteInfo(trans,ErrorId);
}
#endregion

}
}
