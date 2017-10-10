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
/// 图片路径表 -业务逻辑类
/// 创建人:
/// 日期:2017/4/7 18:48:05 +08:00
/// </summary>

public class  ImageReference
{

private readonly ImageReferenceDAL imaDAL=new ImageReferenceDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans,string strFieldName, string strFieldValue,long SN)
{
return imaDAL.CheckInfo(trans,strFieldName, strFieldValue,SN);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(SqlTransaction trans,string strFieldName, long SN)
{
return imaDAL.GetValueByField(trans,strFieldName, SN);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public ImageReferenceModel GetInfo(SqlTransaction trans,long SN)
{
return imaDAL.GetInfo(trans,SN);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public ImageReferenceModel GetCacheInfo(SqlTransaction trans,long SN)
{
string key="Cache_ImageReference_Model_"+SN;
object value = CacheHelper.GetCache(key);
if (value != null)
return (ImageReferenceModel)value;
else
{
ImageReferenceModel imaModel = imaDAL.GetInfo(trans,SN);
CacheHelper.AddCache(key, imaModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return imaModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public int InsertInfo(SqlTransaction trans,ImageReferenceModel imaModel)
{
return imaDAL.InsertInfo(trans,imaModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public int UpdateInfo(SqlTransaction trans,ImageReferenceModel imaModel,long SN)
{
string key="Cache_ImageReference_Model_"+SN;
CacheHelper.RemoveCache(key);
return imaDAL.UpdateInfo(trans,imaModel,SN);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public int DeleteInfo(SqlTransaction trans,long SN)
{
string key="Cache_ImageReference_Model_"+SN;
CacheHelper.RemoveCache(key);
return imaDAL.DeleteInfo(trans,SN);
}
#endregion

}
}
