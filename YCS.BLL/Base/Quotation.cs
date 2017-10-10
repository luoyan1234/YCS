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
/// 询价管理-业务逻辑类
/// 创建人:
/// 日期:2017/4/7 18:48:07 +08:00
/// </summary>

public class  Quotation
{

private readonly QuotationDAL quoDAL=new QuotationDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,int QuotationId)
{
return quoDAL.CheckInfo(trans,strFieldName, strFieldValue,QuotationId);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, int QuotationId)
{
return quoDAL.GetValueByField(trans,strFieldName, QuotationId);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public QuotationModel GetInfo(SqlTransaction trans,int QuotationId)
{
return quoDAL.GetInfo(trans,QuotationId);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public QuotationModel GetCacheInfo(SqlTransaction trans,int QuotationId)
{
string key="Cache_Quotation_Model_"+QuotationId;
object value = CacheHelper.GetCache(key);
if (value != null)
return (QuotationModel)value;
else
{
QuotationModel quoModel = quoDAL.GetInfo(trans,QuotationId);
CacheHelper.AddCache(key, quoModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return quoModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,QuotationModel quoModel)
{
return quoDAL.InsertInfo(trans,quoModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,QuotationModel quoModel,int QuotationId)
{
string key="Cache_Quotation_Model_"+QuotationId;
CacheHelper.RemoveCache(key);
return quoDAL.UpdateInfo(trans,quoModel,QuotationId);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,int QuotationId)
{
string key="Cache_Quotation_Model_"+QuotationId;
CacheHelper.RemoveCache(key);
return quoDAL.DeleteInfo(trans,QuotationId);
}
#endregion

}
}
