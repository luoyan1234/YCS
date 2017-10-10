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
/// 发票-业务逻辑类
/// 创建人:
/// 日期:2017/4/7 18:48:05 +08:00
/// </summary>

public class  Invoice
{

private readonly InvoiceDAL invDAL=new InvoiceDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,int InvoiceId)
{
return invDAL.CheckInfo(trans,strFieldName, strFieldValue,InvoiceId);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, int InvoiceId)
{
return invDAL.GetValueByField(trans,strFieldName, InvoiceId);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public InvoiceModel GetInfo(SqlTransaction trans,int InvoiceId)
{
return invDAL.GetInfo(trans,InvoiceId);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public InvoiceModel GetCacheInfo(SqlTransaction trans,int InvoiceId)
{
string key="Cache_Invoice_Model_"+InvoiceId;
object value = CacheHelper.GetCache(key);
if (value != null)
return (InvoiceModel)value;
else
{
InvoiceModel invModel = invDAL.GetInfo(trans,InvoiceId);
CacheHelper.AddCache(key, invModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return invModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,InvoiceModel invModel)
{
return invDAL.InsertInfo(trans,invModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,InvoiceModel invModel,int InvoiceId)
{
string key="Cache_Invoice_Model_"+InvoiceId;
CacheHelper.RemoveCache(key);
return invDAL.UpdateInfo(trans,invModel,InvoiceId);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,int InvoiceId)
{
string key="Cache_Invoice_Model_"+InvoiceId;
CacheHelper.RemoveCache(key);
return invDAL.DeleteInfo(trans,InvoiceId);
}
#endregion

}
}
