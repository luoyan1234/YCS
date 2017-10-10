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
/// 後台模塊權限列舉表-业务逻辑类
/// 创建人:楊小明
/// 日期:2017/2/20 11:35:17
/// </summary>

public class  SysLimitBLL:Base.SysLimit
{

private readonly SysLimitDAL sysDAL=new SysLimitDAL();

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
string FieldOrder="a.SysLimitId asc";
return sysDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
}
#endregion

#region 取实体集合
/// <summary>
/// 取实体集合
/// </summary>
public List<SysLimitModel> GetModels(SqlTransaction trans)
{
StringBuilder SqlQuery = new StringBuilder();
List<SqlParameter> listParams = new List<SqlParameter>();
string FieldOrder="SysLimitId asc";
return sysDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
}
#endregion

#region 取实体
/// <summary>
/// 取实体
/// </summary>
public SysLimitModel GetModel(SqlTransaction trans, int SysLimitId)
{
StringBuilder SqlQuery = new StringBuilder();
SqlQuery.Append(" and SysLimitId=@SysLimitId");
List<SqlParameter> listParams = new List<SqlParameter>();
listParams.Add(new SqlParameter("@SysLimitId", SysLimitId));
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
return sysDAL.GetAllSum(trans,LeftJoin, SqlQuery, listParams,"a.SysLimitId");
}
#endregion
#region 取列表(复选)
/// <summary>
/// 取列表(复选)
/// </summary>
public string GetLimitList(SqlTransaction trans, string SysLimitIds)
{
    return sysDAL.GetLimitList(trans, SysLimitIds);
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
#region 检查权限是否存在
/// <summary>
/// 检查权限是否存在
/// </summary>
public bool IsExist(SqlTransaction trans, string strLimitValue)
{
    StringBuilder LeftJoin = new StringBuilder();
    StringBuilder SqlQuery = new StringBuilder();
    SqlQuery.Append(" and IsClose=0");
    SqlQuery.Append(" and LimitValue=@LimitValue");
    List<SqlParameter> listParams = new List<SqlParameter>();
    listParams.Add(new SqlParameter("@LimitValue", strLimitValue));
    return sysDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams) > 0;
}
#endregion
}
}
