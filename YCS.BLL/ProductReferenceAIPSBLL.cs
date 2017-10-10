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
    /// 创建人:
    /// 日期:2017/4/5 11:48:46 +08:00
    /// </summary>

    public class ProductReferenceAIPSBLL : Base.ProductReferenceAIPS
    {

        private readonly ProductReferenceAIPSDAL proDAL = new ProductReferenceAIPSDAL();

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
            string FieldShow = "a.*";
            string FieldOrder = "a.SN asc";
            return proDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
        }
        #endregion

        #region 取实体集合
        /// <summary>
        /// 取实体集合
        /// </summary>
        public List<ProductReferenceAIPSModel> GetModels(SqlTransaction trans)
        {
            StringBuilder SqlQuery = new StringBuilder();
            List<SqlParameter> listParams = new List<SqlParameter>();
            string FieldOrder = "SN asc";
            return proDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
        }
        #endregion

        #region 取实体
        /// <summary>
        /// 取实体
        /// </summary>
        public ProductReferenceAIPSModel GetModel(SqlTransaction trans, long SN)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and SN=@SN");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@SN", SN));
            return proDAL.GetModel(trans, SqlQuery, listParams);
        }

        /// <summary>
        /// 取实体
        /// </summary>
        public ProductReferenceAIPSModel GetModelByProductSkuId(SqlTransaction trans, string ProductSkuId)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and ProductSkuId=@ProductSkuId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@ProductSkuId", ProductSkuId));
            return proDAL.GetModel(trans, SqlQuery, listParams);
        }

        /// <summary>
        /// 取实体
        /// </summary>
        public DataTable GetDataTable(SqlTransaction trans, string ProductSkuId)
        {
            return proDAL.GetDataTable(trans, ProductSkuId);
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
            return proDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams);
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
            return proDAL.GetAllSum(trans, LeftJoin, SqlQuery, listParams, "a.SN");
        }
        #endregion

        #region 检查ProductID和ProductSkuId的关系
        /// <summary>
        /// 检查ProductID
        /// </summary>
        public bool CheckReferenceIsExistsByProductSpuId(SqlTransaction trans, string ProductID, string ProductSpuId)
        {
            StringBuilder LeftJoin = new StringBuilder();
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and ProductID=@ProductID");
            SqlQuery.Append(" and not exists(select ProductSkuId from ProductSkuReference where ProductSpuId=@ProductSpuId and ProductSkuId=a.ProductSkuId)");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@ProductID", ProductID));
            listParams.Add(new SqlParameter("@ProductSpuId", ProductSpuId));
            return proDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams) > 0;
        }
        /// <summary>
        /// 检查ProductID和ProductSkuId的关系
        /// </summary>
        public bool CheckReferenceIsExists(SqlTransaction trans, string ProductID, string ProductSkuId)
        {
            StringBuilder LeftJoin = new StringBuilder();
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and ProductID=@ProductID");
            SqlQuery.Append(" and ProductSkuId=@ProductSkuId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@ProductID", ProductID));
            listParams.Add(new SqlParameter("@ProductSkuId", ProductSkuId));
            return proDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams) > 0;
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public int DeleteByProductSpuId(SqlTransaction trans, string ProductSpuId)
        {
            return proDAL.DeleteByProductSpuId(trans, ProductSpuId);
        }
        #endregion

    }
}
