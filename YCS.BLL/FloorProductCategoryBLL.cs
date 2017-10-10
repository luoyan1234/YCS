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
/// 楼层商品分类-业务逻辑类
/// 创建人:luoy
/// 日期:2017/3/15 13:40:48 +08:00
/// </summary>

public class  FloorProductCategoryBLL:Base.FloorProductCategory
{

private readonly FloorProductCategoryDAL floDAL=new FloorProductCategoryDAL();

#region 取信息分页列表
/// <summary>
/// 取信息分页列表
/// </summary>
public DataTable GetInfoPageList(SqlTransaction trans, Hashtable hs, PageHelper p, out StringBuilder PageStr)
{
return floDAL.GetInfoPageList(trans, hs, p, out PageStr);
}
#endregion

#region 取DataTable
/// <summary>
/// 取DataTable
/// </summary>
public DataTable GetDataTable(SqlTransaction trans, Hashtable hs)
{
    StringBuilder LeftJoin = new StringBuilder();
    LeftJoin.Append(" left join Floor as b on a.FloorId=b.FloorId ");
    LeftJoin.Append(" left join ProductCategorySetting as c on a.ProductCategoryId= c.ProductCategoryId ");
    LeftJoin.Append(" left join Admin as d on a.AdminId=d.AdminId ");
    StringBuilder SqlQuery = new StringBuilder();
    List<SqlParameter> listParams = new List<SqlParameter>();
    #region 查询条件
    if (hs.Contains("FloorId"))
    {
        SqlQuery.Append(" and a.FloorId = @FloorId");
        listParams.Add(new SqlParameter("@FloorId", hs["FloorId"]));
    }
    if (hs.Contains("ProductCategoryId"))
    {
        SqlQuery.Append(" and a.ProductCategoryId like @ProductCategoryId");
        listParams.Add(new SqlParameter("@ProductCategoryId", "%" + hs["ProductCategoryId"] + "%"));
    }
    if (hs.Contains("DistributorId"))
    {
        SqlQuery.Append(" and a.DistributorId like @DistributorId");
        listParams.Add(new SqlParameter("@DistributorId", "%" + hs["DistributorId"] + "%"));
    }
    if (hs.Contains("SeqNo"))
    {
        SqlQuery.Append(" and a.SeqNo = @SeqNo");
        listParams.Add(new SqlParameter("@SeqNo", hs["SeqNo"]));
    }
    if (hs.Contains("IsClose"))
    {
        SqlQuery.Append(" and a.IsClose = @IsClose");
        listParams.Add(new SqlParameter("@IsClose", hs["IsClose"]));
    }
    if (hs.Contains("AdminId"))
    {
        SqlQuery.Append(" and a.AdminId = @AdminId");
        listParams.Add(new SqlParameter("@AdminId", hs["AdminId"]));
    }
    if (hs.Contains("CreationDate"))
    {
        SqlQuery.Append(" and a.CreationDate = @CreationDate");
        listParams.Add(new SqlParameter("@CreationDate", hs["CreationDate"]));
    }
    if (hs.Contains("LastUpdateDate"))
    {
        SqlQuery.Append(" and a.LastUpdateDate = @LastUpdateDate");
        listParams.Add(new SqlParameter("@LastUpdateDate", hs["LastUpdateDate"]));
    }
    #endregion
    string FieldShow = "a.*,b.FloorName,c.ProductCategoryName,d.AdminName";
    string FieldOrder = "a.FloorProductCategoryId asc";
    return floDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
}
#endregion

#region 取实体集合
/// <summary>
/// 取实体集合
/// </summary>
public List<FloorProductCategoryModel> GetModels(SqlTransaction trans)
{
StringBuilder SqlQuery = new StringBuilder();
List<SqlParameter> listParams = new List<SqlParameter>();
string FieldOrder="FloorProductCategoryId asc";
return floDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
}
/// <summary>
/// 取实体集合
/// </summary>
public List<FloorProductCategoryModel> GetModels(SqlTransaction trans, int floorId, bool isClose, int topNum=0)
{
    string FiledShow = "a.*,b.ProductCategoryName";
    string LeftJoin = " a right join ProductCategorySetting b on a.ProductCategoryId=b.ProductCategoryId ";
    StringBuilder SqlQuery = new StringBuilder();
    SqlQuery.Append(" and FloorId=@FloorId");
    SqlQuery.Append(" and IsClose=@IsClose");
    List<SqlParameter> listParams = new List<SqlParameter>();
    listParams.Add(new SqlParameter("@FloorId", floorId));
    listParams.Add(new SqlParameter("@IsClose", isClose));
    string FieldOrder = "FloorProductCategoryId asc";
    return floDAL.GetModels(trans, SqlQuery, listParams, topNum, FieldOrder, FiledShow, LeftJoin);
}
public List<FloorProductCategoryModel> GetModelsForForeign(SqlTransaction trans, int floorId, bool isClose, int topNum = 0)
{
    string FiledShow = "a.*,b.ProductCategoryName";
    string LeftJoin = " a right join ProductCategorySetting b on a.ProductCategoryId=b.ProductCategoryId ";
    StringBuilder SqlQuery = new StringBuilder();
    SqlQuery.Append(" and FloorId=@FloorId");
    SqlQuery.Append(" and IsClose=@IsClose");
    List<SqlParameter> listParams = new List<SqlParameter>();
    listParams.Add(new SqlParameter("@FloorId", floorId));
    listParams.Add(new SqlParameter("@IsClose", isClose));
    string FieldOrder = "FloorProductCategoryId asc";
    return floDAL.GetModelsForForeign(trans, SqlQuery, listParams, topNum, FieldOrder, FiledShow, LeftJoin);
}
#endregion

#region 取实体
/// <summary>
/// 取实体
/// </summary>
public FloorProductCategoryModel GetModel(SqlTransaction trans, int FloorProductCategoryId)
{
StringBuilder SqlQuery = new StringBuilder();
SqlQuery.Append(" and FloorProductCategoryId=@FloorProductCategoryId");
List<SqlParameter> listParams = new List<SqlParameter>();
listParams.Add(new SqlParameter("@FloorProductCategoryId", FloorProductCategoryId));
return floDAL.GetModel(trans, SqlQuery, listParams);
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
return floDAL.GetAllCount(trans,LeftJoin, SqlQuery, listParams);
}
public int GetAllCount(SqlTransaction trans, int floorId, bool isClose)
{
    StringBuilder LeftJoin = new StringBuilder();
    StringBuilder SqlQuery = new StringBuilder();
    SqlQuery.Append(" and FloorId=@FloorId");
    SqlQuery.Append(" and IsClose=@IsClose");
    List<SqlParameter> listParams = new List<SqlParameter>();
    listParams.Add(new SqlParameter("@FloorId", floorId));
    listParams.Add(new SqlParameter("@IsClose", isClose));
    return floDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams);
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
return floDAL.GetAllSum(trans,LeftJoin, SqlQuery, listParams,"a.FloorProductCategoryId");
}
#endregion
#region 读取排序号
/// <summary>
/// 读取排序号
/// </summary>
public int GetSeqNo(SqlTransaction trans, int FloorId)
{
    return floDAL.GetSeqNo(trans, FloorId);
}
#endregion

#region 排序信息
/// <summary>
/// 排序信息
/// </summary>
/// <returns></returns>
public void OrderInfo(SqlTransaction trans, int FloorId, int intSeqNo, int intOldSeqNo)
{
    floDAL.OrderInfo(trans, FloorId, intSeqNo, intOldSeqNo);
}
#endregion
#region 显示下拉树形列表
/// <summary>
/// 显示下拉树形列表
/// </summary>
public List<SelectListItem> GetSelectTreeList(SqlTransaction trans, int FloorId,bool isClose)
{
    List<SelectListItem> list = new List<SelectListItem>();
    return floDAL.GetSelectTreeList(trans, FloorId, isClose,list);
}
#endregion
}
}
