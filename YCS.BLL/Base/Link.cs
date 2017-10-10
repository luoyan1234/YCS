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
/// 链接-业务逻辑类
/// 创建人:
/// 日期:2017/4/7 18:48:05 +08:00
/// </summary>

public class  Link
{

private readonly LinkDAL linDAL=new LinkDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,int LinkId)
{
return linDAL.CheckInfo(trans,strFieldName, strFieldValue,LinkId);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, int LinkId)
{
return linDAL.GetValueByField(trans,strFieldName, LinkId);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public LinkModel GetInfo(SqlTransaction trans,int LinkId)
{
return linDAL.GetInfo(trans,LinkId);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public LinkModel GetCacheInfo(SqlTransaction trans,int LinkId)
{
string key="Cache_Link_Model_"+LinkId;
object value = CacheHelper.GetCache(key);
if (value != null)
return (LinkModel)value;
else
{
LinkModel linModel = linDAL.GetInfo(trans,LinkId);
CacheHelper.AddCache(key, linModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return linModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,LinkModel linModel)
{
return linDAL.InsertInfo(trans,linModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,LinkModel linModel,int LinkId)
{
string key="Cache_Link_Model_"+LinkId;
CacheHelper.RemoveCache(key);
return linDAL.UpdateInfo(trans,linModel,LinkId);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,int LinkId)
{
string key="Cache_Link_Model_"+LinkId;
CacheHelper.RemoveCache(key);
return linDAL.DeleteInfo(trans,LinkId);
}
#endregion

}
}
