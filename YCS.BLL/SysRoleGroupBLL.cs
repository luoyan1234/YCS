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
/// 後台角色分組表-业务逻辑类
/// 创建人:楊小明
/// 日期:2017/2/20 11:35:17
/// </summary>

public class  SysRoleGroupBLL:Base.SysRoleGroup
{

private readonly SysRoleGroupDAL sysDAL=new SysRoleGroupDAL();

#region 取信息分页列表
/// <summary>
/// 取信息分页列表
/// </summary>
public DataTable GetInfoPageList(SqlTransaction trans, Hashtable hs, PageHelper p, out StringBuilder PageStr)
{
return sysDAL.GetInfoPageList(trans, hs, p, out PageStr);
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
string FieldOrder="a.SysRoleGroupId asc";
return sysDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
}
#endregion

#region 取实体集合
/// <summary>
/// 取实体集合
/// </summary>
public List<SysRoleGroupModel> GetModels(SqlTransaction trans)
{
StringBuilder SqlQuery = new StringBuilder();
List<SqlParameter> listParams = new List<SqlParameter>();
string FieldOrder="SysRoleGroupId asc";
return sysDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
}
#endregion

#region 取实体
/// <summary>
/// 取实体
/// </summary>
public SysRoleGroupModel GetModel(SqlTransaction trans, int SysRoleGroupId)
{
StringBuilder SqlQuery = new StringBuilder();
SqlQuery.Append(" and SysRoleGroupId=@SysRoleGroupId");
List<SqlParameter> listParams = new List<SqlParameter>();
listParams.Add(new SqlParameter("@SysRoleGroupId", SysRoleGroupId));
return sysDAL.GetModel(trans, SqlQuery, listParams);
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
return sysDAL.GetAllCount(trans,LeftJoin, SqlQuery, listParams);
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
return sysDAL.GetAllSum(trans,LeftJoin, SqlQuery, listParams,"a.SysRoleGroupId");
}
#endregion
#region 下拉列表
/// <summary>
/// 下拉列表
/// </summary>
public List<SelectListItem> GetSelectList(SqlTransaction trans)
{
    DataTable dt = GetDataTable(trans);

    List<SelectListItem> list = new List<SelectListItem>();
    foreach (DataRow dr in dt.Rows)
    {
        list.Add(new SelectListItem() { Text = dr["RoleGroupName"].ToString(), Value = dr["SysRoleGroupId"].ToString() });
    }
    return list;
}
#endregion
#region 取角色分组名称
/// <summary>
/// 取角色分组名称
/// </summary>
public string GetRoleGroupName(SqlTransaction trans, int AdminId)
{
    StringBuilder LeftJoin = new StringBuilder();
    StringBuilder SqlQuery = new StringBuilder();
    SqlQuery.Append(" and SysRoleGroupId=(select SysRoleGroupId from Admin where AdminId=@AdminId)");
    List<SqlParameter> listParams = new List<SqlParameter>();
    listParams.Add(new SqlParameter("@AdminId", AdminId));
    string FieldShow = "a.RoleGroupName";
    string FieldOrder = "a.SysRoleGroupId asc";
    DataTable dt = sysDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
    if (dt.Rows.Count > 0)
    {
        return dt.Rows[0][0].ToString();
    }
    else
    {
        return "";
    }
}
#endregion
#region 读取排序号
/// <summary>
/// 读取排序号
/// </summary>
public int GetSeqNo(SqlTransaction trans)
{
    return sysDAL.GetSeqNo(trans);
}
#endregion

#region 排序信息
/// <summary>
/// 排序信息
/// </summary>
/// <returns></returns>
public void OrderInfo(SqlTransaction trans, int intSeqNo, int intOldSeqNo)
{
    sysDAL.OrderInfo(trans, intSeqNo, intOldSeqNo);
}
#endregion
}
}
