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
/// AIPS产品关系-业务逻辑类
/// 创建人:杨小明
/// 日期:2017/4/21 16:59:21 +08:00
/// </summary>

public class  ProductReferenceAIPS
{

private readonly ProductReferenceAIPSDAL proRefDAL=new ProductReferenceAIPSDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,long SN)
{
return proRefDAL.CheckInfo(trans,strFieldName, strFieldValue,SN);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, long SN)
{
return proRefDAL.GetValueByField(trans,strFieldName, SN);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public ProductReferenceAIPSModel GetInfo(SqlTransaction trans,long SN)
{
return proRefDAL.GetInfo(trans,SN);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public ProductReferenceAIPSModel GetCacheInfo(SqlTransaction trans,long SN)
{
string key="Cache_ProductReferenceAIPS_Model_"+SN;
object value = CacheHelper.GetCache(key);
if (value != null)
return (ProductReferenceAIPSModel)value;
else
{
ProductReferenceAIPSModel proRefModel = proRefDAL.GetInfo(trans,SN);
CacheHelper.AddCache(key, proRefModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return proRefModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,ProductReferenceAIPSModel proRefModel)
{
return proRefDAL.InsertInfo(trans,proRefModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,ProductReferenceAIPSModel proRefModel,long SN)
{
string key="Cache_ProductReferenceAIPS_Model_"+SN;
CacheHelper.RemoveCache(key);
return proRefDAL.UpdateInfo(trans,proRefModel,SN);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,long SN)
{
string key="Cache_ProductReferenceAIPS_Model_"+SN;
CacheHelper.RemoveCache(key);
return proRefDAL.DeleteInfo(trans,SN);
}
#endregion

}
}
