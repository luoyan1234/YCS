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
/// 栏目模板-业务逻辑类
/// 创建人:
/// 日期:2017/4/7 18:48:03 +08:00
/// </summary>

public class  ClassTemplate
{

private readonly ClassTemplateDAL claDAL=new ClassTemplateDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,int ClassTemplateId)
{
return claDAL.CheckInfo(trans,strFieldName, strFieldValue,ClassTemplateId);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, int ClassTemplateId)
{
return claDAL.GetValueByField(trans,strFieldName, ClassTemplateId);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public ClassTemplateModel GetInfo(SqlTransaction trans,int ClassTemplateId)
{
return claDAL.GetInfo(trans,ClassTemplateId);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public ClassTemplateModel GetCacheInfo(SqlTransaction trans,int ClassTemplateId)
{
string key="Cache_ClassTemplate_Model_"+ClassTemplateId;
object value = CacheHelper.GetCache(key);
if (value != null)
return (ClassTemplateModel)value;
else
{
ClassTemplateModel claModel = claDAL.GetInfo(trans,ClassTemplateId);
CacheHelper.AddCache(key, claModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return claModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,ClassTemplateModel claModel)
{
return claDAL.InsertInfo(trans,claModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,ClassTemplateModel claModel,int ClassTemplateId)
{
string key="Cache_ClassTemplate_Model_"+ClassTemplateId;
CacheHelper.RemoveCache(key);
return claDAL.UpdateInfo(trans,claModel,ClassTemplateId);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,int ClassTemplateId)
{
string key="Cache_ClassTemplate_Model_"+ClassTemplateId;
CacheHelper.RemoveCache(key);
return claDAL.DeleteInfo(trans,ClassTemplateId);
}
#endregion

}
}
