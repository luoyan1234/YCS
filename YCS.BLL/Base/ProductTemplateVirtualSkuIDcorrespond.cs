﻿using System;
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
/// 商品模板項目對應屬性-业务逻辑类
/// 创建人:
/// 日期:2017/4/7 18:48:07 +08:00
/// </summary>

public class  ProductTemplateVirtualSkuIDcorrespond
{

private readonly ProductTemplateVirtualSkuIDcorrespondDAL proDAL=new ProductTemplateVirtualSkuIDcorrespondDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,long SN)
{
return proDAL.CheckInfo(trans,strFieldName, strFieldValue,SN);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, long SN)
{
return proDAL.GetValueByField(trans,strFieldName, SN);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public ProductTemplateVirtualSkuIDcorrespondModel GetInfo(SqlTransaction trans,long SN)
{
return proDAL.GetInfo(trans,SN);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public ProductTemplateVirtualSkuIDcorrespondModel GetCacheInfo(SqlTransaction trans,long SN)
{
string key="Cache_ProductTemplateVirtualSkuIDcorrespond_Model_"+SN;
object value = CacheHelper.GetCache(key);
if (value != null)
return (ProductTemplateVirtualSkuIDcorrespondModel)value;
else
{
ProductTemplateVirtualSkuIDcorrespondModel proModel = proDAL.GetInfo(trans,SN);
CacheHelper.AddCache(key, proModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return proModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,ProductTemplateVirtualSkuIDcorrespondModel proModel)
{
return proDAL.InsertInfo(trans,proModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,ProductTemplateVirtualSkuIDcorrespondModel proModel,long SN)
{
string key="Cache_ProductTemplateVirtualSkuIDcorrespond_Model_"+SN;
CacheHelper.RemoveCache(key);
return proDAL.UpdateInfo(trans,proModel,SN);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,long SN)
{
string key="Cache_ProductTemplateVirtualSkuIDcorrespond_Model_"+SN;
CacheHelper.RemoveCache(key);
return proDAL.DeleteInfo(trans,SN);
}
#endregion

}
}
