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
/// -业务逻辑类
/// 创建人:
/// 日期:2017/4/7 18:48:03 +08:00
/// </summary>

public class  AIPS_CodeReference
{

private readonly AIPS_CodeReferenceDAL aipDAL=new AIPS_CodeReferenceDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,string ListID)
{
return aipDAL.CheckInfo(trans,strFieldName, strFieldValue,ListID);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, string ListID)
{
return aipDAL.GetValueByField(trans,strFieldName, ListID);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public AIPS_CodeReferenceModel GetInfo(SqlTransaction trans,string ListID)
{
return aipDAL.GetInfo(trans,ListID);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public AIPS_CodeReferenceModel GetCacheInfo(SqlTransaction trans,string ListID)
{
string key="Cache_AIPS_CodeReference_Model_"+ListID;
object value = CacheHelper.GetCache(key);
if (value != null)
return (AIPS_CodeReferenceModel)value;
else
{
AIPS_CodeReferenceModel aipModel = aipDAL.GetInfo(trans,ListID);
CacheHelper.AddCache(key, aipModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return aipModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,AIPS_CodeReferenceModel aipModel)
{
return aipDAL.InsertInfo(trans,aipModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,AIPS_CodeReferenceModel aipModel,string ListID)
{
string key="Cache_AIPS_CodeReference_Model_"+ListID;
CacheHelper.RemoveCache(key);
return aipDAL.UpdateInfo(trans,aipModel,ListID);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,string ListID)
{
string key="Cache_AIPS_CodeReference_Model_"+ListID;
CacheHelper.RemoveCache(key);
return aipDAL.DeleteInfo(trans,ListID);
}
#endregion

}
}
