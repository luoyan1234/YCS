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
/// 会员安保问题-业务逻辑类
/// 创建人:
/// 日期:2017/4/7 18:48:05 +08:00
/// </summary>

public class  MembershipSecIssues
{

private readonly MembershipSecIssuesDAL memDAL=new MembershipSecIssuesDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,int SecIssuesId)
{
return memDAL.CheckInfo(trans,strFieldName, strFieldValue,SecIssuesId);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, int SecIssuesId)
{
return memDAL.GetValueByField(trans,strFieldName, SecIssuesId);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public MembershipSecIssuesModel GetInfo(SqlTransaction trans,int SecIssuesId)
{
return memDAL.GetInfo(trans,SecIssuesId);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public MembershipSecIssuesModel GetCacheInfo(SqlTransaction trans,int SecIssuesId)
{
string key="Cache_MembershipSecIssues_Model_"+SecIssuesId;
object value = CacheHelper.GetCache(key);
if (value != null)
return (MembershipSecIssuesModel)value;
else
{
MembershipSecIssuesModel memModel = memDAL.GetInfo(trans,SecIssuesId);
CacheHelper.AddCache(key, memModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return memModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,MembershipSecIssuesModel memModel)
{
return memDAL.InsertInfo(trans,memModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,MembershipSecIssuesModel memModel,int SecIssuesId)
{
string key="Cache_MembershipSecIssues_Model_"+SecIssuesId;
CacheHelper.RemoveCache(key);
return memDAL.UpdateInfo(trans,memModel,SecIssuesId);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,int SecIssuesId)
{
string key="Cache_MembershipSecIssues_Model_"+SecIssuesId;
CacheHelper.RemoveCache(key);
return memDAL.DeleteInfo(trans,SecIssuesId);
}
#endregion

}
}
