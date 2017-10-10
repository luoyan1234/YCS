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
    /// 发票-业务逻辑类
    /// 创建人:楊小明
    /// 日期:2017/2/18 15:06:08
    /// </summary>

    public class InvoiceBLL : Base.Invoice
    {

        private readonly InvoiceDAL invDAL = new InvoiceDAL();

        #region 取信息分页列表
        /// <summary>
        /// 取信息分页列表
        /// </summary>
        public DataTable GetInfoPageList(SqlTransaction trans, Hashtable hs, PageHelper p, out StringBuilder PageStr)
        {
            return invDAL.GetInfoPageList(trans, hs, p, out PageStr);
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
            string FieldOrder = "a.InvoiceId asc";
            return invDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
        }
        #endregion

        #region 取实体集合
        /// <summary>
        /// 取实体集合
        /// </summary>
        public List<InvoiceModel> GetModels(SqlTransaction trans)
        {
            StringBuilder SqlQuery = new StringBuilder();
            List<SqlParameter> listParams = new List<SqlParameter>();
            string FieldOrder = "InvoiceId asc";
            return invDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
        }
        #endregion

        #region 取实体
        /// <summary>
        /// 取实体
        /// </summary>
        public InvoiceModel GetModel(SqlTransaction trans, int InvoiceId)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and InvoiceId=@InvoiceId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@InvoiceId", InvoiceId));
            return invDAL.GetModel(trans, SqlQuery, listParams);
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
            return invDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams);
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
            return invDAL.GetAllSum(trans, LeftJoin, SqlQuery, listParams, "a.InvoiceId");
        }
        #endregion

        #region 获取用户发票
        /// <summary>
        /// 获取用户发票
        /// </summary>
        public InvoiceModel GetInvoiceInfo(SqlTransaction trans,  string DistributorId,long MemberId)
        {
            StringBuilder LeftJoin = new StringBuilder();
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and MemberId=@MemberId");
            SqlQuery.Append(" and DistributorId=@DistributorId");
            SqlQuery.Append(" and Status=@Status");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@MemberId", MemberId));
            listParams.Add(new SqlParameter("@DistributorId", DistributorId));
            listParams.Add(new SqlParameter("@Status", EnumList.IsStatus.Yes.ToInt()));
            return invDAL.GetModel(trans, SqlQuery, listParams);
        }
        #endregion
    }
}
