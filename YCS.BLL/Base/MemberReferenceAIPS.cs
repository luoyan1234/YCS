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
/// -业务逻辑类
/// 创建人:
/// 日期:2017/4/7 18:48:05 +08:00
/// </summary>

public class  MemberReferenceAIPS
{

private readonly MemberReferenceAIPSDAL memDAL=new MemberReferenceAIPSDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,long SN)
{
return memDAL.CheckInfo(trans,strFieldName, strFieldValue,SN);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, long SN)
{
return memDAL.GetValueByField(trans,strFieldName, SN);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public MemberReferenceAIPSModel GetInfo(SqlTransaction trans,long SN)
{
return memDAL.GetInfo(trans,SN);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public MemberReferenceAIPSModel GetCacheInfo(SqlTransaction trans,long SN)
{
string key="Cache_MemberReferenceAIPS_Model_"+SN;
object value = CacheHelper.GetCache(key);
if (value != null)
return (MemberReferenceAIPSModel)value;
else
{
MemberReferenceAIPSModel memModel = memDAL.GetInfo(trans,SN);
CacheHelper.AddCache(key, memModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return memModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,MemberReferenceAIPSModel memModel)
{
return memDAL.InsertInfo(trans,memModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,MemberReferenceAIPSModel memModel,long SN)
{
string key="Cache_MemberReferenceAIPS_Model_"+SN;
CacheHelper.RemoveCache(key);
return memDAL.UpdateInfo(trans,memModel,SN);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,long SN)
{
string key="Cache_MemberReferenceAIPS_Model_"+SN;
CacheHelper.RemoveCache(key);
return memDAL.DeleteInfo(trans,SN);
}
#endregion

}
}
