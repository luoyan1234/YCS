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
/// 短信标签表-业务逻辑类
/// 创建人:楊小明
/// 日期:2017/2/18 15:06:09
/// </summary>

public class  SMSLabelBLL:Base.SMSLabel
{

private readonly SMSLabelDAL smsDAL=new SMSLabelDAL();

#region 取信息分页列表
/// <summary>
/// 取信息分页列表
/// </summary>
public DataTable GetInfoPageList(SqlTransaction trans, Hashtable hs, PageHelper p, out StringBuilder PageStr)
{
return smsDAL.GetInfoPageList(trans, hs, p, out PageStr);
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
string FieldOrder="a.SMSLabelId asc";
return smsDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
}
#endregion

#region 取实体集合
/// <summary>
/// 取实体集合
/// </summary>
public List<SMSLabelModel> GetModels(SqlTransaction trans)
{
StringBuilder SqlQuery = new StringBuilder();
List<SqlParameter> listParams = new List<SqlParameter>();
string FieldOrder="SMSLabelId asc";
return smsDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
}
/// <summary>
/// 取实体集合
/// </summary>
public List<SMSLabelModel> GetModels(SqlTransaction trans, int isClose)
{
    StringBuilder SqlQuery = new StringBuilder();
    SqlQuery.Append(" and IsClose=" + isClose);
    List<SqlParameter> listParams = new List<SqlParameter>();
    string FieldOrder = "SMSLabelId asc";
    return smsDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
}
#endregion

#region 取实体
/// <summary>
/// 取实体
/// </summary>
public SMSLabelModel GetModel(SqlTransaction trans, int SMSLabelId)
{
StringBuilder SqlQuery = new StringBuilder();
SqlQuery.Append(" and SMSLabelId=@SMSLabelId");
List<SqlParameter> listParams = new List<SqlParameter>();
listParams.Add(new SqlParameter("@SMSLabelId", SMSLabelId));
return smsDAL.GetModel(trans, SqlQuery, listParams);
}
/// <summary>
/// 取实体
/// </summary>
public SMSLabelModel GetModel(SqlTransaction trans, string labelName)
{
    StringBuilder SqlQuery = new StringBuilder();
    SqlQuery.Append(" and LabelName=@LabelName");
    List<SqlParameter> listParams = new List<SqlParameter>();
    listParams.Add(new SqlParameter("@LabelName", labelName));
    return smsDAL.GetModel(trans, SqlQuery, listParams);
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
return smsDAL.GetAllCount(trans,LeftJoin, SqlQuery, listParams);
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
return smsDAL.GetAllSum(trans,LeftJoin, SqlQuery, listParams,"a.SMSLabelId");
}
#endregion

}
}
