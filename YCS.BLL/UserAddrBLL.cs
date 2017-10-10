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
    /// 会员地址-业务逻辑类
    /// 创建人:楊小明
    /// 日期:2017/2/18 15:06:10
    /// </summary>

    public class UserAddrBLL : Base.UserAddr
    {

        private readonly UserAddrDAL useDAL = new UserAddrDAL();

        #region 取信息分页列表
        /// <summary>
        /// 取信息分页列表
        /// </summary>
        public DataTable GetInfoPageList(SqlTransaction trans, Hashtable hs, PageHelper p, out StringBuilder PageStr)
        {
            return useDAL.GetInfoPageList(trans, hs, p, out PageStr);
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
            string FieldOrder = "a.UserAddrId asc";
            return useDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
        }
        public DataTable GetDataTable(SqlTransaction trans, long MemberId)
        {
            StringBuilder LeftJoin = new StringBuilder();
            LeftJoin.Append(" left join Area as c on c.AreaId=a.ProvinceId");
            LeftJoin.Append(" left join Area as d on d.AreaId=a.CityId");
            LeftJoin.Append(" left join Area as e on e.AreaId=a.AreaId");
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and MemberId=@MemberId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@MemberId", MemberId));
            string FieldShow = "a.*,c.AreaName as Province,d.AreaName as City,e.AreaName as Area";
            string FieldOrder = "a.IsDefault desc,a.UserAddrId asc";
            return useDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
        }
        #endregion

        #region 取实体集合
        /// <summary>
        /// 取实体集合
        /// </summary>
        public List<UserAddrModel> GetModels(SqlTransaction trans)
        {
            StringBuilder SqlQuery = new StringBuilder();
            List<SqlParameter> listParams = new List<SqlParameter>();
            string FieldOrder = "UserAddrId asc";
            return useDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
        }
        #endregion

        #region 取实体
        /// <summary>
        /// 取实体
        /// </summary>
        public UserAddrModel GetModel(SqlTransaction trans, int UserAddrId)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and UserAddrId=@UserAddrId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@UserAddrId", UserAddrId));
            return useDAL.GetModel(trans, SqlQuery, listParams);
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
            return useDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams);
        }
        public int GetAllCount(SqlTransaction trans, long MemberId)
        {
            StringBuilder LeftJoin = new StringBuilder();
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and MemberId=@MemberId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@MemberId", MemberId));
            return useDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams);
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
            return useDAL.GetAllSum(trans, LeftJoin, SqlQuery, listParams, "a.UserAddrId");
        }
        #endregion
        #region 设置默认地址
        /// <summary>
        /// 设置默认地址
        /// </summary>
        public int SetDefault(SqlTransaction trans, long MemberId, int UserAddrID)
        {
            return useDAL.SetDefault(trans, MemberId, UserAddrID);
        }
        #endregion
    }
}
