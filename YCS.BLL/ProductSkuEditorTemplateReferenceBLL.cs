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
    /// 产品SKU与版型的对应表 -业务逻辑类
    /// 创建人:
    /// 日期:2017/4/7 10:01:00 +08:00
    /// </summary>

    public class ProductSkuEditorTemplateReferenceBLL : Base.ProductSkuEditorTemplateReference
    {

        private readonly ProductSkuEditorTemplateReferenceDAL proDAL = new ProductSkuEditorTemplateReferenceDAL();

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
        public List<ProductSkuEditorTemplateReferenceModel> GetModels(SqlTransaction trans)
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
        public ProductSkuEditorTemplateReferenceModel GetModel(SqlTransaction trans, long SN)
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

        #region 检查ProductEditorTemplateId和ProductSkuId的关系
        /// <summary>
        /// 检查ProductEditorTemplateId和ProductSkuId的关系
        /// </summary>
        public bool CheckReferenceIsExists(SqlTransaction trans, string ProductEditorTemplateId, string ProductSkuId)
        {
            StringBuilder LeftJoin = new StringBuilder();
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and ProductEditorTemplateId=@ProductEditorTemplateId");
            SqlQuery.Append(" and ProductSkuId=@ProductSkuId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@ProductEditorTemplateId", ProductEditorTemplateId));
            listParams.Add(new SqlParameter("@ProductSkuId", ProductSkuId));
            return proDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams) > 0;
        }
        #endregion

        #region 取ProductEditorTemplateId
        /// <summary>
        /// 取ProductEditorTemplateId
        /// </summary>
        public string GetProductEditorTemplateIds(SqlTransaction trans, string ProductSkuId)
        {
            StringBuilder LeftJoin = new StringBuilder();
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and ProductSkuId=@ProductSkuId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@ProductSkuId", ProductSkuId));
            string FieldShow = "a.ProductEditorTemplateId";
            string FieldOrder = "a.SN asc";
            DataTable dt= proDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
            List<string> list = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(dr[0].ToString()+",");
            }
            return list.ListToStr<string>("");
        }
        #endregion

    }
}
