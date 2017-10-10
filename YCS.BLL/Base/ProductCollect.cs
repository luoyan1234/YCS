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
/// 产品收藏-业务逻辑类
/// 创建人:
/// 日期:2017/4/7 18:48:06 +08:00
/// </summary>

public class  ProductCollect
{

private readonly ProductCollectDAL proDAL=new ProductCollectDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,int ProductCollectId)
{
return proDAL.CheckInfo(trans,strFieldName, strFieldValue,ProductCollectId);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, int ProductCollectId)
{
return proDAL.GetValueByField(trans,strFieldName, ProductCollectId);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public ProductCollectModel GetInfo(SqlTransaction trans,int ProductCollectId)
{
return proDAL.GetInfo(trans,ProductCollectId);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public ProductCollectModel GetCacheInfo(SqlTransaction trans,int ProductCollectId)
{
string key="Cache_ProductCollect_Model_"+ProductCollectId;
object value = CacheHelper.GetCache(key);
if (value != null)
return (ProductCollectModel)value;
else
{
ProductCollectModel proModel = proDAL.GetInfo(trans,ProductCollectId);
CacheHelper.AddCache(key, proModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return proModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,ProductCollectModel proModel)
{
return proDAL.InsertInfo(trans,proModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,ProductCollectModel proModel,int ProductCollectId)
{
string key="Cache_ProductCollect_Model_"+ProductCollectId;
CacheHelper.RemoveCache(key);
return proDAL.UpdateInfo(trans,proModel,ProductCollectId);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,int ProductCollectId)
{
string key="Cache_ProductCollect_Model_"+ProductCollectId;
CacheHelper.RemoveCache(key);
return proDAL.DeleteInfo(trans,ProductCollectId);
}
#endregion

}
}
