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
/// 後台管理員日誌表-业务逻辑类
/// 创建人:楊小明
/// 日期:2017/2/20 11:35:13
/// </summary>

public class  AdminLogBLL:Base.AdminLog
{

private readonly AdminLogDAL admDAL=new AdminLogDAL();

#region 取信息分页列表
/// <summary>
/// 取信息分页列表
/// </summary>
public DataTable GetInfoPageList(SqlTransaction trans, Hashtable hs, PageHelper p, out StringBuilder PageStr)
{
return admDAL.GetInfoPageList(trans, hs, p, out PageStr);
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
string FieldOrder="a.AdminLogId asc";
return admDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
}
#endregion

#region 取实体集合
/// <summary>
/// 取实体集合
/// </summary>
public List<AdminLogModel> GetModels(SqlTransaction trans)
{
StringBuilder SqlQuery = new StringBuilder();
List<SqlParameter> listParams = new List<SqlParameter>();
string FieldOrder="AdminLogId asc";
return admDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
}
#endregion

#region 取实体
/// <summary>
/// 取实体
/// </summary>
public AdminLogModel GetModel(SqlTransaction trans, int AdminLogId)
{
StringBuilder SqlQuery = new StringBuilder();
SqlQuery.Append(" and AdminLogId=@AdminLogId");
List<SqlParameter> listParams = new List<SqlParameter>();
listParams.Add(new SqlParameter("@AdminLogId", AdminLogId));
return admDAL.GetModel(trans, SqlQuery, listParams);
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
return admDAL.GetAllCount(trans,LeftJoin, SqlQuery, listParams);
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
return admDAL.GetAllSum(trans,LeftJoin, SqlQuery, listParams,"a.AdminLogId");
}
#endregion

#region 添加操作日志
/// <summary>
/// 添加操作日志
/// </summary>
public void InsertLog(SqlTransaction trans, EnumList.AdminLogActionType actionType, string strLogContent)
{
    try
    {
        AdminLogModel admLogModel = new AdminLogModel();
        admLogModel.LogAction = (int)actionType;
        admLogModel.LogContent = strLogContent;
        admLogModel.ScriptFile = HttpContext.Current.Request.RawUrl.ToString2();
        admLogModel.IPAddress = HttpContext.Current.Request.UserHostAddress.ToString2();
        admLogModel.AdminId = HttpContext.Current.Session["AdminId"].ToInt();
        admLogModel.CreationDate = DateTimeOffset.Now;
        admDAL.InsertInfo(trans, admLogModel);
    }
    catch
    {

    }
}
#endregion

}
}
