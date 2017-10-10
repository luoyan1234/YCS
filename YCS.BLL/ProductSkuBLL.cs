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
    /// 创建人:楊小明
    /// 日期:2017/2/18 15:06:09
    /// </summary>

    public class ProductSkuBLL : Base.ProductSku
    {

        private readonly ProductSkuDAL proDAL = new ProductSkuDAL();

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
            string FieldOrder = "a.ProductSkuId asc";
            return proDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
        }
        #endregion

        #region 取实体集合
        /// <summary>
        /// 取实体集合
        /// </summary>
        public List<ProductSkuModel> GetModels(SqlTransaction trans)
        {
            StringBuilder SqlQuery = new StringBuilder();
            List<SqlParameter> listParams = new List<SqlParameter>();
            string FieldOrder = "ProductSkuId asc";
            return proDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
        }
        /// <summary>
        /// 取实体集合
        /// </summary>
        public List<ProductSkuModel> GetModels(SqlTransaction trans, string ProductSpuId)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and exists(select ProductSkuId from ProductSkuReference where ProductSkuId=ProductSku.ProductSkuId and ProductSpuId=@ProductSpuId)");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@ProductSpuId", ProductSpuId));
            string FieldOrder = "ProductSkuId asc";
            return proDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
        }
        #endregion

        #region 取实体
        /// <summary>
        /// 取实体
        /// </summary>
        public ProductSkuModel GetModel(SqlTransaction trans, string ProductSkuId)
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
        public ProductSkuModel GetModelByAttr(SqlTransaction trans, string strSql)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(strSql);
            List<SqlParameter> listParams = new List<SqlParameter>();
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
            return proDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams);
        }
        /// <summary>
        /// 取记录总数
        /// </summary>
        public int GetAllCount(SqlTransaction trans,string strSql)
        {
            StringBuilder LeftJoin = new StringBuilder();
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(strSql);
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
            return proDAL.GetAllSum(trans, LeftJoin, SqlQuery, listParams, "a.ProductSkuId");
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public int DeleteByProductSkuId(SqlTransaction trans, string ProductSkuId)
        {
            return proDAL.DeleteByProductSkuId(trans, ProductSkuId);
        }
        #endregion

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo2(SqlTransaction trans, string strFieldName, string strFieldValue, string ProductSkuId)
        {
            return proDAL.CheckInfo2(trans, strFieldName, strFieldValue, ProductSkuId);
        }
        #endregion

        #region 取编号
        /// <summary>
        /// 取编号
        /// </summary>
        public string GetNo(SqlTransaction trans)
        {
            StringBuilder strNo = new StringBuilder("SKU");
            strNo.Append(DateTime.Now.ToString("yyyyMMdd"));

            StringBuilder LeftJoin = new StringBuilder();
            StringBuilder SqlQuery = new StringBuilder();
            List<SqlParameter> listParams = new List<SqlParameter>();
            string FieldShow = "top 1 a.ProductSkuId";
            string FieldOrder = "a.SN desc";
            DataTable dt = proDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
            if (dt.Rows.Count > 0)
            {
                string strTemp = dt.Rows[0][0].ToString();
                int len = strTemp.Length;
                int baseNo;
                if (len < 15)
                {
                    baseNo = 1;
                }
                else
                {
                    string strBaseNo = strTemp.Substring(11, 4);
                    if (int.TryParse(strBaseNo, out baseNo))
                    {
                        baseNo += 1;
                    }
                    else
                    {
                        baseNo = 1;
                    }
                }
                strNo.Append(Config.ShowZero(baseNo, 4));
            }
            else
            {
                strNo.Append("0001");
            }
            return strNo.ToString();
        }
        #endregion

        #region 取ProductSkuIds
        /// <summary>
        /// 取ProductSkuIds
        /// </summary>
        public List<string> GetProductSkuIds(SqlTransaction trans, string ProductSpuId)
        {
            StringBuilder LeftJoin = new StringBuilder();
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and exists(select ProductSkuId from ProductSkuReference where ProductSkuId=a.ProductSkuId and ProductSpuId=@ProductSpuId)");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@ProductSpuId", ProductSpuId));
            string FieldShow = "a.ProductSkuId";
            string FieldOrder = "a.ProductSkuId asc";
            DataTable dt= proDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
            List<string> list = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(dr[0].ToString());
            }
            return list;
        }
        #endregion

        #region 取SPU下最小和最大价格
        /// <summary>
        /// 取SPU下最小和最大价格
        /// </summary>
        public void GetMinAndMaxPrice(SqlTransaction trans, string ProductSpuId,out decimal minPrice,out decimal maxPrice)
        {
            minPrice = 0;
            maxPrice = 0;
            StringBuilder LeftJoin = new StringBuilder();
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and exists(select ProductSkuId from ProductSkuReference where ProductSkuId=a.ProductSkuId and ProductSpuId=@ProductSpuId)");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@ProductSpuId", ProductSpuId));
            string FieldShow = "min(a.Price) as minPrice,max(a.Price) as maxPrice";
            string FieldOrder = "min(a.Price) asc";
            DataTable dt= proDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
            if (dt.Rows.Count > 0)
            {
                minPrice = dt.Rows[0]["minPrice"].ToDecimal();
                maxPrice = dt.Rows[0]["maxPrice"].ToDecimal();
            }
        }
        #endregion


    }
}
