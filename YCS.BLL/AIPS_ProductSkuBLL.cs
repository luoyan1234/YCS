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
/// -业务逻辑类
/// 创建人:杨小明
/// 日期:2017/3/13 10:30:21 +08:00
/// </summary>

public class  AIPS_ProductSkuBLL:Base.AIPS_ProductSku
{

private readonly AIPS_ProductSkuDAL aipDAL=new AIPS_ProductSkuDAL();

#region 取信息分页列表
/// <summary>
/// 取信息分页列表
/// </summary>
public DataTable GetInfoPageList(SqlTransaction trans, Hashtable hs, PageHelper p, out StringBuilder PageStr)
{
return aipDAL.GetInfoPageList(trans, hs, p, out PageStr);
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
string FieldOrder="a.ID asc";
return aipDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
}
#endregion

#region 取实体集合
/// <summary>
/// 取实体集合
/// </summary>
public List<AIPS_ProductSkuModel> GetModels(SqlTransaction trans)
{
StringBuilder SqlQuery = new StringBuilder();
List<SqlParameter> listParams = new List<SqlParameter>();
string FieldOrder="ID asc";
return aipDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
}
#endregion

#region 取实体
/// <summary>
/// 取实体
/// </summary>
public AIPS_ProductSkuModel GetModel(SqlTransaction trans, int ID)
{
StringBuilder SqlQuery = new StringBuilder();
SqlQuery.Append(" and ID=@ID");
List<SqlParameter> listParams = new List<SqlParameter>();
listParams.Add(new SqlParameter("@ID", ID));
return aipDAL.GetModel(trans, SqlQuery, listParams);
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
return aipDAL.GetAllCount(trans,LeftJoin, SqlQuery, listParams);
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
return aipDAL.GetAllSum(trans,LeftJoin, SqlQuery, listParams,"a.ID");
}
#endregion

}
}
