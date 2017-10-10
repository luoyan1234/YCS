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
/// 会员地址-业务逻辑类
/// 创建人:
/// 日期:2017/4/7 18:48:08 +08:00
/// </summary>

public class  UserAddr
{

private readonly UserAddrDAL useDAL=new UserAddrDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,int UserAddrId)
{
return useDAL.CheckInfo(trans,strFieldName, strFieldValue,UserAddrId);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, int UserAddrId)
{
return useDAL.GetValueByField(trans,strFieldName, UserAddrId);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public UserAddrModel GetInfo(SqlTransaction trans,int UserAddrId)
{
return useDAL.GetInfo(trans,UserAddrId);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public UserAddrModel GetCacheInfo(SqlTransaction trans,int UserAddrId)
{
string key="Cache_UserAddr_Model_"+UserAddrId;
object value = CacheHelper.GetCache(key);
if (value != null)
return (UserAddrModel)value;
else
{
UserAddrModel useModel = useDAL.GetInfo(trans,UserAddrId);
CacheHelper.AddCache(key, useModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return useModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,UserAddrModel useModel)
{
return useDAL.InsertInfo(trans,useModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,UserAddrModel useModel,int UserAddrId)
{
string key="Cache_UserAddr_Model_"+UserAddrId;
CacheHelper.RemoveCache(key);
return useDAL.UpdateInfo(trans,useModel,UserAddrId);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,int UserAddrId)
{
string key="Cache_UserAddr_Model_"+UserAddrId;
CacheHelper.RemoveCache(key);
return useDAL.DeleteInfo(trans,UserAddrId);
}
#endregion

}
}
