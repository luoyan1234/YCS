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
/// 印刷商-业务逻辑类
/// 创建人:
/// 日期:2017/4/7 18:48:05 +08:00
/// </summary>

public class  ManuFacturer
{

private readonly ManuFacturerDAL manDAL=new ManuFacturerDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,long SN)
{
return manDAL.CheckInfo(trans,strFieldName, strFieldValue,SN);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, long SN)
{
return manDAL.GetValueByField(trans,strFieldName, SN);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public ManuFacturerModel GetInfo(SqlTransaction trans,long SN)
{
return manDAL.GetInfo(trans,SN);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public ManuFacturerModel GetCacheInfo(SqlTransaction trans,long SN)
{
string key="Cache_ManuFacturer_Model_"+SN;
object value = CacheHelper.GetCache(key);
if (value != null)
return (ManuFacturerModel)value;
else
{
ManuFacturerModel manModel = manDAL.GetInfo(trans,SN);
CacheHelper.AddCache(key, manModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return manModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,ManuFacturerModel manModel)
{
return manDAL.InsertInfo(trans,manModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,ManuFacturerModel manModel,long SN)
{
string key="Cache_ManuFacturer_Model_"+SN;
CacheHelper.RemoveCache(key);
return manDAL.UpdateInfo(trans,manModel,SN);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,long SN)
{
string key="Cache_ManuFacturer_Model_"+SN;
CacheHelper.RemoveCache(key);
return manDAL.DeleteInfo(trans,SN);
}
#endregion

}
}
