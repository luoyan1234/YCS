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
/// 楼层产品分类-业务逻辑类
/// 创建人:
/// 日期:2017/4/7 18:48:04 +08:00
/// </summary>

public class  FloorProductCategory
{

private readonly FloorProductCategoryDAL floDAL=new FloorProductCategoryDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,int FloorProductCategoryId)
{
return floDAL.CheckInfo(trans,strFieldName, strFieldValue,FloorProductCategoryId);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, int FloorProductCategoryId)
{
return floDAL.GetValueByField(trans,strFieldName, FloorProductCategoryId);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public FloorProductCategoryModel GetInfo(SqlTransaction trans,int FloorProductCategoryId)
{
return floDAL.GetInfo(trans,FloorProductCategoryId);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public FloorProductCategoryModel GetCacheInfo(SqlTransaction trans,int FloorProductCategoryId)
{
string key="Cache_FloorProductCategory_Model_"+FloorProductCategoryId;
object value = CacheHelper.GetCache(key);
if (value != null)
return (FloorProductCategoryModel)value;
else
{
FloorProductCategoryModel floModel = floDAL.GetInfo(trans,FloorProductCategoryId);
CacheHelper.AddCache(key, floModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return floModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,FloorProductCategoryModel floModel)
{
return floDAL.InsertInfo(trans,floModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,FloorProductCategoryModel floModel,int FloorProductCategoryId)
{
string key="Cache_FloorProductCategory_Model_"+FloorProductCategoryId;
CacheHelper.RemoveCache(key);
return floDAL.UpdateInfo(trans,floModel,FloorProductCategoryId);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,int FloorProductCategoryId)
{
string key="Cache_FloorProductCategory_Model_"+FloorProductCategoryId;
CacheHelper.RemoveCache(key);
return floDAL.DeleteInfo(trans,FloorProductCategoryId);
}
#endregion

}
}
