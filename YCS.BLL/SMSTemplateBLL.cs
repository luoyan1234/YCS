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
    /// 短信模板表-业务逻辑类
    /// 创建人:楊小明
    /// 日期:2017/2/18 15:06:10
    /// </summary>

    public class SMSTemplateBLL : Base.SMSTemplate
    {

        private readonly SMSTemplateDAL smsDAL = new SMSTemplateDAL();

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
            string FieldOrder = "a.SMSTemplateId asc";
            return smsDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
        }
        #endregion

        #region 取实体集合
        /// <summary>
        /// 取实体集合
        /// </summary>
        public List<SMSTemplateModel> GetModels(SqlTransaction trans)
        {
            StringBuilder SqlQuery = new StringBuilder();
            List<SqlParameter> listParams = new List<SqlParameter>();
            string FieldOrder = "SMSTemplateId asc";
            return smsDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
        }
        /// <summary>
        /// 取实体集合
        /// </summary>
        public List<SMSTemplateModel> GetModels(SqlTransaction trans, string smsText)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and smsText like '%" + smsText + "%'");
            List<SqlParameter> listParams = new List<SqlParameter>();
            string FieldOrder = "SMSTemplateId asc";
            return smsDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
        }
        #endregion

        #region 取实体
        /// <summary>
        /// 取实体
        /// </summary>
        public SMSTemplateModel GetModel(SqlTransaction trans, int SMSTemplateId)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and SMSTemplateId=@SMSTemplateId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@SMSTemplateId", SMSTemplateId));
            return smsDAL.GetModel(trans, SqlQuery, listParams);
        }

        /// <summary>
        /// 取实体
        /// </summary>
        public SMSTemplateModel GetModel(SqlTransaction trans, string smsCode)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and SMSCode=@SMSCode");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@SMSCode", smsCode));
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
            return smsDAL.GetAllSum(trans, LeftJoin, SqlQuery, listParams, "a.SMSTemplateId");
        }
        #endregion

    }
}
