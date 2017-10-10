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
/// 印刷商-业务逻辑类
/// 创建人:楊小明
/// 日期:2017/2/18 15:06:08
/// </summary>

public class  ManuFacturerBLL:Base.ManuFacturer
{

private readonly ManuFacturerDAL manDAL=new ManuFacturerDAL();

#region 取信息分页列表
/// <summary>
/// 取信息分页列表
/// </summary>
public DataTable GetInfoPageList(SqlTransaction trans, Hashtable hs, PageHelper p, out StringBuilder PageStr)
{
return manDAL.GetInfoPageList(trans, hs, p, out PageStr);
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
    string FieldShow = "a.*";
    string FieldOrder = "a.ManufacturerId asc";
    return manDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
}
    /// <summary>
/// 取DataTable
/// </summary>
public DataTable GetDataTable(SqlTransaction trans, bool IsClose)
{
    StringBuilder LeftJoin = new StringBuilder();
    StringBuilder SqlQuery = new StringBuilder();
    SqlQuery.Append(" and IsClose=@IsClose ");
    List<SqlParameter> listParams = new List<SqlParameter>();
    listParams.Add(new SqlParameter("@IsClose", IsClose));
    string FieldShow = "a.*";
    string FieldOrder = "a.ManufacturerId asc";
    return manDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
}
#endregion

#region 取实体集合
/// <summary>
/// 取实体集合
/// </summary>
public List<ManuFacturerModel> GetModels(SqlTransaction trans)
{
StringBuilder SqlQuery = new StringBuilder();
List<SqlParameter> listParams = new List<SqlParameter>();
string FieldOrder="ManufacturerId asc";
return manDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
}
#endregion

#region 取实体
/// <summary>
/// 取实体
/// </summary>
public ManuFacturerModel GetModel(SqlTransaction trans, string ManufacturerId)
{
StringBuilder SqlQuery = new StringBuilder();
SqlQuery.Append(" and ManufacturerId=@ManufacturerId ");
List<SqlParameter> listParams = new List<SqlParameter>();
listParams.Add(new SqlParameter("@ManufacturerId", ManufacturerId));
return manDAL.GetModel(trans, SqlQuery, listParams);
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
return manDAL.GetAllCount(trans,LeftJoin, SqlQuery, listParams);
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
return manDAL.GetAllSum(trans,LeftJoin, SqlQuery, listParams,"a.ManufacturerId");
}
#endregion
#region 下拉列表
/// <summary>
/// 下拉列表
/// </summary>
public List<SelectListItem> GetSelectList(SqlTransaction trans)
{
    DataTable dt = GetDataTable(trans,false);

    List<SelectListItem> list = new List<SelectListItem>();
    foreach (DataRow dr in dt.Rows)
    {
        list.Add(new SelectListItem() { Text = dr["ManuFacturerName"].ToString(), Value = dr["ManuFacturerId"].ToString() });
    }
    return list;
}
#endregion
}
}
