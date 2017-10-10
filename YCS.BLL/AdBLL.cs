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
    /// 广告-业务逻辑类
    /// 创建人:楊小明
    /// 日期:2017/2/18 15:06:07
    /// </summary>

    public class AdBLL : Base.Ad
    {

        private readonly AdDAL adDAL = new AdDAL();

        #region 取信息分页列表
        /// <summary>
        /// 取信息分页列表
        /// </summary>
        public DataTable GetInfoPageList(SqlTransaction trans, Hashtable hs, PageHelper p, out StringBuilder PageStr)
        {
            return adDAL.GetInfoPageList(trans, hs, p, out PageStr);
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
            string FieldOrder = "a.AdId asc";
            return adDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
        }
        /// <summary>
        /// 取DataTable
        /// </summary>
        public DataTable GetDataTable(SqlTransaction trans,string DistributorId, int AdPositionId, int TopNum)
        {
            StringBuilder LeftJoin = new StringBuilder();
            LeftJoin.Append(" left join AdPosition as b on b.AdPositionId=a.AdPositionId");
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and a.IsClose=@IsClose");
            SqlQuery.Append(" and a.DistributorId=@DistributorId");
            SqlQuery.Append(" and b.AdPositionId=@AdPositionId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@IsClose", EnumList.CloseStatus.Open.ToInt()));
            listParams.Add(new SqlParameter("@AdPositionId", AdPositionId));
            listParams.Add(new SqlParameter("@DistributorId", DistributorId));
            string FieldShow = (TopNum > 0 ? " top " + TopNum : "") + " a.*,b.Width,b.Height";
            string FieldOrder = "a.SeqNo asc";
            return adDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
        }
        #endregion

        #region 取实体集合
        /// <summary>
        /// 取实体集合
        /// </summary>
        public List<AdModel> GetModels(SqlTransaction trans)
        {
            StringBuilder SqlQuery = new StringBuilder();
            List<SqlParameter> listParams = new List<SqlParameter>();
            string FieldOrder = "AdId asc";
            return adDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
        }
        #endregion

        #region 取实体
        /// <summary>
        /// 取实体
        /// </summary>
        public AdModel GetModel(SqlTransaction trans, int AdId)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and AdId=@AdId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@AdId", AdId));
            return adDAL.GetModel(trans, SqlQuery, listParams);
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
            return adDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams);
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
            return adDAL.GetAllSum(trans, LeftJoin, SqlQuery, listParams, "a.AdId");
        }
        #endregion

        #region 按广告位名称读取排序号
        /// <summary>
        /// 读取排序号
        /// </summary>
        public int GetSeqNo(SqlTransaction trans, int AdPositionId)
        {
            return adDAL.GetSeqNo(trans, AdPositionId);
        }
        #endregion

        #region 排序信息
        /// <summary>
        /// 排序信息
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(SqlTransaction trans, int intSeqNo, int intOldSeqNo)
        {
            adDAL.OrderInfo(trans, intSeqNo, intOldSeqNo);
        }
        #endregion
    }
}
