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
/// 經銷商對應產品資料表-业务逻辑类
/// 创建人:杨小明
/// 日期:2017/2/27 9:15:37
/// </summary>

public class  DistributorProductSettingBLL:Base.DistributorProductSetting
{

private readonly DistributorProductSettingDAL disDAL=new DistributorProductSettingDAL();

#region 取信息分页列表
/// <summary>
/// 取信息分页列表
/// </summary>
public DataTable GetInfoPageList(SqlTransaction trans, Hashtable hs, PageHelper p, out StringBuilder PageStr, out int PageCount, out int AllCount)
{
    return disDAL.GetInfoPageList(trans, hs, p, out PageStr, out PageCount, out AllCount);
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
return disDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
}
#endregion

#region 取实体集合
/// <summary>
/// 取实体集合
/// </summary>
public List<DistributorProductSettingModel> GetModels(SqlTransaction trans)
{
StringBuilder SqlQuery = new StringBuilder();
List<SqlParameter> listParams = new List<SqlParameter>();
string FieldOrder="SN asc";
return disDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
}
#endregion

#region 取实体
/// <summary>
/// 取实体
/// </summary>
public DistributorProductSettingModel GetModel(SqlTransaction trans, int SN)
{
StringBuilder SqlQuery = new StringBuilder();
SqlQuery.Append(" and SN=@SN");
List<SqlParameter> listParams = new List<SqlParameter>();
listParams.Add(new SqlParameter("@SN", SN));
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
return disDAL.GetAllSum(trans,LeftJoin, SqlQuery, listParams,"a.SN");
}
#endregion
#region 修改商品资料之后修改经销商商品设置
/// <summary>
/// 修改商品资料之后修改经销商商品设置
/// </summary>
public int UpdateDisSpu(SqlTransaction trans, string ProductSpuId, DateTimeOffset OnShelfDate, DateTimeOffset OffShelfDate, string ObsoleteReason)
{
    StringBuilder SqlQuery = new StringBuilder();
    SqlQuery.Append(" and ProductSpuId=@ProductSpuId");
    List<SqlParameter> listParams = new List<SqlParameter>();
    listParams.Add(new SqlParameter("@ProductSpuId", ProductSpuId));
    return disDAL.UpdateDisSpu(trans, ProductSpuId, OnShelfDate, OffShelfDate, ObsoleteReason);
}
#endregion
}
}
