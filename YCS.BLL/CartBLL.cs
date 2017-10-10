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
    /// 购物车-业务逻辑类
    /// 创建人:楊小明
    /// 日期:2017/2/18 15:06:07
    /// </summary>

    public class CartBLL : Base.Cart
    {

        private readonly CartDAL carDAL = new CartDAL();

        #region 取信息分页列表
        /// <summary>
        /// 取信息分页列表
        /// </summary>
        public DataTable GetInfoPageList(SqlTransaction trans, Hashtable hs, PageHelper p, out StringBuilder PageStr)
        {
            return carDAL.GetInfoPageList(trans, hs, p, out PageStr);
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
            string FieldOrder = "a.CartId asc";
            return carDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
        }
        #endregion
        
        #region 取实体集合
        /// <summary>
        /// 取实体集合
        /// </summary>
        public List<CartModel> GetModels(SqlTransaction trans)
        {
            StringBuilder SqlQuery = new StringBuilder();
            List<SqlParameter> listParams = new List<SqlParameter>();
            string FieldOrder = "CartId asc";
            return carDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
        }
        #endregion

        #region 取实体
        /// <summary>
        /// 取实体
        /// </summary>
        public CartModel GetModel(SqlTransaction trans, int CartId)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and CartId=@CartId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@CartId", CartId));
            return carDAL.GetModel(trans, SqlQuery, listParams);
        }
        public CartModel GetModel(SqlTransaction trans, long MemberId, string ProductSkuId)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and MemberId=@MemberId");
            SqlQuery.Append(" and ProductSkuId=@ProductSkuId");
            SqlQuery.Append(" and IsInvalid=0");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@MemberId", MemberId));
            listParams.Add(new SqlParameter("@ProductSkuId", ProductSkuId));
            return carDAL.GetModel(trans, SqlQuery, listParams);
        }
        public CartModel GetModelByPortfolioId(SqlTransaction trans, long MemberId, string PortfolioId)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and MemberId=@MemberId");
            SqlQuery.Append(" and PortfolioId=@PortfolioId");
            SqlQuery.Append(" and IsInvalid=0");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@MemberId", MemberId));
            listParams.Add(new SqlParameter("@PortfolioId", PortfolioId));
            return carDAL.GetModel(trans, SqlQuery, listParams);
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
            return carDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams);
        }

        /// <summary>
        /// 取记录总数
        /// </summary>
        public int GetAllCount(SqlTransaction trans,long MemberId)
        {
            StringBuilder LeftJoin = new StringBuilder();
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and MemberId=@MemberId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@MemberId", MemberId));
            return carDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams);
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
            return carDAL.GetAllSum(trans, LeftJoin, SqlQuery, listParams, "a.CartId");
        }
        #endregion

        //---------------------

        #region 取购物车列表
        /// <summary>
        /// 取购物车列表
        /// </summary>
        public DataTable GetDataTable(SqlTransaction trans, string DistributorId, long MemberId, int ProductProcessType)
        {
            StringBuilder LeftJoin = new StringBuilder();
            LeftJoin.Append(" left join ProductSpu as b on b.ProductSpuId=a.ProductSpuId ");
            //LeftJoin.Append(" left join ProductSku as c on c.ProductSkuId=a.ProductSkuId ");
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and a.IsInvalid=@IsInvalid");
            SqlQuery.Append(" and a.DistributorId=@DistributorId");
            SqlQuery.Append(" and a.MemberId=@MemberId");
            SqlQuery.Append(" and a.ProductProcessType=@ProductProcessType");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@IsInvalid", EnumList.IsStatus.No.ToInt()));
            listParams.Add(new SqlParameter("@DistributorId", DistributorId));
            listParams.Add(new SqlParameter("@MemberId", MemberId));
            listParams.Add(new SqlParameter("@ProductProcessType", ProductProcessType));
            string FieldShow = "a.*,(a.Quantity*a.Price) as CartAmount,b.ProductSpuName,b.ProductSpuPic";
            string FieldOrder = "a.CartId desc";
            return carDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
        }

        /// <summary>
        /// 取DataTable(获取已选中下单的购物车)
        /// </summary>
        public DataTable GetDataTableForOrder(SqlTransaction trans, string DistributorId, long MemberId, int ProductProcessType)
        {
            StringBuilder LeftJoin = new StringBuilder();
            LeftJoin.Append(" left join ProductSpu as b on b.ProductSpuId=a.ProductSpuId ");
            //LeftJoin.Append(" left join ProductSku as c on c.ProductSkuId=a.ProductSkuId ");
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and a.IsInvalid=@IsInvalid");
            SqlQuery.Append(" and a.DistributorId=@DistributorId");
            SqlQuery.Append(" and a.MemberId=@MemberId");
            SqlQuery.Append(" and a.ProductProcessType=@ProductProcessType");
            SqlQuery.Append(" and a.IsSelectOrder=@IsSelectOrder");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@IsInvalid", EnumList.IsStatus.No.ToInt()));
            listParams.Add(new SqlParameter("@DistributorId", DistributorId));
            listParams.Add(new SqlParameter("@MemberId", MemberId));
            listParams.Add(new SqlParameter("@ProductProcessType", ProductProcessType));
            listParams.Add(new SqlParameter("@IsSelectOrder", EnumList.IsStatus.Yes.ToInt()));
            string FieldShow = "a.*,(a.Quantity*a.Price) as CartAmount,b.ProductSpuName,b.ProductSpuPic";
            string FieldOrder = "a.CartId desc";
            return carDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
        }
        #endregion

        #region 取购物车总数量
        /// <summary>
        /// 取购物车总数量(订购数量之和)
        /// </summary>
        public decimal GetCartCount(SqlTransaction trans, string DistributorId, long MemberId)
        {
            StringBuilder LeftJoin = new StringBuilder();
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and a.IsInvalid=@IsInvalid");
            SqlQuery.Append(" and a.DistributorId=@DistributorId");
            SqlQuery.Append(" and a.MemberId=@MemberId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@IsInvalid", EnumList.IsStatus.No.ToInt()));
            listParams.Add(new SqlParameter("@DistributorId", DistributorId));
            listParams.Add(new SqlParameter("@MemberId", MemberId));
            return carDAL.GetAllSum(trans, LeftJoin, SqlQuery, listParams, "a.Quantity");
        }
        /// <summary>
        /// 取购物车总数量(记录总数)
        /// </summary>
        public int GetCartCount(SqlTransaction trans, string DistributorId, long MemberId,int ProductProcessType)
        {
            StringBuilder LeftJoin = new StringBuilder();
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and a.IsInvalid=@IsInvalid");
            SqlQuery.Append(" and a.DistributorId=@DistributorId");
            SqlQuery.Append(" and a.MemberId=@MemberId");
            SqlQuery.Append(" and a.ProductProcessType=@ProductProcessType");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@IsInvalid", EnumList.IsStatus.No.ToInt()));
            listParams.Add(new SqlParameter("@DistributorId", DistributorId));
            listParams.Add(new SqlParameter("@MemberId", MemberId));
            listParams.Add(new SqlParameter("@ProductProcessType", ProductProcessType));
            return carDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams);
        }

        /// </summary>
        /// 取购物车总数量(选择下单)
        /// </summary>
        public int GetCartCountForOrder(SqlTransaction trans, string DistributorId, long MemberId, int ProductProcessType)
        {
            StringBuilder LeftJoin = new StringBuilder();
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and a.IsInvalid=@IsInvalid");
            SqlQuery.Append(" and a.DistributorId=@DistributorId");
            SqlQuery.Append(" and a.MemberId=@MemberId");
            SqlQuery.Append(" and a.ProductProcessType=@ProductProcessType");
            SqlQuery.Append(" and a.IsSelectOrder=@IsSelectOrder");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@IsInvalid", EnumList.IsStatus.No.ToInt()));
            listParams.Add(new SqlParameter("@DistributorId", DistributorId));
            listParams.Add(new SqlParameter("@MemberId", MemberId));
            listParams.Add(new SqlParameter("@ProductProcessType", ProductProcessType));
            listParams.Add(new SqlParameter("@IsSelectOrder", EnumList.IsStatus.Yes.ToInt()));
            return carDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams);
        }

        #endregion

        #region 取购物车总金额(选择下单)
        /// <summary>
        /// 已选购物车总金额(选择下单)
        /// </summary>
        public decimal GetCartAmount(SqlTransaction trans, string DistributorId, long MemberId, int ProductProcessType)
        {
            StringBuilder LeftJoin = new StringBuilder();
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and a.IsInvalid=@IsInvalid");
            SqlQuery.Append(" and a.DistributorId=@DistributorId");
            SqlQuery.Append(" and a.MemberId=@MemberId");
            SqlQuery.Append(" and a.ProductProcessType=@ProductProcessType");
            SqlQuery.Append(" and a.IsSelectOrder=@IsSelectOrder");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@IsInvalid", EnumList.IsStatus.No.ToInt()));
            listParams.Add(new SqlParameter("@DistributorId", DistributorId));
            listParams.Add(new SqlParameter("@MemberId", MemberId));
            listParams.Add(new SqlParameter("@ProductProcessType", ProductProcessType));
            listParams.Add(new SqlParameter("@IsSelectOrder", EnumList.IsStatus.Yes.ToInt()));
            return carDAL.GetAllSum(trans, LeftJoin, SqlQuery, listParams, "a.Price*a.Quantity");
        }
        #endregion

        #region 清除所选商品
        /// <summary>
        /// 清除所选商品
        /// </summary>
        public int DelSelect(SqlTransaction trans, string CartIds, string DistributorId, long MemberId)
        {
            return carDAL.DelSelect(trans, CartIds, DistributorId, MemberId);
        }
        #endregion

        #region 更新选择下单状态
        /// <summary>
        /// 更新选择下单状态
        /// </summary>
        public int UpdateSelectStatus(SqlTransaction trans, string CartIDs, long UserID, int IsSelectOrder)
        {
            return carDAL.UpdateSelectStatus(trans, CartIDs, UserID, IsSelectOrder);
        }

        #endregion

        #region 获取购物车相关信息
        /// <summary>
        /// 取已选下单总重量
        /// </summary>
        public decimal GetCartWeightForOrder(SqlTransaction trans, string DistributorId, long MemberId, int ProductProcessType)
        {
            StringBuilder LeftJoin = new StringBuilder();
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and a.IsInvalid=@IsInvalid");
            SqlQuery.Append(" and a.DistributorId=@DistributorId");
            SqlQuery.Append(" and a.MemberId=@MemberId");
            SqlQuery.Append(" and a.ProductProcessType=@ProductProcessType");
            SqlQuery.Append(" and a.IsSelectOrder=@IsSelectOrder");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@IsInvalid", EnumList.IsStatus.No.ToInt()));
            listParams.Add(new SqlParameter("@DistributorId", DistributorId));
            listParams.Add(new SqlParameter("@MemberId", MemberId));
            listParams.Add(new SqlParameter("@ProductProcessType", ProductProcessType));
            listParams.Add(new SqlParameter("@IsSelectOrder", EnumList.IsStatus.Yes.ToInt()));
            return carDAL.GetAllSum(trans, LeftJoin, SqlQuery, listParams, "a.Weight*a.Quantity");
        }

        /// <summary>
        /// 取实体集合(选择下单)
        /// </summary>
        public List<CartModel> GetModelsForOrder(SqlTransaction trans, string DistributorId, long MemberId, int ProductProcessType)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and IsInvalid=@IsInvalid");
            SqlQuery.Append(" and DistributorId=@DistributorId");
            SqlQuery.Append(" and MemberId=@MemberId");
            SqlQuery.Append(" and ProductProcessType=@ProductProcessType");
            SqlQuery.Append(" and IsSelectOrder=@IsSelectOrder");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@IsInvalid", EnumList.IsStatus.No.ToInt()));
            listParams.Add(new SqlParameter("@DistributorId", DistributorId));
            listParams.Add(new SqlParameter("@MemberId", MemberId));
            listParams.Add(new SqlParameter("@ProductProcessType", ProductProcessType));
            listParams.Add(new SqlParameter("@IsSelectOrder", EnumList.IsStatus.Yes.ToInt()));
            string FieldOrder = "CartID desc";
            return carDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
        }
        #endregion

        #region 下完单后清空购物车
        /// <summary>
        /// 下完单后清空购物车
        /// </summary>
        public int DelAllForOrder(SqlTransaction trans, long MemberId, string DistributorId)
        {
            return carDAL.DelAllForOrder(trans, MemberId, DistributorId);
        }
        #endregion

        #region 加入购物车
        public void CartAdd(string DistributorId, long SessionMemberId, string ProductSpuId, string ProductSkuId, int Quantity, string PortfolioId,out int code,out string msg)
        {
            ProductSpuModel spuModel = Factory.ProductSpu().GetModel(null, ProductSpuId);
            if (spuModel != null)
            {
                if (spuModel.Status == EnumList.ProductSpuStatus.上架.ToInt() && (spuModel.OnShelfDate != null && spuModel.OnShelfDate <= DateTimeOffset.Now) && (spuModel.OffShelfDate != null && spuModel.OffShelfDate >= DateTimeOffset.Now))
                {
                    ProductSkuModel skuModel = Factory.ProductSku().GetModel(null, ProductSkuId);
                    if (skuModel != null)
                    {
                        int result = 0;
                        //判断购物车中是否存在
                        CartModel cart = null;
                        if (spuModel.Type == EnumList.ProductProcessType.UploadProcess.ToInt())
                        {
                            cart=Factory.Cart().GetModel(null, SessionMemberId, ProductSkuId);
                        }
                        else if (spuModel.Type == EnumList.ProductProcessType.DiyProcess.ToInt() && !string.IsNullOrEmpty(PortfolioId))
                        {
                            cart = Factory.Cart().GetModelByPortfolioId(null, SessionMemberId, PortfolioId);
                        }
                        if (cart != null)
                        {
                            cart.Quantity = Math.Min(cart.Quantity + 1, 5000);
                            cart.LastUpdateDate = DateTimeOffset.Now;

                            result = Factory.Cart().UpdateInfo(null, cart, cart.CartId);
                        }
                        else
                        {
                            CartModel carModel = new CartModel()
                            {
                                CreationDate = DateTimeOffset.Now,
                                DistributorId = DistributorId,
                                IsInvalid = EnumList.IsStatus.No.ToInt(),
                                IsSelectOrder = EnumList.IsStatus.Yes.ToInt(),
                                LastUpdateDate = DateTimeOffset.Now,
                                MemberId = SessionMemberId,
                                Price = skuModel.Price,
                                ProductProcessType = spuModel.Type,
                                ProductSkuId = ProductSkuId,
                                ProductSpuId = ProductSpuId,
                                Quantity = Quantity,
                                Weight = 0,
                                PortfolioId = PortfolioId
                            };
                            result = Factory.Cart().InsertInfo(null, carModel);
                        }
                        if (result > 0)
                        {
                            code = 1;
                            msg = "加入成功!";
                        }
                        else
                        {
                            code = 0;
                            msg = "加入失败!";
                        }
                    }
                    else
                    {
                        code = 0;
                        msg = "加入失败!";
                    }
                }
                else
                {
                    code = 0;
                    msg = "商品已下架!";
                }
            }
            else
            {
                code = 0;
                msg = "商品信息不存在!";
            }
        }
        #endregion
    }
}
