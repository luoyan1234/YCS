﻿using System;
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
    /// 日期:2017/4/1 13:51:07 +08:00
    /// </summary>

    public class PortfolioBLL : Base.Portfolio
    {

        private readonly PortfolioDAL porDAL = new PortfolioDAL();

        #region 取信息分页列表
        /// <summary>
        /// 取信息分页列表
        /// </summary>
        public DataTable GetInfoPageList(SqlTransaction trans, Hashtable hs, PageHelper p, out StringBuilder PageStr)
        {
            return porDAL.GetInfoPageList(trans, hs, p, out PageStr);
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
            return porDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
        }
        #endregion

        #region 取实体集合
        /// <summary>
        /// 取实体集合
        /// </summary>
        public List<PortfolioModel> GetModels(SqlTransaction trans)
        {
            StringBuilder SqlQuery = new StringBuilder();
            List<SqlParameter> listParams = new List<SqlParameter>();
            string FieldOrder = "SN asc";
            return porDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
        }
        #endregion

        #region 取实体
        /// <summary>
        /// 取实体
        /// </summary>
        public PortfolioModel GetModel(SqlTransaction trans, long SN)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and SN=@SN");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@SN", SN));
            return porDAL.GetModel(trans, SqlQuery, listParams);
        }
        /// <summary>
        /// 取实体
        /// </summary>
        public PortfolioModel GetModel(SqlTransaction trans, string DistributorId, long MemberId, string ProductSpuId, string ProductSkuId)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and DistributorId=@DistributorId");
            SqlQuery.Append(" and MemberId=@MemberId");
            SqlQuery.Append(" and ProductSpuId=@ProductSpuId");
            SqlQuery.Append(" and ProductSkuId=@ProductSkuId");
            SqlQuery.Append(" and Status = 100");// 100正常.  -10已刪除..等作品狀態
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@DistributorId", DistributorId));
            listParams.Add(new SqlParameter("@MemberId", MemberId));
            listParams.Add(new SqlParameter("@ProductSpuId", ProductSpuId));
            listParams.Add(new SqlParameter("@ProductSkuId", ProductSkuId));
            return porDAL.GetModel(trans, SqlQuery, listParams);
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
            return porDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams);
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
            return porDAL.GetAllSum(trans, LeftJoin, SqlQuery, listParams, "a.SN");
        }
        #endregion

    }
}