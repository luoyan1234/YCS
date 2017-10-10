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
    /// 短信日志-业务逻辑类
    /// 创建人:楊小明
    /// 日期:2017/2/18 15:06:09
    /// </summary>

    public class SMSLogBLL : Base.SMSLog
    {

        private readonly SMSLogDAL smsDAL = new SMSLogDAL();

        #region 取信息分页列表
        /// <summary>
        /// 取信息分页列表
        /// </summary>
        public DataTable GetInfoPageList(SqlTransaction trans, Hashtable hs, PageHelper p, out StringBuilder PageStr)
        {
            return smsDAL.GetInfoPageList(trans, hs, p, out PageStr);
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
            string FieldOrder = "a.SMSLogId asc";
            return smsDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
        }
        #endregion

        #region 取实体集合
        /// <summary>
        /// 取实体集合
        /// </summary>
        public List<SMSLogModel> GetModels(SqlTransaction trans)
        {
            StringBuilder SqlQuery = new StringBuilder();
            List<SqlParameter> listParams = new List<SqlParameter>();
            string FieldOrder = "SMSLogId asc";
            return smsDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
        }
        #endregion

        #region 取实体
        /// <summary>
        /// 取实体
        /// </summary>
        public SMSLogModel GetModel(SqlTransaction trans, int SMSLogId)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and SMSLogId=@SMSLogId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@SMSLogId", SMSLogId));
            return smsDAL.GetModel(trans, SqlQuery, listParams);
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
            return smsDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams);
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
            return smsDAL.GetAllSum(trans, LeftJoin, SqlQuery, listParams, "a.SMSLogId");
        }
        #endregion

        #region 添加操作日志
        /// <summary>
        /// 添加操作日志
        /// </summary>
        public void InsertLog(SqlTransaction trans, string MsgId, string Mobile, string LogContent, int ReturnCode, string ReturnDesc)
        {
            SMSLogModel smsLogModel = new SMSLogModel();
            smsLogModel.MsgId = MsgId;
            smsLogModel.Mobile = Mobile;
            smsLogModel.LogContent = LogContent;
            smsLogModel.Code = ReturnCode;
            smsLogModel.Description = ReturnDesc;
            smsLogModel.ScriptFile = HttpContext.Current.Request.RawUrl.ToString2();
            smsLogModel.IPAddress = HttpContext.Current.Request.UserHostAddress.ToString2();
            smsLogModel.CreationDate = DateTimeOffset.Now;
            smsLogModel.LastUpdateDate = DateTimeOffset.Now;
            smsDAL.InsertInfo(trans, smsLogModel);
        }
        #endregion
    }
}
