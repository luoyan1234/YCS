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
/// 上传文件至Aips-业务逻辑类
/// 创建人:杨小明
/// 日期:2017/5/10 10:44:09 +08:00
/// </summary>

public class  UploadToAips
{

private readonly UploadToAipsDAL uplDAL=new UploadToAipsDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,int UploadToAipsId)
{
return uplDAL.CheckInfo(trans,strFieldName, strFieldValue,UploadToAipsId);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, int UploadToAipsId)
{
return uplDAL.GetValueByField(trans,strFieldName, UploadToAipsId);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public UploadToAipsModel GetInfo(SqlTransaction trans,int UploadToAipsId)
{
return uplDAL.GetInfo(trans,UploadToAipsId);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public UploadToAipsModel GetCacheInfo(SqlTransaction trans,int UploadToAipsId)
{
string key="Cache_UploadToAips_Model_"+UploadToAipsId;
object value = CacheHelper.GetCache(key);
if (value != null)
return (UploadToAipsModel)value;
else
{
UploadToAipsModel uplModel = uplDAL.GetInfo(trans,UploadToAipsId);
CacheHelper.AddCache(key, uplModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return uplModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,UploadToAipsModel uplModel)
{
return uplDAL.InsertInfo(trans,uplModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,UploadToAipsModel uplModel,int UploadToAipsId)
{
string key="Cache_UploadToAips_Model_"+UploadToAipsId;
CacheHelper.RemoveCache(key);
return uplDAL.UpdateInfo(trans,uplModel,UploadToAipsId);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,int UploadToAipsId)
{
string key="Cache_UploadToAips_Model_"+UploadToAipsId;
CacheHelper.RemoveCache(key);
return uplDAL.DeleteInfo(trans,UploadToAipsId);
}
#endregion

}
}
