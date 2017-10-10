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
    /// 验证日志-业务逻辑类
    /// 创建人:楊小明
    /// 日期:2017/2/18 15:06:10
    /// </summary>

    public class VerifyLogBLL : Base.VerifyLog
    {

        private readonly VerifyLogDAL verDAL = new VerifyLogDAL();

        #region 取信息分页列表
        /// <summary>
        /// 取信息分页列表
        /// </summary>
        public DataTable GetInfoPageList(SqlTransaction trans, Hashtable hs, PageHelper p, out StringBuilder PageStr)
        {
            return verDAL.GetInfoPageList(trans, hs, p, out PageStr);
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
            string FieldOrder = "a.VerifyLogId asc";
            return verDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
        }
        #endregion

        #region 取实体集合
        /// <summary>
        /// 取实体集合
        /// </summary>
        public List<VerifyLogModel> GetModels(SqlTransaction trans)
        {
            StringBuilder SqlQuery = new StringBuilder();
            List<SqlParameter> listParams = new List<SqlParameter>();
            string FieldOrder = "VerifyLogId asc";
            return verDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
        }
        #endregion

        #region 取实体
        /// <summary>
        /// 取实体
        /// </summary>
        public VerifyLogModel GetModel(SqlTransaction trans, int VerifyLogId)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and VerifyLogId=@VerifyLogId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@VerifyLogId", VerifyLogId));
            return verDAL.GetModel(trans, SqlQuery, listParams);
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
            return verDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams);
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
            return verDAL.GetAllSum(trans, LeftJoin, SqlQuery, listParams, "a.VerifyLogId");
        }
        #endregion

        #region 验证码重发间隔
        /// <summary>
        /// 验证码重发间隔
        /// </summary>
        public bool CheckReSendTime(SqlTransaction trans,string DistributorId, int VerifyType, string VerifyObject)
        {
            StringBuilder LeftJoin = new StringBuilder();
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and DistributorId=@DistributorId");
            SqlQuery.Append(" and VerifyType=@VerifyType");
            SqlQuery.Append(" and VerifyObject=@VerifyObject");
            SqlQuery.Append(" and DATEDIFF(s,CreationDate,@Now)<=@ReSendTime");
            SqlQuery.Append(" and IsVerify=@IsVerify");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@DistributorId", DistributorId));
            listParams.Add(new SqlParameter("@VerifyType", VerifyType));
            listParams.Add(new SqlParameter("@VerifyObject", VerifyObject));
            listParams.Add(new SqlParameter("@Now", DateTimeOffset.Now.ToString()));
            listParams.Add(new SqlParameter("@ReSendTime", Config.ReSendTime));
            listParams.Add(new SqlParameter("@IsVerify", EnumList.VerifyStatus.未验证.ToInt()));
            return verDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams) > 0;
        }
        /// <summary>
        /// 验证码重发间隔
        /// </summary>
        public bool CheckReSendTime(SqlTransaction trans, string DistributorId, int VerifyType, int VerifyAction, string VerifyObject)
        {
            StringBuilder LeftJoin = new StringBuilder();
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and DistributorId=@DistributorId");
            SqlQuery.Append(" and VerifyType=@VerifyType");
            SqlQuery.Append(" and VerifyAction=@VerifyAction");
            SqlQuery.Append(" and VerifyObject=@VerifyObject");
            SqlQuery.Append(" and DATEDIFF(s,CreationDate,@Now)<=@ReSendTime");
            SqlQuery.Append(" and IsVerify=@IsVerify");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@DistributorId", DistributorId));
            listParams.Add(new SqlParameter("@VerifyType", VerifyType));
            listParams.Add(new SqlParameter("@VerifyAction", VerifyAction));
            listParams.Add(new SqlParameter("@VerifyObject", VerifyObject));
            listParams.Add(new SqlParameter("@Now", DateTimeOffset.Now.ToString()));
            listParams.Add(new SqlParameter("@ReSendTime", Config.ReSendTime));
            listParams.Add(new SqlParameter("@IsVerify", EnumList.VerifyStatus.未验证.ToInt()));
            return verDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams) > 0;
        }
        #endregion

        #region 检测有效时间内的验证码
        /// <summary>
        /// 检测有效时间内的验证码
        /// </summary>
        public bool CheckValidVerifyCode(SqlTransaction trans,string DistributorId, int VerifyType, int VerifyAction, string VerifyObject, string VerifyCode)
        {
            StringBuilder LeftJoin = new StringBuilder();
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and DistributorId=@DistributorId");
            SqlQuery.Append(" and IsVerify=@IsVerify");
            SqlQuery.Append(" and VerifyType=@VerifyType");
            SqlQuery.Append(" and VerifyAction=@VerifyAction");
            SqlQuery.Append(" and VerifyObject=@VerifyObject");
            SqlQuery.Append(" and VerifyCode=@VerifyCode");
            SqlQuery.Append(" and DATEDIFF(ss,CreationDate,@Now)<=@ValidTime");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@DistributorId", DistributorId));
            listParams.Add(new SqlParameter("@IsVerify", EnumList.VerifyStatus.未验证.ToInt()));
            listParams.Add(new SqlParameter("@VerifyType", VerifyType));
            listParams.Add(new SqlParameter("@VerifyAction", VerifyAction));
            listParams.Add(new SqlParameter("@VerifyObject", VerifyObject));
            listParams.Add(new SqlParameter("@VerifyCode", VerifyCode));
            listParams.Add(new SqlParameter("@Now", DateTimeOffset.Now.ToString()));
            listParams.Add(new SqlParameter("@ValidTime", Config.ValidTime * 60));
            return verDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams) > 0;
        }
        #endregion

        #region 更新验证码状态
        /// <summary>
        /// 更新验证码状态
        /// </summary>
        public int UpdateVerifyStatus(SqlTransaction trans,string DistributorId, int VerifyType, int VerifyAction, string VerifyObject, string VerifyCode)
        {
            return verDAL.UpdateVerifyStatus(trans,DistributorId, VerifyType, VerifyAction, VerifyObject, VerifyCode);
        }
        #endregion
    }
}
