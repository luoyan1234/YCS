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
using System.Xml;

namespace YCS.BLL
{
    /// <summary>
    /// 错误日志-业务逻辑类
    /// 创建人:楊小明
    /// 日期:2017/2/20 11:35:14
    /// </summary>

    public class ErrorBLL : Base.Error
    {

        private readonly ErrorDAL errDAL = new ErrorDAL();

        #region 取信息分页列表
        /// <summary>
        /// 取信息分页列表
        /// </summary>
        public DataTable GetInfoPageList(SqlTransaction trans, Hashtable hs, PageHelper p, out StringBuilder PageStr)
        {
            return errDAL.GetInfoPageList(trans, hs, p, out PageStr);
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
            string FieldOrder = "a.ErrorId asc";
            return errDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
        }
        #endregion

        #region 取实体集合
        /// <summary>
        /// 取实体集合
        /// </summary>
        public List<ErrorModel> GetModels(SqlTransaction trans)
        {
            StringBuilder SqlQuery = new StringBuilder();
            List<SqlParameter> listParams = new List<SqlParameter>();
            string FieldOrder = "ErrorId asc";
            return errDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
        }
        #endregion

        #region 取实体
        /// <summary>
        /// 取实体
        /// </summary>
        public ErrorModel GetModel(SqlTransaction trans, int ErrorId)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and ErrorId=@ErrorId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@ErrorId", ErrorId));
            return errDAL.GetModel(trans, SqlQuery, listParams);
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
            return errDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams);
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
            return errDAL.GetAllSum(trans, LeftJoin, SqlQuery, listParams, "a.ErrorId");
        }
        #endregion

        #region 添加错误日志
        /// <summary>
        /// 添加操作日志
        /// </summary>
        public void InsertLog(SqlTransaction trans, Exception ex)
        {

            ErrorModel errModel = new ErrorModel();
            errModel.Message = ex.Message.ToString2();
            errModel.InnerException = ex.InnerException.ToString2();
            errModel.StackTrace = ex.StackTrace.ToString2();
            errModel.Source = ex.Source.ToString2();
            errModel.TargetSite = ex.TargetSite.ToString2();
            errModel.RequestUrl = HttpContext.Current.Request.Url.ToString2();
            errModel.RequestMethod = HttpContext.Current.Request.HttpMethod.ToString2();
            errModel.UrlReferrer = HttpContext.Current.Request.UrlReferrer.ToString2();
            errModel.UserAgent = HttpContext.Current.Request.UserAgent.ToString2();
            errModel.UserHostAddress = HttpContext.Current.Request.UserHostAddress.ToString2();
            errModel.CreationDate = DateTimeOffset.Now;
            errDAL.InsertInfo(trans, errModel);
        }
        #endregion

    }
}
