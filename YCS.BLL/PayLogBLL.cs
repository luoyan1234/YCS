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
    /// 支付日志-业务逻辑类
    /// 创建人:楊小明
    /// 日期:2017/2/18 15:06:09
    /// </summary>

    public class PayLogBLL : Base.PayLog
    {

        private readonly PayLogDAL payDAL = new PayLogDAL();

        #region 取信息分页列表
        /// <summary>
        /// 取信息分页列表
        /// </summary>
        public DataTable GetInfoPageList(SqlTransaction trans, Hashtable hs, PageHelper p, out StringBuilder PageStr)
        {
            return payDAL.GetInfoPageList(trans, hs, p, out PageStr);
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
            string FieldOrder = "a.PayLogId asc";
            return payDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
        }
        #endregion

        #region 取实体集合
        /// <summary>
        /// 取实体集合
        /// </summary>
        public List<PayLogModel> GetModels(SqlTransaction trans)
        {
            StringBuilder SqlQuery = new StringBuilder();
            List<SqlParameter> listParams = new List<SqlParameter>();
            string FieldOrder = "PayLogId asc";
            return payDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
        }
        #endregion

        #region 取实体
        /// <summary>
        /// 取实体
        /// </summary>
        public PayLogModel GetModel(SqlTransaction trans, int PayLogId)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and PayLogId=@PayLogId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@PayLogId", PayLogId));
            return payDAL.GetModel(trans, SqlQuery, listParams);
        }
        /// <summary>
        /// 取实体
        /// </summary>
        public PayLogModel GetModelByPayNo(SqlTransaction trans, string PayNo)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and PayNo=@PayNo");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@PayNo", PayNo));
            return payDAL.GetModel(trans, SqlQuery, listParams);
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
            return payDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams);
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
            return payDAL.GetAllSum(trans, LeftJoin, SqlQuery, listParams, "a.PayLogId");
        }
        #endregion

        #region 添加日志
        /// <summary>
        /// 添加日志
        /// </summary>
        public void InsertLog(SqlTransaction trans, int PayID, string OrderNo,string tradeStatus, decimal OrderAmount, string PayNo, decimal PayAmount, string DistributorId)
        {
            PayLogModel payLogModel = new PayLogModel();
            payLogModel.PayId = PayID;
            payLogModel.OrderNo = OrderNo;
            payLogModel.OrderAmount = OrderAmount;
            payLogModel.PayNo = PayNo;
            payLogModel.PayAmount = PayAmount;
            payLogModel.TradeNo = "";
            payLogModel.DistributorId = DistributorId;
            payLogModel.TradeStatus = tradeStatus;
            payLogModel.CreationDate = DateTime.Now;
            payLogModel.LastUpdateDate = DateTime.Now;
            payDAL.InsertInfo(trans, payLogModel);
        }
        #endregion

        #region 更新日志
        /// <summary>
        /// 更新日志
        /// </summary>
        public void UpdateLog(SqlTransaction trans, string PayNo, string TradeNo, string TradeStatus)
        {
            PayLogModel payLogModel = GetModelByPayNo(trans, PayNo);
            if (payLogModel != null)
            {
                payLogModel.TradeNo = TradeNo;
                payLogModel.TradeStatus = TradeStatus;
                payLogModel.LastUpdateDate = DateTime.Now;
                payDAL.UpdateInfo(trans, payLogModel, payLogModel.PayLogId);
            }
        }
        #endregion


    }
}
