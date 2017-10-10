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
/// 支付方式-业务逻辑类
/// 创建人:
/// 日期:2017/4/7 18:48:06 +08:00
/// </summary>

public class  Pay
{

private readonly PayDAL payDAL=new PayDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,int PayId)
{
return payDAL.CheckInfo(trans,strFieldName, strFieldValue,PayId);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, int PayId)
{
return payDAL.GetValueByField(trans,strFieldName, PayId);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public PayModel GetInfo(SqlTransaction trans,int PayId)
{
return payDAL.GetInfo(trans,PayId);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public PayModel GetCacheInfo(SqlTransaction trans,int PayId)
{
string key="Cache_Pay_Model_"+PayId;
object value = CacheHelper.GetCache(key);
if (value != null)
return (PayModel)value;
else
{
PayModel payModel = payDAL.GetInfo(trans,PayId);
CacheHelper.AddCache(key, payModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return payModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,PayModel payModel)
{
return payDAL.InsertInfo(trans,payModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,PayModel payModel,int PayId)
{
string key="Cache_Pay_Model_"+PayId;
CacheHelper.RemoveCache(key);
return payDAL.UpdateInfo(trans,payModel,PayId);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,int PayId)
{
string key="Cache_Pay_Model_"+PayId;
CacheHelper.RemoveCache(key);
return payDAL.DeleteInfo(trans,PayId);
}
#endregion

}
}
