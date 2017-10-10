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
/// 栏目-业务逻辑类
/// 创建人:
/// 日期:2017/4/7 18:48:03 +08:00
/// </summary>

public class  Class
{

private readonly ClassDAL claDAL=new ClassDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,int ClassId)
{
return claDAL.CheckInfo(trans,strFieldName, strFieldValue,ClassId);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, int ClassId)
{
return claDAL.GetValueByField(trans,strFieldName, ClassId);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public ClassModel GetInfo(SqlTransaction trans,int ClassId)
{
return claDAL.GetInfo(trans,ClassId);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public ClassModel GetCacheInfo(SqlTransaction trans,int ClassId)
{
string key="Cache_Class_Model_"+ClassId;
object value = CacheHelper.GetCache(key);
if (value != null)
return (ClassModel)value;
else
{
ClassModel claModel = claDAL.GetInfo(trans,ClassId);
CacheHelper.AddCache(key, claModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return claModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,ClassModel claModel)
{
return claDAL.InsertInfo(trans,claModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,ClassModel claModel,int ClassId)
{
string key="Cache_Class_Model_"+ClassId;
CacheHelper.RemoveCache(key);
return claDAL.UpdateInfo(trans,claModel,ClassId);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,int ClassId)
{
string key="Cache_Class_Model_"+ClassId;
CacheHelper.RemoveCache(key);
return claDAL.DeleteInfo(trans,ClassId);
}
#endregion

}
}
