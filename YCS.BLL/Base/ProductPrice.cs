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
/// 產品價格-业务逻辑类
/// 创建人:
/// 日期:2017/4/7 18:48:06 +08:00
/// </summary>

public class  ProductPrice
{

private readonly ProductPriceDAL proDAL=new ProductPriceDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,string ProductPriceId)
{
return proDAL.CheckInfo(trans,strFieldName, strFieldValue,ProductPriceId);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, string ProductPriceId)
{
return proDAL.GetValueByField(trans,strFieldName, ProductPriceId);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public ProductPriceModel GetInfo(SqlTransaction trans,string ProductPriceId)
{
return proDAL.GetInfo(trans,ProductPriceId);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public ProductPriceModel GetCacheInfo(SqlTransaction trans,string ProductPriceId)
{
string key="Cache_ProductPrice_Model_"+ProductPriceId;
object value = CacheHelper.GetCache(key);
if (value != null)
return (ProductPriceModel)value;
else
{
ProductPriceModel proModel = proDAL.GetInfo(trans,ProductPriceId);
CacheHelper.AddCache(key, proModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return proModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,ProductPriceModel proModel)
{
return proDAL.InsertInfo(trans,proModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,ProductPriceModel proModel,string ProductPriceId)
{
string key="Cache_ProductPrice_Model_"+ProductPriceId;
CacheHelper.RemoveCache(key);
return proDAL.UpdateInfo(trans,proModel,ProductPriceId);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,string ProductPriceId)
{
string key="Cache_ProductPrice_Model_"+ProductPriceId;
CacheHelper.RemoveCache(key);
return proDAL.DeleteInfo(trans,ProductPriceId);
}
#endregion

}
}
