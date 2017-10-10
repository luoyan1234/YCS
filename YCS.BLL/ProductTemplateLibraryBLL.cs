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
    /// 商品模板表-业务逻辑类
    /// 创建人:杨小明
    /// 日期:2017/3/13 10:18:05 +08:00
    /// </summary>

    public class ProductTemplateLibraryBLL : Base.ProductTemplateLibrary
    {

        private readonly ProductTemplateLibraryDAL proDAL = new ProductTemplateLibraryDAL();

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
        public DataTable GetDataTable(SqlTransaction trans, string ProductCategoryId, int Process)
        {
            StringBuilder LeftJoin = new StringBuilder();
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and exists(select ProductTemplateId from ProductTemplateRelationSetting where ProductCategoryId=@ProductCategoryId and ProductTemplateId=a.ProductTemplateId)");
            SqlQuery.Append(" and Process=@Process");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@ProductCategoryId", ProductCategoryId));
            listParams.Add(new SqlParameter("@Process", Process));
            string FieldShow = "a.*";
            string FieldOrder = "a.SN asc";
            return proDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
        }
        #endregion

        #region 取实体集合
        /// <summary>
        /// 取实体集合
        /// </summary>
        public List<ProductTemplateLibraryModel> GetModels(SqlTransaction trans)
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
        public ProductTemplateLibraryModel GetModel(SqlTransaction trans, long SN)
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

        #region 下拉列表
        /// <summary>
        /// 下拉列表
        /// </summary>
        public List<SelectListItem> GetSelectList(SqlTransaction trans, string ProductCategoryId, int Process)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (DataRow dr in GetDataTable(trans, ProductCategoryId, Process).Rows)
            {
                list.Add(new SelectListItem() { Text = dr["ProductTemplateName"].ToString(), Value = dr["ProductTemplateId"].ToString() });
            }
            return list;
        }
        #endregion

        #region 取编号
        /// <summary>
        /// 取编号
        /// </summary>
        public string GetNo(SqlTransaction trans)
        {
            StringBuilder strNo = new StringBuilder("SXMB");
            strNo.Append(DateTime.Now.ToString("yyyyMMdd"));

            StringBuilder LeftJoin = new StringBuilder();
            StringBuilder SqlQuery = new StringBuilder();
            List<SqlParameter> listParams = new List<SqlParameter>();
            string FieldShow = "top 1 a.ProductTemplateId";
            string FieldOrder = "a.SN desc";
            DataTable dt= proDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
            if (dt.Rows.Count > 0)
            {
                string strTemp = dt.Rows[0][0].ToString();
                int len = strTemp.Length;
                int baseNo;
                if (len < 16)
                {
                    baseNo = 1;
                }
                else
                {
                    string strBaseNo = strTemp.Substring(12, 4);
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
    }
}
