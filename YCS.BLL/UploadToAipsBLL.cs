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
    /// 上传文件至Aips-业务逻辑类
    /// 创建人:杨小明
    /// 日期:2017/5/10 9:42:46 +08:00
    /// </summary>

    public class UploadToAipsBLL : Base.UploadToAips
    {

        private readonly UploadToAipsDAL uplDAL = new UploadToAipsDAL();

        #region 取信息分页列表
        /// <summary>
        /// 取信息分页列表
        /// </summary>
        public DataTable GetInfoPageList(SqlTransaction trans, Hashtable hs, PageHelper p, out StringBuilder PageStr)
        {
            return uplDAL.GetInfoPageList(trans, hs, p, out PageStr);
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
            string FieldOrder = "a.UploadToAipsId asc";
            return uplDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
        }
        /// <summary>
        /// 取DataTable
        /// </summary>
        public DataTable GetDataTable(SqlTransaction trans,int SendStatus)
        {
            StringBuilder LeftJoin = new StringBuilder();
            LeftJoin.Append(" left join MembershipUser as b on b.MemberId=a.MemberId");
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and a.SendStatus=@SendStatus");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@SendStatus", SendStatus));
            string FieldShow = "a.UploadToAipsId,b.UserName,b.Password";
            string FieldOrder = "a.UploadToAipsId asc";
            return uplDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
        }
        #endregion

        #region 取实体集合
        /// <summary>
        /// 取实体集合
        /// </summary>
        public List<UploadToAipsModel> GetModels(SqlTransaction trans)
        {
            StringBuilder SqlQuery = new StringBuilder();
            List<SqlParameter> listParams = new List<SqlParameter>();
            string FieldOrder = "UploadToAipsId asc";
            return uplDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
        }
        #endregion

        #region 取实体
        /// <summary>
        /// 取实体
        /// </summary>
        public UploadToAipsModel GetModel(SqlTransaction trans, int UploadToAipsId)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and UploadToAipsId=@UploadToAipsId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@UploadToAipsId", UploadToAipsId));
            return uplDAL.GetModel(trans, SqlQuery, listParams);
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
            return uplDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams);
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
            return uplDAL.GetAllSum(trans, LeftJoin, SqlQuery, listParams, "a.UploadToAipsId");
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public int UpdateInfoNoCache(SqlTransaction trans, UploadToAipsModel uplModel, int UploadToAipsId)
        {
            return uplDAL.UpdateInfo(trans, uplModel, UploadToAipsId);
        }
        #endregion
    }
}
