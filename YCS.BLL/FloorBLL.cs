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
/// 楼层-业务逻辑类
/// 创建人:luoy
/// 日期:2017/3/15 13:40:33 +08:00
/// </summary>

public class  FloorBLL:Base.Floor
{

private readonly FloorDAL floDAL=new FloorDAL();

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
string FieldOrder="a.FloorId asc";
return floDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
}
/// <summary>
/// 取DataTable
/// </summary>
public DataTable GetDataTable(SqlTransaction trans,string distributorId,bool isClose)
{
    StringBuilder LeftJoin = new StringBuilder();
    StringBuilder SqlQuery = new StringBuilder();
    SqlQuery.Append(" and DistributorId=@DistributorId");
    SqlQuery.Append(" and IsClose=@IsClose");
    List<SqlParameter> listParams = new List<SqlParameter>();
    listParams.Add(new SqlParameter("DistributorId", distributorId));
    listParams.Add(new SqlParameter("IsClose", isClose));
    string FieldShow = "a.*";
    string FieldOrder = "a.SeqNo asc";
    return floDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
}
#endregion

#region 取实体集合
/// <summary>
/// 取实体集合
/// </summary>
public List<FloorModel> GetModels(SqlTransaction trans)
{
StringBuilder SqlQuery = new StringBuilder();
List<SqlParameter> listParams = new List<SqlParameter>();
string FieldOrder="FloorId asc";
return floDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
}
#endregion

#region 取实体
/// <summary>
/// 取实体
/// </summary>
public FloorModel GetModel(SqlTransaction trans, int FloorId)
{
StringBuilder SqlQuery = new StringBuilder();
SqlQuery.Append(" and FloorId=@FloorId");
List<SqlParameter> listParams = new List<SqlParameter>();
listParams.Add(new SqlParameter("@FloorId", FloorId));
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
/// <summary>
/// 取记录总数
/// </summary>
public int GetAllCount(SqlTransaction trans, string distributorId, bool isClose)
{
    StringBuilder LeftJoin = new StringBuilder();
    StringBuilder SqlQuery = new StringBuilder();
    SqlQuery.Append(" and DistributorId=@DistributorId");
    SqlQuery.Append(" and IsClose=@IsClose");
    List<SqlParameter> listParams = new List<SqlParameter>();
    listParams.Add(new SqlParameter("DistributorId", distributorId));
    listParams.Add(new SqlParameter("IsClose", isClose));
    return floDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams);
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
return floDAL.GetAllSum(trans,LeftJoin, SqlQuery, listParams,"a.FloorId");
}
#endregion
#region 读取排序号
/// <summary>
/// 读取排序号
/// </summary>
public int GetSeqNo(SqlTransaction trans)
{
    return floDAL.GetSeqNo(trans);
}
#endregion

#region 排序信息
/// <summary>
/// 排序信息
/// </summary>
/// <returns></returns>
public void OrderInfo(SqlTransaction trans, int intSeqNo, int intOldSeqNo)
{
    floDAL.OrderInfo(trans, intSeqNo, intOldSeqNo);
}
#endregion
#region 下拉列表
/// <summary>
/// 下拉列表
/// </summary>
public List<SelectListItem> GetSelectList(SqlTransaction trans, string distributorId)
{
    DataTable dt = GetDataTable(trans, distributorId,false);

    List<SelectListItem> list = new List<SelectListItem>();
    foreach (DataRow dr in dt.Rows)
    {
        list.Add(new SelectListItem() { Text = dr["FloorName"].ToString(), Value = dr["FloorId"].ToString() });
    }
    return list;
}
#endregion
}
}
