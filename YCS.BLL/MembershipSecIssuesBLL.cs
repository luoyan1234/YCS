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
/// 用户安保问题-业务逻辑类
/// 创建人:luoy
/// 日期:2017/3/4 18:04:35
/// </summary>

public class  MembershipSecIssuesBLL:Base.MembershipSecIssues
{

private readonly MembershipSecIssuesDAL memDAL=new MembershipSecIssuesDAL();

#region 取信息分页列表
/// <summary>
/// 取信息分页列表
/// </summary>
public DataTable GetInfoPageList(SqlTransaction trans, Hashtable hs, PageHelper p, out StringBuilder PageStr)
{
return memDAL.GetInfoPageList(trans, hs, p, out PageStr);
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
string FieldOrder="a.SecIssuesId asc";
return memDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
}
#endregion

#region 取实体集合
/// <summary>
/// 取实体集合
/// </summary>
public List<MembershipSecIssuesModel> GetModels(SqlTransaction trans)
{
StringBuilder SqlQuery = new StringBuilder();
List<SqlParameter> listParams = new List<SqlParameter>();
string FieldOrder="SecIssuesId asc";
return memDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
}
public List<MembershipSecIssuesModel> GetModels(SqlTransaction trans, long MemberId)
{
    StringBuilder SqlQuery = new StringBuilder();
    SqlQuery.Append(" and MemberId=@MemberId");
    List<SqlParameter> listParams = new List<SqlParameter>();
    listParams.Add(new SqlParameter("@MemberId", MemberId));
    string FieldOrder = "SecIssuesId asc";
    return memDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
}
#endregion

#region 取实体
/// <summary>
/// 取实体
/// </summary>
public MembershipSecIssuesModel GetModel(SqlTransaction trans, int SecIssuesId)
{
StringBuilder SqlQuery = new StringBuilder();
SqlQuery.Append(" and SecIssuesId=@SecIssuesId");
List<SqlParameter> listParams = new List<SqlParameter>();
listParams.Add(new SqlParameter("@SecIssuesId", SecIssuesId));
return memDAL.GetModel(trans, SqlQuery, listParams);
}
/// <summary>
/// 取实体
/// </summary>
public MembershipSecIssuesModel GetModel(SqlTransaction trans, long MemberId)
{
    StringBuilder SqlQuery = new StringBuilder();
    SqlQuery.Append(" and MemberId=@MemberId");
    List<SqlParameter> listParams = new List<SqlParameter>();
    listParams.Add(new SqlParameter("@MemberId", MemberId));
    return memDAL.GetModel(trans, SqlQuery, listParams);
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
return memDAL.GetAllCount(trans,LeftJoin, SqlQuery, listParams);
}
public int GetAllCount(SqlTransaction trans, long MemberId)
{
    StringBuilder LeftJoin = new StringBuilder();
    StringBuilder SqlQuery = new StringBuilder();
    SqlQuery.Append(" and MemberId=@MemberId");
    List<SqlParameter> listParams = new List<SqlParameter>();
    listParams.Add(new SqlParameter("@MemberId", MemberId));
    return memDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams);
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
return memDAL.GetAllSum(trans,LeftJoin, SqlQuery, listParams,"a.SecIssuesId");
}
#endregion

}
}
