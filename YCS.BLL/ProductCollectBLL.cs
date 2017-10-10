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
/// 商品收藏-业务逻辑类
/// 创建人:楊小明
/// 日期:2017/2/18 15:06:09
/// </summary>

public class  ProductCollectBLL:Base.ProductCollect
{

private readonly ProductCollectDAL proDAL=new ProductCollectDAL();

#region 取信息分页列表
/// <summary>
/// 取信息分页列表
/// </summary>
public DataTable GetInfoPageList(SqlTransaction trans, Hashtable hs, PageHelper p, out StringBuilder PageStr)
{
return proDAL.GetInfoPageList(trans, hs, p, out PageStr);
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
string FieldOrder="a.ProductCollectId asc";
return proDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
}
/// <summary>
/// 取DataTable
/// </summary>
public DataTable GetDataTable(SqlTransaction trans, int ProcessType, long MemberId, string DistributorId, int topNum)
{
    StringBuilder LeftJoin = new StringBuilder();
    LeftJoin.Append(" left join v_Product b on a.ProductSpuId=b.ProductSpuId");
    StringBuilder SqlQuery = new StringBuilder();
    SqlQuery.Append(" and a.MemberId=@MemberId");
    SqlQuery.Append(" and a.DistributorId=@DistributorId");
    SqlQuery.Append(" and b.Status=" + EnumList.ProductSpuStatus.上架.ToInt() + " and b.OnShelfDate<=getDate() and b.OffShelfDate>=getDate()");
    List<SqlParameter> listParams = new List<SqlParameter>();
    listParams.Add(new SqlParameter("@MemberId", MemberId));
    listParams.Add(new SqlParameter("@DistributorId", DistributorId));
    if (ProcessType > 0)
    {
        SqlQuery.Append(" and b.Type=@Type");
        listParams.Add(new SqlParameter("@Type", ProcessType));
    }
    string FieldOrder = "LastUpdateDate desc";
    string FieldShow = "a.*,b.ProductSpuName,b.ProductSpuPic ";
    if (topNum > 0)
    {
        FieldShow = "top " + topNum + " " + FieldShow;
    }
    return proDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
}
#endregion

#region 取实体集合
/// <summary>
/// 取实体集合
/// </summary>
public List<ProductCollectModel> GetModels(SqlTransaction trans)
{
StringBuilder SqlQuery = new StringBuilder();
List<SqlParameter> listParams = new List<SqlParameter>();
string FieldOrder="ProductCollectId asc";
return proDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
}
/// <summary>
/// 取实体集合
/// </summary>
public List<ProductCollectModel> GetModels(SqlTransaction trans, int ProcessType, long MemberId, string DistributorId,int topNum)
{
    StringBuilder LeftJoin = new StringBuilder();
    LeftJoin.Append(" left join v_Product b on a.ProductSpuId=b.ProductSpuId");
    StringBuilder SqlQuery = new StringBuilder();
    SqlQuery.Append(" and a.MemberId=@MemberId");
    SqlQuery.Append(" and a.DistributorId=@DistributorId");
    SqlQuery.Append(" and b.Status=" + EnumList.ProductSpuStatus.上架.ToInt() + " and b.OnShelfDate<=getDate() and b.OffShelfDate>=getDate()");
    List<SqlParameter> listParams = new List<SqlParameter>();
    listParams.Add(new SqlParameter("@MemberId", MemberId));
    listParams.Add(new SqlParameter("@DistributorId", DistributorId));
    if (ProcessType > 0)
    {
        SqlQuery.Append(" and b.Type=@Type");
        listParams.Add(new SqlParameter("@Type", ProcessType));
    }
    string FieldOrder = "LastUpdateDate desc";
    return proDAL.GetModels(trans, SqlQuery, listParams, topNum, FieldOrder);
}
#endregion

#region 取实体
/// <summary>
/// 取实体
/// </summary>
public ProductCollectModel GetModel(SqlTransaction trans, int ProductCollectId)
{
StringBuilder SqlQuery = new StringBuilder();
SqlQuery.Append(" and ProductCollectId=@ProductCollectId");
List<SqlParameter> listParams = new List<SqlParameter>();
listParams.Add(new SqlParameter("@ProductCollectId", ProductCollectId));
return proDAL.GetModel(trans, SqlQuery, listParams);
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
return proDAL.GetAllCount(trans,LeftJoin, SqlQuery, listParams);
}
/// <summary>
/// 取记录总数
/// </summary>
public int GetAllCount(SqlTransaction trans, int ProcessType, long MemberId, string DistributorId)
{
    StringBuilder LeftJoin = new StringBuilder();
    LeftJoin.Append(" left join v_Product b on a.ProductSpuId=b.ProductSpuId");
    StringBuilder SqlQuery = new StringBuilder();
    SqlQuery.Append(" and a.MemberId=@MemberId");
    SqlQuery.Append(" and a.DistributorId=@DistributorId");
    SqlQuery.Append(" and b.Status=" + EnumList.ProductSpuStatus.上架.ToInt() + " and b.OnShelfDate<=getDate() and b.OffShelfDate>=getDate()");
    List<SqlParameter> listParams = new List<SqlParameter>();
    listParams.Add(new SqlParameter("@MemberId", MemberId));
    listParams.Add(new SqlParameter("@DistributorId", DistributorId));
    if (ProcessType > 0)
    {
        SqlQuery.Append(" and b.Type=@Type");
        listParams.Add(new SqlParameter("@Type", ProcessType));
    }
    return proDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams);
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
return proDAL.GetAllSum(trans,LeftJoin, SqlQuery, listParams,"a.ProductCollectId");
}
#endregion
/// <summary>
/// 是否已关注
/// </summary>
public bool IsCollect(SqlTransaction trans, string ProductSpuId, long MemberId)
{
    StringBuilder LeftJoin = new StringBuilder();
    StringBuilder SqlQuery = new StringBuilder();
    SqlQuery.Append(" and ProductSpuId=@ProductSpuId");
    SqlQuery.Append(" and MemberId=@MemberId");
    List<SqlParameter> listParams = new List<SqlParameter>();
    listParams.Add(new SqlParameter("@ProductSpuId", ProductSpuId));
    listParams.Add(new SqlParameter("@MemberId", MemberId));
    return proDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams) > 0;
}
}
}
