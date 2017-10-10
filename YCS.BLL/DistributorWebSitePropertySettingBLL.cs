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
/// 經銷商網站屬性配置-业务逻辑类
/// 创建人:楊小明
/// 日期:2017/2/18 15:06:08
/// </summary>

public class  DistributorWebSitePropertySettingBLL:Base.DistributorWebSitePropertySetting
{

private readonly DistributorWebSitePropertySettingDAL disDAL=new DistributorWebSitePropertySettingDAL();

#region 取信息分页列表
/// <summary>
/// 取信息分页列表
/// </summary>
public DataTable GetInfoPageList(SqlTransaction trans, Hashtable hs, PageHelper p, out StringBuilder PageStr)
{
return disDAL.GetInfoPageList(trans, hs, p, out PageStr);
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
string FieldOrder="a.DistributorId asc";
return disDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
}
#endregion

#region 取实体集合
/// <summary>
/// 取实体集合
/// </summary>
public List<DistributorWebSitePropertySettingModel> GetModels(SqlTransaction trans)
{
StringBuilder SqlQuery = new StringBuilder();
List<SqlParameter> listParams = new List<SqlParameter>();
string FieldOrder="DistributorId asc";
return disDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
}
#endregion

#region 取实体
/// <summary>
/// 取实体
/// </summary>
public DistributorWebSitePropertySettingModel GetModel(SqlTransaction trans, string DistributorId)
{
StringBuilder SqlQuery = new StringBuilder();
SqlQuery.Append(" and DistributorId=@DistributorId");
List<SqlParameter> listParams = new List<SqlParameter>();
listParams.Add(new SqlParameter("@DistributorId", DistributorId));
return disDAL.GetModel(trans, SqlQuery, listParams);
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
return disDAL.GetAllCount(trans,LeftJoin, SqlQuery, listParams);
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
return disDAL.GetAllSum(trans,LeftJoin, SqlQuery, listParams,"a.DistributorId");
}
#endregion

}
}
