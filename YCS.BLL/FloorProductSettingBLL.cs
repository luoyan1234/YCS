using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;
using System.Web.Caching;
using YCS.Common;
using YCS.Model;
using YCS.DAL;

namespace YCS.BLL
{
/// <summary>
/// 楼层热卖推荐商品设置-业务逻辑类
/// 创建人:luoy
/// 日期:2017/3/15 13:41:19 +08:00
/// </summary>

public class  FloorProductSettingBLL:Base.FloorProductSetting
{

private readonly FloorProductSettingDAL floDAL=new FloorProductSettingDAL();

#region 取信息分页列表
/// <summary>
/// 取信息分页列表
/// </summary>
public DataTable GetInfoPageList(SqlTransaction trans, Hashtable hs, PageHelper p, out StringBuilder PageStr)
{
return floDAL.GetInfoPageList(trans, hs, p, out PageStr);
}
#endregion

#region 取DataTable
/// <summary>
/// 取DataTable
/// </summary>
public DataTable GetDataTable(SqlTransaction trans)
{
StringBuilder LeftJoin = new StringBuilder();
StringBuilder SqlQuery = new StringBuilder();
List<SqlParameter> listParams = new List<SqlParameter>();
string FieldShow="a.*";
string FieldOrder="a.FloorProductSettingId asc";
return floDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
}
#endregion

#region 取实体集合
/// <summary>
/// 取实体集合
/// </summary>
public List<FloorProductSettingModel> GetModels(SqlTransaction trans)
{
StringBuilder SqlQuery = new StringBuilder();
List<SqlParameter> listParams = new List<SqlParameter>();
string FieldOrder="FloorProductSettingId asc";
return floDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
}
/// <summary>
/// 取实体集合
/// </summary>
public List<FloorProductSettingModel> GetModels(SqlTransaction trans, int FloorId, int FloorProductType, bool IsClose, int topNum=0)
{
    string FiledShow = "a.*,b.ProductSpuPic,b.ProductSpuName";
    string LeftJoin = " a left join ProductSpu b on a.ProductSpuId=b.ProductSpuId";
    StringBuilder SqlQuery = new StringBuilder();
    SqlQuery.Append(" and FloorId = @FloorId");
    SqlQuery.Append(" and FloorProductType = @FloorProductType");
    SqlQuery.Append(" and IsClose = @IsClose");
    List<SqlParameter> listParams = new List<SqlParameter>();
    listParams.Add(new SqlParameter("@FloorId", FloorId));
    listParams.Add(new SqlParameter("@FloorProductType", FloorProductType));
    listParams.Add(new SqlParameter("@IsClose", IsClose));
    string FieldOrder = "FloorProductSettingId asc";
    return floDAL.GetModels(trans, SqlQuery, listParams, topNum, FieldOrder, FiledShow, LeftJoin);
}/// <summary>
/// 取实体集合
/// </summary>
public List<FloorProductSettingModel> GetModelsForeign(SqlTransaction trans, int FloorId, int FloorProductType, bool IsClose, int topNum = 0)
{
    string FiledShow = "a.*,b.ProductSpuPic,b.ProductSpuName";
    string LeftJoin = " a left join ProductSpu b on a.ProductSpuId=b.ProductSpuId";
    StringBuilder SqlQuery = new StringBuilder();
    SqlQuery.Append(" and FloorId = @FloorId");
    SqlQuery.Append(" and FloorProductType = @FloorProductType");
    SqlQuery.Append(" and IsClose = @IsClose");
    SqlQuery.Append(" and b.Status=" + EnumList.ProductSpuStatus.上架.ToInt() + " and (OnShelfDate is not null and getDate() >= OnShelfDate) and (OffShelfDate is not null and getDate() <= OffShelfDate)");
    List<SqlParameter> listParams = new List<SqlParameter>();
    listParams.Add(new SqlParameter("@FloorId", FloorId));
    listParams.Add(new SqlParameter("@FloorProductType", FloorProductType));
    listParams.Add(new SqlParameter("@IsClose", IsClose));
    string FieldOrder = "a.SeqNo asc";
    return floDAL.GetModelsForeign(trans, SqlQuery, listParams, topNum, FieldOrder, FiledShow, LeftJoin);
}
#endregion

#region 取实体
/// <summary>
/// 取实体
/// </summary>
public FloorProductSettingModel GetModel(SqlTransaction trans, int FloorProductSettingId)
{
StringBuilder SqlQuery = new StringBuilder();
SqlQuery.Append(" and FloorProductSettingId=@FloorProductSettingId");
List<SqlParameter> listParams = new List<SqlParameter>();
listParams.Add(new SqlParameter("@FloorProductSettingId", FloorProductSettingId));
return floDAL.GetModel(trans, SqlQuery, listParams);
}
#endregion

#region 取记录总数
/// <summary>
/// 取记录总数
/// </summary>
public int GetAllCount(SqlTransaction trans)
{
StringBuilder LeftJoin = new StringBuilder();
StringBuilder SqlQuery = new StringBuilder();
List<SqlParameter> listParams = new List<SqlParameter>();
return floDAL.GetAllCount(trans,LeftJoin, SqlQuery, listParams);
}
#endregion

#region 取字段总和
/// <summary>
/// 取字段总和
/// </summary>
public decimal GetAllSum(SqlTransaction trans)
{
StringBuilder LeftJoin = new StringBuilder();
StringBuilder SqlQuery = new StringBuilder();
List<SqlParameter> listParams = new List<SqlParameter>();
return floDAL.GetAllSum(trans,LeftJoin, SqlQuery, listParams,"a.FloorProductSettingId");
}
#endregion
#region 读取排序号
/// <summary>
/// 读取排序号
/// </summary>
public int GetSeqNo(SqlTransaction trans, int floorId, int floorProductType)
{
    return floDAL.GetSeqNo(trans, floorId, floorProductType);
}
#endregion

#region 排序信息
/// <summary>
/// 排序信息
/// </summary>
/// <returns></returns>
public void OrderInfo(SqlTransaction trans, int floorId, int floorProductType, int intListID, int intOldListID)
{
    floDAL.OrderInfo(trans, floorId, floorProductType, intListID, intOldListID);
}
#endregion
}
}
