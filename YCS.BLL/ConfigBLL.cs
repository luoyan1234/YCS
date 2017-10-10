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
    /// 系统配置-业务逻辑类
    /// 创建人:楊小明
    /// 日期:2017/2/20 11:35:13
    /// </summary>

    public class ConfigBLL : Base.Config
    {

        private readonly ConfigDAL conDAL = new ConfigDAL();

        #region 取信息分页列表
        /// <summary>
        /// 取信息分页列表
        /// </summary>
        public DataTable GetInfoPageList(SqlTransaction trans, Hashtable hs, PageHelper p, out StringBuilder PageStr)
        {
            return conDAL.GetInfoPageList(trans, hs, p, out PageStr);
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
            string FieldOrder = "a.ConfigId asc";
            return conDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
        }
        #endregion

        #region 取实体集合
        /// <summary>
        /// 取实体集合
        /// </summary>
        public List<ConfigModel> GetModels(SqlTransaction trans)
        {
            StringBuilder SqlQuery = new StringBuilder();
            List<SqlParameter> listParams = new List<SqlParameter>();
            string FieldOrder = "ConfigId asc";
            return conDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
        }
        #endregion

        #region 取实体
        /// <summary>
        /// 取实体
        /// </summary>
        public ConfigModel GetModel(SqlTransaction trans, int ConfigId)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and ConfigId=@ConfigId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@ConfigId", ConfigId));
            return conDAL.GetModel(trans, SqlQuery, listParams);
        }
        /// <summary>
        /// 从缓存中取实体
        /// </summary>
        public ConfigModel GetCacheModel(SqlTransaction trans, string DistributorId)
        {
            string key = "Cache_Config_Model_DistributorId_" + DistributorId;
            object value = CacheHelper.GetCache(key);
            if (value != null)
                return (ConfigModel)value;
            else
            {
                StringBuilder SqlQuery = new StringBuilder();
                SqlQuery.Append(" and DistributorId=@DistributorId");
                List<SqlParameter> listParams = new List<SqlParameter>();
                listParams.Add(new SqlParameter("@DistributorId", DistributorId));
                ConfigModel conModel = conDAL.GetModel(trans, SqlQuery, listParams);
                CacheHelper.AddCache(key, conModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return conModel;
            }
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
            return conDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams);
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
            return conDAL.GetAllSum(trans, LeftJoin, SqlQuery, listParams, "a.ConfigId");
        }
        #endregion

        #region 读取排序号
        /// <summary>
        /// 读取排序号
        /// </summary>
        public int GetSeqNo(SqlTransaction trans)
        {
            return conDAL.GetSeqNo(trans);
        }
        #endregion

        #region 排序信息
        /// <summary>
        /// 排序信息
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(SqlTransaction trans, int intSeqNo, int intOldSeqNo)
        {
            conDAL.OrderInfo(trans, intSeqNo, intOldSeqNo);
        }
        #endregion

        #region 取字段值
        /// <summary>
        /// 取取字段值
        /// </summary>
        public string GetValueByField(SqlTransaction trans, string strFieldName, string DistributorId)
        {
            StringBuilder LeftJoin = new StringBuilder();
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and DistributorId=@DistributorId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@DistributorId", DistributorId));
            string FieldShow = "a." + strFieldName;
            string FieldOrder = "a.ConfigId asc";
            DataTable dt = conDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0][strFieldName].ToString();
            }
            else
            {
                return "";
            }
        }
        #endregion

    }
}
