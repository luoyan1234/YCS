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
/// 楼层个性化商品-业务逻辑类
/// 创建人:luoy
/// 日期:2017/3/15 13:41:02 +08:00
/// </summary>

public class  FloorPersonalProductBLL:Base.FloorPersonalProduct
{

private readonly FloorPersonalProductDAL floDAL=new FloorPersonalProductDAL();

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
    LeftJoin.Append(" left join Admin as b on a.AdminId=b.AdminId ");
    LeftJoin.Append(" left join ProductSpu as c on a.ProductSpuId=c.ProductSpuId ");
    StringBuilder SqlQuery = new StringBuilder();
    List<SqlParameter> listParams = new List<SqlParameter>();
    #region 查询条件
    if (hs.Contains("FloorProductCategoryId"))
    {
        SqlQuery.Append(" and a.FloorProductCategoryId = @FloorProductCategoryId");
        listParams.Add(new SqlParameter("@FloorProductCategoryId", hs["FloorProductCategoryId"]));
    }
    if (hs.Contains("ProductSpuId"))
    {
        SqlQuery.Append(" and a.ProductSpuId like @ProductSpuId");
        listParams.Add(new SqlParameter("@ProductSpuId", "%" + hs["ProductSpuId"] + "%"));
    }
    if (hs.Contains("ProductPicUrl"))
    {
        SqlQuery.Append(" and a.ProductPicUrl like @ProductPicUrl");
        listParams.Add(new SqlParameter("@ProductPicUrl", "%" + hs["ProductPicUrl"] + "%"));
    }
    if (hs.Contains("Title"))
    {
        SqlQuery.Append(" and a.Title like @Title");
        listParams.Add(new SqlParameter("@Title", "%" + hs["Title"] + "%"));
    }
    if (hs.Contains("SubTitle"))
    {
        SqlQuery.Append(" and a.SubTitle like @SubTitle");
        listParams.Add(new SqlParameter("@SubTitle", "%" + hs["SubTitle"] + "%"));
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
    if (hs.Contains("DistributorId"))
    {
        SqlQuery.Append(" and a.DistributorId like @DistributorId");
        listParams.Add(new SqlParameter("@DistributorId", "%" + hs["DistributorId"] + "%"));
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
    string FieldShow = "a.*,b.AdminName,c.ProductSpuName";
    string FieldOrder = "a.FloorPersonalProductId asc";
    return floDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
}
#endregion

#region 取实体集合
/// <summary>
/// 取实体集合
/// </summary>
public List<FloorPersonalProductModel> GetModels(SqlTransaction trans)
{
StringBuilder SqlQuery = new StringBuilder();
List<SqlParameter> listParams = new List<SqlParameter>();
string FieldOrder="FloorPersonalProductId asc";
return floDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
}
/// <summary>
/// 取实体集合
/// </summary>
public List<FloorPersonalProductModel> GetModels(SqlTransaction trans, int FloorProductCategoryId, bool IsClose, int topNum = 0)
{
    string LeftJoin = " a left join ProductSpu b on a.ProductSpuId=b.ProductSpuId";
    StringBuilder SqlQuery = new StringBuilder();
    SqlQuery.Append(" and FloorProductCategoryId = @FloorProductCategoryId ");
    SqlQuery.Append(" and IsClose = @IsClose ");
    SqlQuery.Append(" and b.Status=" + EnumList.ProductSpuStatus.上架.ToInt() + " and (OnShelfDate is not null and getDate() >= OnShelfDate) and (OffShelfDate is not null and getDate() <= OffShelfDate) ");
    List<SqlParameter> listParams = new List<SqlParameter>();
    listParams.Add(new SqlParameter("@FloorProductCategoryId", FloorProductCategoryId));
    listParams.Add(new SqlParameter("@IsClose", IsClose));
    string FieldOrder = "a.SeqNo asc";
    return floDAL.GetModels(trans, SqlQuery, listParams, topNum, FieldOrder, LeftJoin);
}
#endregion

#region 取实体
/// <summary>
/// 取实体
/// </summary>
public FloorPersonalProductModel GetModel(SqlTransaction trans, int FloorPersonalProductId)
{
StringBuilder SqlQuery = new StringBuilder();
SqlQuery.Append(" and FloorPersonalProductId=@FloorPersonalProductId");
List<SqlParameter> listParams = new List<SqlParameter>();
listParams.Add(new SqlParameter("@FloorPersonalProductId", FloorPersonalProductId));
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
return floDAL.GetAllSum(trans,LeftJoin, SqlQuery, listParams,"a.FloorPersonalProductId");
}
#endregion
#region 读取排序号
/// <summary>
/// 读取排序号
/// </summary>
public int GetSeqNo(SqlTransaction trans, int FloorProductCategoryId)
{
    return floDAL.GetSeqNo(trans, FloorProductCategoryId);
}
#endregion

#region 排序信息
/// <summary>
/// 排序信息
/// </summary>
/// <returns></returns>
public void OrderInfo(SqlTransaction trans, int FloorProductCategoryId, int intSeqNo, int intOldSeqNo)
{
    floDAL.OrderInfo(trans, FloorProductCategoryId, intSeqNo, intOldSeqNo);
}
#endregion
}
}
