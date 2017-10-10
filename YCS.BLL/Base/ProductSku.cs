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
/// 产品SKU-业务逻辑类
/// 创建人:杨小明
/// 日期:2017/4/17 11:10:34 +08:00
/// </summary>

public class  ProductSku
{

private readonly ProductSkuDAL proSkuDAL=new ProductSkuDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,long SN)
{
return proSkuDAL.CheckInfo(trans,strFieldName, strFieldValue,SN);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, long SN)
{
return proSkuDAL.GetValueByField(trans,strFieldName, SN);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public ProductSkuModel GetInfo(SqlTransaction trans,long SN)
{
return proSkuDAL.GetInfo(trans,SN);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public ProductSkuModel GetCacheInfo(SqlTransaction trans,long SN)
{
string key="Cache_ProductSku_Model_"+SN;
object value = CacheHelper.GetCache(key);
if (value != null)
return (ProductSkuModel)value;
else
{
ProductSkuModel proSkuModel = proSkuDAL.GetInfo(trans,SN);
CacheHelper.AddCache(key, proSkuModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return proSkuModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,ProductSkuModel proSkuModel)
{
return proSkuDAL.InsertInfo(trans,proSkuModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,ProductSkuModel proSkuModel,long SN)
{
string key="Cache_ProductSku_Model_"+SN;
CacheHelper.RemoveCache(key);
return proSkuDAL.UpdateInfo(trans,proSkuModel,SN);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,long SN)
{
string key="Cache_ProductSku_Model_"+SN;
CacheHelper.RemoveCache(key);
return proSkuDAL.DeleteInfo(trans,SN);
}
#endregion

}
}
