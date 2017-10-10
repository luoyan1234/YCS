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
/// 配送费用-业务逻辑类
/// 创建人:楊小明
/// 日期:2017/2/18 15:06:08
/// </summary>

public class  DeliverCostBLL:Base.DeliverCost
{

private readonly DeliverCostDAL delDAL=new DeliverCostDAL();

#region 取信息分页列表
/// <summary>
/// 取信息分页列表
/// </summary>
public DataTable GetInfoPageList(SqlTransaction trans, Hashtable hs, PageHelper p, out StringBuilder PageStr)
{
return delDAL.GetInfoPageList(trans, hs, p, out PageStr);
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
string FieldOrder="a.DeliverCostId asc";
return delDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
}
#endregion

#region 取实体集合
/// <summary>
/// 取实体集合
/// </summary>
public List<DeliverCostModel> GetModels(SqlTransaction trans)
{
StringBuilder SqlQuery = new StringBuilder();
List<SqlParameter> listParams = new List<SqlParameter>();
string FieldOrder="DeliverCostId asc";
return delDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
}
#endregion

#region 取实体
/// <summary>
/// 取实体
/// </summary>
public DeliverCostModel GetModel(SqlTransaction trans, int DeliverCostId)
{
StringBuilder SqlQuery = new StringBuilder();
SqlQuery.Append(" and DeliverCostId=@DeliverCostId");
List<SqlParameter> listParams = new List<SqlParameter>();
listParams.Add(new SqlParameter("@DeliverCostId", DeliverCostId));
return delDAL.GetModel(trans, SqlQuery, listParams);
}
/// <summary>
/// 取实体
/// </summary>
public DeliverCostModel GetModel(SqlTransaction trans, int DeliverId, int AreaType)
{
    StringBuilder SqlQuery = new StringBuilder();
    SqlQuery.Append(" and DeliverId=@DeliverId");
    SqlQuery.Append(" and AreaType=@AreaType");
    List<SqlParameter> listParams = new List<SqlParameter>();
    listParams.Add(new SqlParameter("@DeliverId", DeliverId));
    listParams.Add(new SqlParameter("@AreaType", AreaType));
    return delDAL.GetModel(trans, SqlQuery, listParams);
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
return delDAL.GetAllCount(trans,LeftJoin, SqlQuery, listParams);
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
return delDAL.GetAllSum(trans,LeftJoin, SqlQuery, listParams,"a.DeliverCostId");
}
#endregion
#region
/// <summary>
/// 是否存在相同区域类型的费用设置
/// </summary>
public bool IsHaveAreaType(SqlTransaction trans, int AreaType, int DeliverId, int DeliverCostId)
{
    StringBuilder LeftJoin = new StringBuilder();
    StringBuilder SqlQuery = new StringBuilder();
    SqlQuery.Append(" and AreaType=@AreaType");
    SqlQuery.Append(" and DeliverId=@DeliverId");
    SqlQuery.Append(" and DeliverCostId<>@DeliverCostId");
    List<SqlParameter> listParams = new List<SqlParameter>();
    listParams.Add(new SqlParameter("@AreaType", AreaType));
    listParams.Add(new SqlParameter("@DeliverId", DeliverId));
    listParams.Add(new SqlParameter("@DeliverCostId", DeliverCostId));
    return delDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams) > 0;
}
#endregion
#region 获取运费
/// <summary>
/// 获取运费
/// </summary>
/// <param name="DeliverId"></param>
/// <param name="AreaType"></param>
/// <param name="TotalWeight"></param>
/// <returns></returns>
public decimal GetDeliverCost(int DeliverId, int AreaType, decimal TotalWeight)
{
    decimal DeliverCost = 0;
    DeliverCostModel delCosModel = GetModel(null, DeliverId, AreaType);
    if (delCosModel != null)
    {
        decimal TotalAddedWeight = TotalWeight - delCosModel.FirstWeight;//总续重
        TotalAddedWeight = Math.Max(TotalAddedWeight, 0);
        DeliverCost = delCosModel.FirstCost + TotalAddedWeight / delCosModel.AddedWeight * delCosModel.AddedCost;
    }
    return DeliverCost;
}
#endregion
}
}
