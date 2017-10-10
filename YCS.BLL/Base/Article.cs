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
/// 文章-业务逻辑类
/// 创建人:
/// 日期:2017/4/7 18:48:03 +08:00
/// </summary>

public class  Article
{

private readonly ArticleDAL artDAL=new ArticleDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,int ArticleId)
{
return artDAL.CheckInfo(trans,strFieldName, strFieldValue,ArticleId);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, int ArticleId)
{
return artDAL.GetValueByField(trans,strFieldName, ArticleId);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public ArticleModel GetInfo(SqlTransaction trans,int ArticleId)
{
return artDAL.GetInfo(trans,ArticleId);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public ArticleModel GetCacheInfo(SqlTransaction trans,int ArticleId)
{
string key="Cache_Article_Model_"+ArticleId;
object value = CacheHelper.GetCache(key);
if (value != null)
return (ArticleModel)value;
else
{
ArticleModel artModel = artDAL.GetInfo(trans,ArticleId);
CacheHelper.AddCache(key, artModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return artModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,ArticleModel artModel)
{
return artDAL.InsertInfo(trans,artModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,ArticleModel artModel,int ArticleId)
{
string key="Cache_Article_Model_"+ArticleId;
CacheHelper.RemoveCache(key);
return artDAL.UpdateInfo(trans,artModel,ArticleId);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,int ArticleId)
{
string key="Cache_Article_Model_"+ArticleId;
CacheHelper.RemoveCache(key);
return artDAL.DeleteInfo(trans,ArticleId);
}
#endregion

}
}
