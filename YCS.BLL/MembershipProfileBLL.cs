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
    /// 會員資料-业务逻辑类
    /// 创建人:楊小明
    /// 日期:2017/2/18 15:06:08
    /// </summary>

    public class MembershipProfileBLL : Base.MembershipProfile
    {

        private readonly MembershipProfileDAL memDAL = new MembershipProfileDAL();

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
            string FieldOrder = "a.DistributorId asc";
            return memDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
        }
        #endregion

        #region 取实体集合
        /// <summary>
        /// 取实体集合
        /// </summary>
        public List<MembershipProfileModel> GetModels(SqlTransaction trans)
        {
            StringBuilder SqlQuery = new StringBuilder();
            List<SqlParameter> listParams = new List<SqlParameter>();
            string FieldOrder = "DistributorId asc";
            return memDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
        }
        #endregion

        #region 取实体
        /// <summary>
        /// 取实体
        /// </summary>
        public MembershipProfileModel GetModel(SqlTransaction trans, string DistributorId)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and DistributorId=@DistributorId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@DistributorId", DistributorId));
            return memDAL.GetModel(trans, SqlQuery, listParams);
        }
        /// <summary>
        /// 取实体
        /// </summary>
        public MembershipProfileModel GetModel(SqlTransaction trans, long MemberId)
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
            return memDAL.GetAllSum(trans, LeftJoin, SqlQuery, listParams, "a.DistributorId");
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public int UpdateInfoForMember(SqlTransaction trans, MembershipProfileModel memModel, long MemberId)
        {
            string key = "Cache_MembershipProfile_Model_" + MemberId;
            CacheHelper.RemoveCache(key);
            return memDAL.UpdateInfoForMember(trans, memModel, MemberId);
        }
        #endregion

        #region 获取用户配置
        /// <summary>
        /// 获取用户配置
        /// </summary>
        public MembershipProfileModel GetMembershipProfile(SqlTransaction trans, string DistributorId, long MemberId)
        {
            StringBuilder LeftJoin = new StringBuilder();
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and MemberId=@MemberId");
            SqlQuery.Append(" and DistributorId=@DistributorId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@MemberId", MemberId));
            listParams.Add(new SqlParameter("@DistributorId", DistributorId));
            return memDAL.GetModel(trans, SqlQuery, listParams);
        }
        #endregion
    }
}
