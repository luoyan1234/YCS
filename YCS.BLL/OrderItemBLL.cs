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
    /// 訂單明細表-业务逻辑类
    /// 创建人:楊小明
    /// 日期:2017/2/18 15:06:09
    /// </summary>

    public class OrderItemBLL : Base.OrderItem
    {

        private readonly OrderItemDAL ordDAL = new OrderItemDAL();

        #region 取信息分页列表
        /// <summary>
        /// 取信息分页列表
        /// </summary>
        public DataTable GetInfoPageList(SqlTransaction trans, Hashtable hs, PageHelper p, out StringBuilder PageStr)
        {
            return ordDAL.GetInfoPageList(trans, hs, p, out PageStr);
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
            string FieldShow = "a.*";
            string FieldOrder = "a.OrderId asc";
            return ordDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
        }
        /// <summary>
        /// 取DataTable
        /// </summary>
        public DataTable GetDataTable(SqlTransaction trans, long OrderId)
        {
            StringBuilder LeftJoin = new StringBuilder();
            LeftJoin.Append(" left join ProductSku b on a.ProductSkuId=b.ProductSkuId");
            LeftJoin.Append(" left join ProductSkuReference c on b.ProductSkuId=c.ProductSkuId");
            LeftJoin.Append(" left join ProductSpu d on c.ProductSpuId=d.ProductSpuId");
            LeftJoin.Append(" left join [Order] e on e.OrderId=a.OrderId");
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and a.OrderId=@OrderId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@OrderId", OrderId));
            string FieldShow = " distinct a.*,b.ProductSkuName,b.Price,d.Status spuStatus,d.OnShelfDate,d.OffShelfDate,d.Type,e.OrderNo";
            string FieldOrder = "a.SN desc";
            return ordDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
        }
        /// <summary>
        /// 取DataTable
        /// </summary>
        public DataTable GetDataTable(SqlTransaction trans, long OrderId, int type)
        {
            StringBuilder LeftJoin = new StringBuilder();
            LeftJoin.Append(" left join [Order] o on o.OrderId=a.OrderId");
            LeftJoin.Append(" left join ProductSku b on a.ProductSkuId=b.ProductSkuId");
            LeftJoin.Append(" left join ProductSkuReference c on b.ProductSkuId=c.ProductSkuId");
            LeftJoin.Append(" left join ProductSpu d on c.ProductSpuId=d.ProductSpuId");
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and a.OrderId=@OrderId");
            SqlQuery.Append(" and o.Type=@Type");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@OrderId", OrderId));
            listParams.Add(new SqlParameter("@Type", type));
            string FieldShow = " distinct a.*,b.ProductSkuName,b.Price,d.Status spuStatus,d.OnShelfDate,d.OffShelfDate,o.Type";
            string FieldOrder = "a.SN asc";
            return ordDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
        }
        #endregion

        #region 取实体集合
        /// <summary>
        /// 取实体集合
        /// </summary>
        public List<OrderItemModel> GetModels(SqlTransaction trans)
        {
            StringBuilder SqlQuery = new StringBuilder();
            List<SqlParameter> listParams = new List<SqlParameter>();
            string FieldOrder = "OrderId asc";
            return ordDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
        }
       
        #endregion

        #region 取实体
        /// <summary>
        /// 取实体
        /// </summary>
        public OrderItemModel GetModel(SqlTransaction trans, string OrderId)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and OrderId=@OrderId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@OrderId", OrderId));
            return ordDAL.GetModel(trans, SqlQuery, listParams);
        }
        /// <summary>
        /// 取实体
        /// </summary>
        public OrderItemModel GetModelBySN(SqlTransaction trans, long SN)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and SN=@SN");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@SN", SN));
            return ordDAL.GetModel(trans, SqlQuery, listParams);
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
            return ordDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams);
        }

        /// <summary>
        /// 取记录总数
        /// </summary>
        public int GetAllCountByOrderItem(SqlTransaction trans, long OrderId)
        {
            StringBuilder LeftJoin = new StringBuilder();
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and OrderId = @OrderId");
            SqlQuery.Append(" and Status = @Status");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@OrderId", OrderId));
            listParams.Add(new SqlParameter("@Status", EnumList.OrderStatus.待上传设计文件.ToInt()));
            return ordDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams);
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
            return ordDAL.GetAllSum(trans, LeftJoin, SqlQuery, listParams, "a.OrderId");
        }
        #endregion

        #region 取实体集合
        /// <summary>
        /// 取实体集合
        /// </summary>
        public List<OrderItemModel> GetModels(SqlTransaction trans, long OrderID)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and OrderId=@OrderID");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@OrderID", OrderID));
            string FieldOrder = " SN asc";
            return ordDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
        }
        #endregion

        #region 获取订单明细列表
        /// <summary>
        /// 获取订单明细列表
        /// </summary>
        public List<OrderItemModel> GetOrderItemList(SqlTransaction trans, int pdfStatus)
        {
            return ordDAL.GetOrderItemList(trans, pdfStatus);
            
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public int UpdateInfoNoCache(SqlTransaction trans, OrderItemModel ordItemModel, long SN)
        {
            return ordDAL.UpdateInfo(trans, ordItemModel, SN);
        }
        #endregion


    }
}
