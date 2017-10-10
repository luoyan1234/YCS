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
    /// 商品模板項目對應屬性-业务逻辑类
    /// 创建人:杨小明
    /// 日期:2017/3/13 10:18:06 +08:00
    /// </summary>

    public class ProductTemplateVirtualSkuIDcorrespondBLL : Base.ProductTemplateVirtualSkuIDcorrespond
    {

        private readonly ProductTemplateVirtualSkuIDcorrespondDAL proDAL = new ProductTemplateVirtualSkuIDcorrespondDAL();

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
        /// <summary>
        /// 取DataTable
        /// </summary>
        public DataTable GetDataTableForAttr(SqlTransaction trans, string VirtualSkuId)
        {
            StringBuilder LeftJoin = new StringBuilder();
            LeftJoin.Append(" left join ProductAttributeTypeLibrary as b on b.ProductAttributeTypeId=a.ProductAttributeTypeId");
            LeftJoin.Append(" left join ProductAttributeLibrary as c on c.ProductAttributeId=a.ProductAttributeId");
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and a.VirtualSkuId=@VirtualSkuId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@VirtualSkuId", VirtualSkuId));
            string FieldShow = "a.ProductAttributeId,a.PicUrl,b.SN as b_SN,c.ProductAttributeName";
            string FieldOrder = "a.SN asc";
            return proDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
        }
        #endregion

        #region 取实体集合
        /// <summary>
        /// 取实体集合
        /// </summary>
        public List<ProductTemplateVirtualSkuIDcorrespondModel> GetModels(SqlTransaction trans)
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
        public ProductTemplateVirtualSkuIDcorrespondModel GetModel(SqlTransaction trans, long SN)
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

        #region 取属性模板中的属性类型
        /// <summary>
        /// 取属性模板中的属性类型
        /// </summary>
        public string GetProductAttributeTypes(SqlTransaction trans, string ProductTemplateId)
        {
            StringBuilder LeftJoin = new StringBuilder();
            LeftJoin.Append(" left join ProductAttributeTypeLibrary as b on b.ProductAttributeTypeId=a.ProductAttributeTypeId");
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and a.VirtualSkuId =(select top 1 VirtualSkuId  from ProductTemplateVirtualSkuColumn where ProductTemplateId=@ProductTemplateId)");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@ProductTemplateId", ProductTemplateId));
            string FieldShow = "b.ProductAttributeTypeName";
            string FieldOrder = "a.SN asc";
            DataTable dt = proDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
            StringBuilder strReturn = new StringBuilder();
            foreach (DataRow dr in dt.Rows)
            {
                strReturn.Append(dr["ProductAttributeTypeName"].ToString() + "、");
            }
            return strReturn.ToString().TrimEnd('、');
        }
        #endregion
    }
}
