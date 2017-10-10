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
    /// 會員事件紀錄-业务逻辑类
    /// 创建人:楊小明
    /// 日期:2017/2/18 15:06:08
    /// </summary>

    public class MemberEventLogBLL : Base.MemberEventLog
    {

        private readonly MemberEventLogDAL memDAL = new MemberEventLogDAL();

        #region 取信息分页列表
        /// <summary>
        /// 取信息分页列表
        /// </summary>
        public DataTable GetInfoPageList(SqlTransaction trans, Hashtable hs, PageHelper p, out StringBuilder PageStr)
        {
            return memDAL.GetInfoPageList(trans, hs, p, out PageStr);
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
            string FieldOrder = "a.MemberId asc";
            return memDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
        }
        #endregion

        #region 取实体集合
        /// <summary>
        /// 取实体集合
        /// </summary>
        public List<MemberEventLogModel> GetModels(SqlTransaction trans)
        {
            StringBuilder SqlQuery = new StringBuilder();
            List<SqlParameter> listParams = new List<SqlParameter>();
            string FieldOrder = "MemberId asc";
            return memDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
        }
        #endregion

        #region 取实体
        /// <summary>
        /// 取实体
        /// </summary>
        public MemberEventLogModel GetModel(SqlTransaction trans, string MemberId)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and MemberId=@MemberId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@MemberId", MemberId));
            return memDAL.GetModel(trans, SqlQuery, listParams);
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
            return memDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams);
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
            return memDAL.GetAllSum(trans, LeftJoin, SqlQuery, listParams, "a.MemberId");
        }
        #endregion

        #region 添加操作日志
        /// <summary>
        /// 添加操作日志
        /// </summary>
        public void InsertLog(SqlTransaction trans, string DistributorId, EnumList.MemberEventLogType actionType, string strEventContent)
        {
            MemberEventLogModel memModel = new MemberEventLogModel();
            memModel.MemberId = HttpContext.Current.Session["MemberId"].ToLong();
            memModel.DistributorId = DistributorId;
            memModel.EventType = actionType.ToInt();
            memModel.UserAgent = HttpContext.Current.Request.UserAgent.ToString2();
            memModel.IP = HttpContext.Current.Request.UserHostAddress.ToString2();
            memModel.CreationDate = DateTimeOffset.Now;
            memModel.OpStatus = EnumList.OpStatus.成功.ToBoolean();
            memModel.EventContent = strEventContent;
            memDAL.InsertInfo(trans, memModel);
        }
        /// <summary>
        /// 添加操作日志
        /// </summary>
        public void InsertLog(SqlTransaction trans, string DistributorId, EnumList.MemberEventLogType actionType, long MemberId, bool OpStatus, string strEventContent)
        {
            MemberEventLogModel memModel = new MemberEventLogModel();
            memModel.MemberId = MemberId;
            memModel.DistributorId = DistributorId;
            memModel.EventType = actionType.ToInt();
            memModel.UserAgent = HttpContext.Current.Request.UserAgent.ToString2();
            memModel.IP = HttpContext.Current.Request.UserHostAddress.ToString2();
            memModel.CreationDate = DateTimeOffset.Now;
            memModel.OpStatus = OpStatus;
            memModel.EventContent = strEventContent;
            memDAL.InsertInfo(trans, memModel);
        }
        #endregion

        #region 取一段时间内登录失败次数
        /// <summary>
        /// 取一段时间内登录失败次数
        /// </summary>
        public int GetLoginFailedCount(SqlTransaction trans, long MemberId)
        {
            string strEventType = string.Join(",", new int[] { EnumList.MemberEventLogType.Login.ToInt(),EnumList.MemberEventLogType.FindPass.ToInt()});

            StringBuilder LeftJoin = new StringBuilder();
            StringBuilder SqlQuery = new StringBuilder();
            List<SqlParameter> listParams = new List<SqlParameter>();
            SqlQuery.Append(" and MemberId = @MemberId");
            SqlQuery.Append(" and EventType = @EventType");
            SqlQuery.Append(" and OpStatus = @OpStatus");
            SqlQuery.Append(" and DATEDIFF(ss,CreationDate,@Now)<=@CountLoginFailedTime");
            SqlQuery.Append(" and CreationDate > (select max(CreationDate) from MemberEventLog where MemberId = @MemberId and EventType in(" + strEventType + ") and OpStatus = @OpStatus2)");
            SqlQuery.Append(" and SN in(select top " + Config.LockLoginFailedCount + " SN from MemberEventLog where 1=1 and  MemberId = @MemberId and EventType = @EventType order by SN desc)");
            listParams.Add(new SqlParameter("@MemberId", MemberId));
            listParams.Add(new SqlParameter("@EventType", EnumList.MemberEventLogType.Login.ToInt()));
            listParams.Add(new SqlParameter("@OpStatus", EnumList.OpStatus.失败.ToInt()));
            listParams.Add(new SqlParameter("@Now", DateTimeOffset.Now.ToString()));
            listParams.Add(new SqlParameter("@CountLoginFailedTime", Config.CountLoginFailedTime * 60));
            listParams.Add(new SqlParameter("@OpStatus2", EnumList.OpStatus.成功.ToInt()));
            return memDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams);
        }

        #endregion
    }
}
