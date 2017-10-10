﻿using System;
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
/// 日期:2017/3/13 10:18:05 +08:00
/// </summary>

public class  ProductTemplateTypeBLL:Base.ProductTemplateType
{

private readonly ProductTemplateTypeDAL proDAL=new ProductTemplateTypeDAL();

#region 取信息分页列表
/// <summary>
/// 取信息分页列表
/// </summary>
public DataTable GetInfoPageList(SqlTransaction trans, Hashtable hs, PageHelper p, out StringBuilder PageStr)
{
return proDAL.GetInfoPageList(trans, hs, p, out PageStr);
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
string FieldOrder="a.SN asc";
return proDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
}
#endregion

#region 取实体集合
/// <summary>
/// 取实体集合
/// </summary>
public List<ProductTemplateTypeModel> GetModels(SqlTransaction trans)
{
StringBuilder SqlQuery = new StringBuilder();
List<SqlParameter> listParams = new List<SqlParameter>();
string FieldOrder="SN asc";
return proDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
}
#endregion

#region 取实体
/// <summary>
/// 取实体
/// </summary>
public ProductTemplateTypeModel GetModel(SqlTransaction trans, long SN)
{
StringBuilder SqlQuery = new StringBuilder();
SqlQuery.Append(" and SN=@SN");
List<SqlParameter> listParams = new List<SqlParameter>();
listParams.Add(new SqlParameter("@SN", SN));
return proDAL.GetModel(trans, SqlQuery, listParams);
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
return proDAL.GetAllCount(trans,LeftJoin, SqlQuery, listParams);
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
return proDAL.GetAllSum(trans,LeftJoin, SqlQuery, listParams,"a.SN");
}
#endregion

}
}
