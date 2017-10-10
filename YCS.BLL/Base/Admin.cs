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
/// 後台帳號表-业务逻辑类
/// 创建人:
/// 日期:2017/4/7 18:48:03 +08:00
/// </summary>

public class  Admin
{

private readonly AdminDAL admDAL=new AdminDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,int AdminId)
{
return admDAL.CheckInfo(trans,strFieldName, strFieldValue,AdminId);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, int AdminId)
{
return admDAL.GetValueByField(trans,strFieldName, AdminId);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public AdminModel GetInfo(SqlTransaction trans,int AdminId)
{
return admDAL.GetInfo(trans,AdminId);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public AdminModel GetCacheInfo(SqlTransaction trans,int AdminId)
{
string key="Cache_Admin_Model_"+AdminId;
object value = CacheHelper.GetCache(key);
if (value != null)
return (AdminModel)value;
else
{
AdminModel admModel = admDAL.GetInfo(trans,AdminId);
CacheHelper.AddCache(key, admModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return admModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,AdminModel admModel)
{
return admDAL.InsertInfo(trans,admModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,AdminModel admModel,int AdminId)
{
string key="Cache_Admin_Model_"+AdminId;
CacheHelper.RemoveCache(key);
return admDAL.UpdateInfo(trans,admModel,AdminId);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,int AdminId)
{
string key="Cache_Admin_Model_"+AdminId;
CacheHelper.RemoveCache(key);
return admDAL.DeleteInfo(trans,AdminId);
}
#endregion

}
}
