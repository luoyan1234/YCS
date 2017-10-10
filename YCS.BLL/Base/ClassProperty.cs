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
/// 栏目属性-业务逻辑类
/// 创建人:
/// 日期:2017/4/7 18:48:03 +08:00
/// </summary>

public class  ClassProperty
{

private readonly ClassPropertyDAL claDAL=new ClassPropertyDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,int ClassPropertyId)
{
return claDAL.CheckInfo(trans,strFieldName, strFieldValue,ClassPropertyId);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, int ClassPropertyId)
{
return claDAL.GetValueByField(trans,strFieldName, ClassPropertyId);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public ClassPropertyModel GetInfo(SqlTransaction trans,int ClassPropertyId)
{
return claDAL.GetInfo(trans,ClassPropertyId);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public ClassPropertyModel GetCacheInfo(SqlTransaction trans,int ClassPropertyId)
{
string key="Cache_ClassProperty_Model_"+ClassPropertyId;
object value = CacheHelper.GetCache(key);
if (value != null)
return (ClassPropertyModel)value;
else
{
ClassPropertyModel claModel = claDAL.GetInfo(trans,ClassPropertyId);
CacheHelper.AddCache(key, claModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return claModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,ClassPropertyModel claModel)
{
return claDAL.InsertInfo(trans,claModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,ClassPropertyModel claModel,int ClassPropertyId)
{
string key="Cache_ClassProperty_Model_"+ClassPropertyId;
CacheHelper.RemoveCache(key);
return claDAL.UpdateInfo(trans,claModel,ClassPropertyId);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,int ClassPropertyId)
{
string key="Cache_ClassProperty_Model_"+ClassPropertyId;
CacheHelper.RemoveCache(key);
return claDAL.DeleteInfo(trans,ClassPropertyId);
}
#endregion

}
}
