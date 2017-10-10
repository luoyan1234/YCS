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
    /// 訂單紀錄表-业务逻辑类
    /// 创建人:楊小明
    /// 日期:2017/2/18 15:06:08
    /// </summary>

    public class OrderBLL : Base.Order
    {

        private readonly OrderDAL ordDAL = new OrderDAL();
        private readonly OrderItemDAL ordItemDAL = new OrderItemDAL();

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
        #endregion

        #region 取实体集合
        /// <summary>
        /// 取实体集合
        /// </summary>
        public List<OrderModel> GetModels(SqlTransaction trans)
        {
            StringBuilder SqlQuery = new StringBuilder();
            List<SqlParameter> listParams = new List<SqlParameter>();
            string FieldOrder = "OrderId asc";
            return ordDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
        }
        /// <summary>
        /// 取实体集合
        /// </summary>
        public List<OrderModel> GetModels(SqlTransaction trans, long MemberId, int topNum, int Status, int Type)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and MemberId = @MemberId");
            SqlQuery.Append(" and Status = @Status");
            SqlQuery.Append(" and Type = @Type");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@MemberId", MemberId));
            listParams.Add(new SqlParameter("@Status", Status));
            listParams.Add(new SqlParameter("@Type", Type));
            string FieldOrder = "CreationDate desc";
            return ordDAL.GetModels(trans, SqlQuery, listParams, topNum, FieldOrder);
        }
        #endregion

        #region 取实体
        /// <summary>
        /// 取实体
        /// </summary>
        public OrderModel GetModel(SqlTransaction trans, long OrderId)
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
        public OrderModel GetModelByPayNo(SqlTransaction trans, string PayNo)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and OrderNo=@PayNo");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@PayNo", PayNo));
            return ordDAL.GetModel(trans, SqlQuery, listParams);
        }
        /// <summary>
        /// 取实体
        /// </summary>
        public OrderModel GetModelByOrderNo(SqlTransaction trans, string OrderNo)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and OrderNo=@OrderNo");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@OrderNo", OrderNo));
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
        public int GetAllCount(SqlTransaction trans, long MemberId, string DistributorId, int ProcessType)
        {
            StringBuilder LeftJoin = new StringBuilder();
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and MemberId = @MemberId");
            SqlQuery.Append(" and DistributorId = @DistributorId");
            SqlQuery.Append(" and Type = @Type");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@MemberId", MemberId));
            listParams.Add(new SqlParameter("@DistributorId", DistributorId));
            listParams.Add(new SqlParameter("@Type", ProcessType));
            return ordDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams);
        }
        /// <summary>
        /// 取记录总数
        /// </summary>
        public int GetAllCount(SqlTransaction trans, long MemberId, string DistributorId, int ProcessType,int Status)
        {
            StringBuilder LeftJoin = new StringBuilder();
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and MemberId = @MemberId");
            SqlQuery.Append(" and DistributorId = @DistributorId");
            SqlQuery.Append(" and Type = @Type");
            SqlQuery.Append(" and Status = @Status");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@MemberId", MemberId));
            listParams.Add(new SqlParameter("@DistributorId", DistributorId));
            listParams.Add(new SqlParameter("@Type", ProcessType));
            listParams.Add(new SqlParameter("@Status", Status));
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

        #region 取每个订单状态的数据总和
        /// <summary>
        /// 取每个订单状态的数据总和
        /// </summary>
        public decimal GetAllCountByStatus(SqlTransaction trans, int Status)
        {
            StringBuilder LeftJoin = new StringBuilder();
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and Status = @Status");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@Status", Status));
            return ordDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams);
        }
        #endregion

        #region 更改状态
        /// <summary>
        /// 更改状态
        /// </summary>
        public int UpdateOrderStatus(SqlTransaction trans, OrderModel ordModel, long OrderId, short Status)
        {
            string key = "Cache_Order_Model_" + OrderId;
            CacheHelper.RemoveCache(key);
            ordDAL.UpdateInfo(trans, ordModel, OrderId);
            //更改订单明细状态
            return ordItemDAL.UpdateOrderStatus(trans, ordModel.OrderId, Status);
        }
        #endregion

        #region 取超过有效时间未支付的订单
        /// <summary>
        /// 取超过有效时间未支付的订单
        /// </summary>
        /// <param name="trans"></param>
        /// <returns></returns>
        public List<OrderModel> GetModelsForNoPay(SqlTransaction trans, long UserID, long OrderID)
        {
            StringBuilder SqlQuery = new StringBuilder();
            List<SqlParameter> listParams = new List<SqlParameter>();
            if (UserID > 0)
            {
                SqlQuery.Append(" and MemberId=@UserID");
                listParams.Add(new SqlParameter("@UserID", UserID));
            }
            if (OrderID > 0)
            {
                SqlQuery.Append(" and OrderId=@OrderID");
                listParams.Add(new SqlParameter("@OrderID", OrderID));
            }

            SqlQuery.Append(" and Status=@OrderStatus");
            listParams.Add(new SqlParameter("@OrderStatus", EnumList.OrderStatus.待付款.ToInt()));

            SqlQuery.Append(" and DATEDIFF(ss,CreationDate,@Now)>@ValidTime");
            listParams.Add(new SqlParameter("@Now", DateTime.Now));
            listParams.Add(new SqlParameter("@ValidTime", Config.OrderNoPayTime * 60 * 60));
            string FieldOrder = "OrderId asc";
            return ordDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
        }
        #endregion

        #region 更新超过有效时间未支付的订单状态
        /// <summary>
        /// 更新超过有效时间未支付的订单状态
        /// </summary>
        public bool UpdateNoPayStatus(long UserID, long OrderID)
        {
            //执行事务
            List<object> listOut;
            if (DeleHelpler.RunTrans(SubmitCancelTrans, new List<object>() { UserID, OrderID }, out listOut))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 更新超过有效时间未支付的订单状态
        /// </summary>
        public bool UpdateNoPayStatus(long UserID, long OrderID, out int count)
        {
            //执行事务
            List<object> listOut;
            count = 0;
            if (DeleHelpler.RunTrans(SubmitCancelTrans, new List<object>() { UserID, OrderID }, out listOut))
            {
                count = (int)listOut[0];
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 提交取消事务
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="listInput"></param>
        private void SubmitCancelTrans(SqlTransaction trans, List<object> listInput, out List<object> listOut)
        {
            //OrderProductBLL ordProBLL = new OrderProductBLL();
            //ProductBLL proBLL = new ProductBLL();

            //获取参数
            long UserID = (long)listInput[0];
            long OrderID = (long)listInput[1];

            List<OrderModel> list = GetModelsForNoPay(trans, UserID, OrderID);

            foreach (var ordModel in list)
            {
                //取消订单
                ordModel.Status = (short)EnumList.OrderStatus.已取消.ToInt();
                ordModel.CancelReason = "超过" + Config.OrderNoPayTime + "小时未支付，订单自动取消";
                ordModel.CancelAdminId=1;
                ordModel.CancelMemberId=0;
                ordModel.CancellationDate = DateTime.Now;
                ordModel.LastUpdateDate = DateTime.Now;
                ordDAL.UpdateInfo(trans, ordModel, ordModel.OrderId);

                //修改订单产品明细
                List<OrderItemModel> listItem = Factory.OrderItem().GetModels(trans, ordModel.OrderId);
                foreach (OrderItemModel orm in listItem) {
                    orm.Status = (short)EnumList.OrderStatus.已取消.ToInt();
                    orm.LastUpdateDate = DateTime.Now;
                    Factory.OrderItem().UpdateInfo(trans,orm,orm.SN);
                }

                //订单商品(todo)
                //foreach (OrderProductModel ordProModel in ordProBLL.GetModels(trans, ordModel.OrderId))
                //{
                //    //更新库存和销量
                //    ProductModel proModel = proBLL.GetInfo(trans, ordProModel.ProductID);
                //    if (proModel != null)
                //    {
                //        proModel.StockCount += ordProModel.Quantity;
                //        proModel.SaleCount -= ordProModel.Quantity;
                //        proBLL.UpdateInfo(trans, proModel, proModel.ProductID);
                //    }
                //}
            }
            listOut = new List<object> { list.Count };
        }
        #endregion


        #region 超过收货时间未收货
        /// <summary>
        /// 超过收货时间未收货
        /// </summary>
        /// <param name="trans"></param>
        /// <returns></returns>
        public List<OrderModel> GetModelsForNoDeliver(SqlTransaction trans, long UserID, long OrderID)
        {
            StringBuilder SqlQuery = new StringBuilder();
            List<SqlParameter> listParams = new List<SqlParameter>();
            if (UserID > 0)
            {
                SqlQuery.Append(" and MemberId=@UserID");
                listParams.Add(new SqlParameter("@UserID", UserID));
            }
            if (OrderID > 0)
            {
                SqlQuery.Append(" and OrderId=@OrderID");
                listParams.Add(new SqlParameter("@OrderID", OrderID));
            }

            SqlQuery.Append(" and Status=@OrderStatus");
            listParams.Add(new SqlParameter("@OrderStatus", EnumList.OrderStatus.待收货.ToInt()));

            SqlQuery.Append(" and DATEDIFF(ss,ShipDate,@Now)>@ValidTime");
            listParams.Add(new SqlParameter("@Now", DateTime.Now));
            listParams.Add(new SqlParameter("@ValidTime", Config.OrderMotionDay * 24 * 60 * 60));
            string FieldOrder = "OrderId asc";
            return ordDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
        }
        #endregion

        #region 超过收货时间未收货
        /// <summary>
        /// 超过收货时间未收货
        /// </summary>
        public bool UpdateNoDeliverStatus(long UserID, long OrderID)
        {
            //执行事务
            List<object> listOut;
            if (DeleHelpler.RunTrans(SubmitDeliverTrans, new List<object>() { UserID, OrderID }, out listOut))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 超过收货时间未收货
        /// </summary>
        public bool UpdateNoDeliverStatus(long UserID, long OrderID, out int count)
        {
            //执行事务
            List<object> listOut;
            count = 0;
            if (DeleHelpler.RunTrans(SubmitDeliverTrans, new List<object>() { UserID, OrderID }, out listOut))
            {
                count = (int)listOut[0];
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 提交取消事务
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="listInput"></param>
        private void SubmitDeliverTrans(SqlTransaction trans, List<object> listInput, out List<object> listOut)
        {
            //OrderProductBLL ordProBLL = new OrderProductBLL();
            //ProductBLL proBLL = new ProductBLL();

            //获取参数
            long UserID = (long)listInput[0];
            long OrderID = (long)listInput[1];

            List<OrderModel> list = GetModelsForNoDeliver(trans, UserID, OrderID);

            foreach (var ordModel in list)
            {
                //订单确认收货
                ordModel.Status = (short)EnumList.OrderStatus.已完成.ToInt();
                ordModel.LastUpdateDate = DateTime.Now;
                //ordModel.ArrivalDate = DateTime.Now;
                ordDAL.UpdateInfo(trans, ordModel, ordModel.OrderId);


                //修改订单产品明细
                List<OrderItemModel> listItem = Factory.OrderItem().GetModels(trans, ordModel.OrderId);
                foreach (OrderItemModel orm in listItem)
                {
                    orm.Status = (short)EnumList.OrderStatus.已完成.ToInt();
                    orm.LastUpdateDate = DateTime.Now;
                    Factory.OrderItem().UpdateInfo(trans, orm, orm.SN);
                }

                //订单商品(todo)
                //foreach (OrderProductModel ordProModel in ordProBLL.GetModels(trans, ordModel.OrderId))
                //{
                //    //更新库存和销量
                //    ProductModel proModel = proBLL.GetInfo(trans, ordProModel.ProductID);
                //    if (proModel != null)
                //    {
                //        proModel.StockCount += ordProModel.Quantity;
                //        proModel.SaleCount -= ordProModel.Quantity;
                //        proBLL.UpdateInfo(trans, proModel, proModel.ProductID);
                //    }
                //}
            }
            listOut = new List<object> { list.Count };
        }
        #endregion

        #region 是否支付
        /// <summary>
        /// 是否支付
        /// </summary>
        public bool IsPay(SqlTransaction trans, long OrderID, long UserID)
        {
            List<int> status = new List<int> { EnumList.OrderStatus.待发货.ToInt(),EnumList.OrderStatus.待上传设计文件.ToInt() };
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and OrderId=@OrderID");
            SqlQuery.Append(" and MemberId=@UserID");
            SqlQuery.Append(" and Status in(" + status.ListToStr() + ")");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@OrderID", OrderID));
            listParams.Add(new SqlParameter("@UserID", UserID));
            return ordDAL.GetModel(trans, SqlQuery, listParams) != null;
        }
        #endregion

        #region 更新主工單編號
        /// <summary>
        /// 更新主工單編號
        /// </summary>
        public int UpdateMainWorkId(SqlTransaction trans, long OrderId, string mainWorkId)
        {
            return ordDAL.UpdateMainWorkId(trans,OrderId,mainWorkId);
        }
        #endregion
    }
}
