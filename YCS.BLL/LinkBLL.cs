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
/// 链接-业务逻辑类
/// 创建人:楊小明
/// 日期:2017/2/18 15:06:08
/// </summary>

public class  LinkBLL:Base.Link
{

private readonly LinkDAL linDAL=new LinkDAL();

#region 取信息分页列表
/// <summary>
/// 取信息分页列表
/// </summary>
public DataTable GetInfoPageList(SqlTransaction trans, Hashtable hs, PageHelper p, out StringBuilder PageStr)
{
return linDAL.GetInfoPageList(trans, hs, p, out PageStr);
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
string FieldOrder="a.LinkId asc";
return linDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
}
#endregion

#region 取实体集合
/// <summary>
/// 取实体集合
/// </summary>
public List<LinkModel> GetModels(SqlTransaction trans)
{
StringBuilder SqlQuery = new StringBuilder();
List<SqlParameter> listParams = new List<SqlParameter>();
string FieldOrder="LinkId asc";
return linDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
}
/// <summary>
/// 取实体集合
/// </summary>
public List<LinkModel> GetModels(SqlTransaction trans, int TypeId)
{
    StringBuilder SqlQuery = new StringBuilder();
    SqlQuery.Append(" and IsClose=@IsClose");
    SqlQuery.Append(" and TypeId=@TypeId");
    List<SqlParameter> listParams = new List<SqlParameter>();
    listParams.Add(new SqlParameter("@IsClose", EnumList.CloseStatus.Open.ToInt()));
    listParams.Add(new SqlParameter("@TypeId", TypeId));
    string FieldOrder = "SeqNo asc";
    return linDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
}
#endregion

#region 取实体
/// <summary>
/// 取实体
/// </summary>
public LinkModel GetModel(SqlTransaction trans, int LinkId)
{
StringBuilder SqlQuery = new StringBuilder();
SqlQuery.Append(" and LinkId=@LinkId");
List<SqlParameter> listParams = new List<SqlParameter>();
listParams.Add(new SqlParameter("@LinkId", LinkId));
return linDAL.GetModel(trans, SqlQuery, listParams);
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
return linDAL.GetAllCount(trans,LeftJoin, SqlQuery, listParams);
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
return linDAL.GetAllSum(trans,LeftJoin, SqlQuery, listParams,"a.LinkId");
}
#endregion

#region 读取排序号
/// <summary>
/// 读取排序号
/// </summary>
public int GetSeqNo(SqlTransaction trans)
{
    return linDAL.GetSeqNo(trans);
}
#endregion

#region 排序信息
/// <summary>
/// 排序信息
/// </summary>
/// <returns></returns>
public void OrderInfo(SqlTransaction trans, int intSeqNo, int intOldSeqNo)
{
    linDAL.OrderInfo(trans, intSeqNo, intOldSeqNo);
}
#endregion
}
}
