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
/// 商品属性值資料表-业务逻辑类
/// 创建人:
/// 日期:2017/4/7 18:48:06 +08:00
/// </summary>

public class  ProductAttributeLibrary
{

private readonly ProductAttributeLibraryDAL proDAL=new ProductAttributeLibraryDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,int SN)
{
return proDAL.CheckInfo(trans,strFieldName, strFieldValue,SN);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, int SN)
{
return proDAL.GetValueByField(trans,strFieldName, SN);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public ProductAttributeLibraryModel GetInfo(SqlTransaction trans,int SN)
{
return proDAL.GetInfo(trans,SN);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public ProductAttributeLibraryModel GetCacheInfo(SqlTransaction trans,int SN)
{
string key="Cache_ProductAttributeLibrary_Model_"+SN;
object value = CacheHelper.GetCache(key);
if (value != null)
return (ProductAttributeLibraryModel)value;
else
{
ProductAttributeLibraryModel proModel = proDAL.GetInfo(trans,SN);
CacheHelper.AddCache(key, proModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return proModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,ProductAttributeLibraryModel proModel)
{
return proDAL.InsertInfo(trans,proModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,ProductAttributeLibraryModel proModel,int SN)
{
string key="Cache_ProductAttributeLibrary_Model_"+SN;
CacheHelper.RemoveCache(key);
return proDAL.UpdateInfo(trans,proModel,SN);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,int SN)
{
string key="Cache_ProductAttributeLibrary_Model_"+SN;
CacheHelper.RemoveCache(key);
return proDAL.DeleteInfo(trans,SN);
}
#endregion

}
}
