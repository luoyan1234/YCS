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
    /// 文章-业务逻辑类
    /// 创建人:杨小明
    /// 日期:2015/7/10 10:49:37
    /// </summary>

    public class ArticleBLL : Base.Article
    {

        private readonly ArticleDAL artDAL = new ArticleDAL();

        #region 取信息分页列表
        /// <summary>
        /// 取信息分页列表
        /// </summary>
        public DataTable GetInfoPageList(SqlTransaction trans, Hashtable hs, PageHelper p, out StringBuilder PageStr)
        {
            return artDAL.GetInfoPageList(trans, hs, p, out PageStr);
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
            string FieldOrder = "a.ArticleId asc";
            return artDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
        }
        /// <summary>
        /// 取DataTable
        /// </summary>
        public DataTable GetDataTable(SqlTransaction trans, int ClassId, int TopNum)
        {
            StringBuilder LeftJoin = new StringBuilder();
            StringBuilder SqlQuery = new StringBuilder();
            List<SqlParameter> listParams = new List<SqlParameter>();
            SqlQuery.Append(" and IsClose=0");
            if (ClassId > 0)
            {
                SqlQuery.Append(" and a.ClassId in(select id from f_GetChildClassId(@ClassId))");
                listParams.Add(new SqlParameter("@ClassId", ClassId));
            }
            string FieldShow = (TopNum > 0 ? " top " + TopNum : "") + "a.*";
            string FieldOrder = "a.CreationDate desc";
            return artDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
        }
        #endregion

        #region 取实体集合
        /// <summary>
        /// 取实体集合
        /// </summary>
        public List<ArticleModel> GetModels(SqlTransaction trans)
        {
            StringBuilder SqlQuery = new StringBuilder();
            List<SqlParameter> listParams = new List<SqlParameter>();
            string FieldOrder = "ArticleId asc";
            return artDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
        }
        #endregion

        #region 取实体
        /// <summary>
        /// 取实体
        /// </summary>
        public ArticleModel GetModel(SqlTransaction trans, int intArticleId)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and ArticleId=@ArticleId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@ArticleId", intArticleId));
            return artDAL.GetModel(trans, SqlQuery, listParams);
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
            return artDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams);
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
            return artDAL.GetAllSum(trans, LeftJoin, SqlQuery, listParams, "a.ArticleId");
        }
        #endregion

        #region 读取排序号
        /// <summary>
        /// 读取排序号
        /// </summary>
        public int GetSeqNo(SqlTransaction trans)
        {
            return artDAL.GetSeqNo(trans);
        }
        #endregion

        #region 排序信息
        /// <summary>
        /// 排序信息
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(SqlTransaction trans, int intSeqNo, int intOldSeqNo)
        {
            artDAL.OrderInfo(trans, intSeqNo, intOldSeqNo);
        }
        #endregion

        #region 前台读取信息
        /// <summary>
        /// 前台读取信息
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        public ArticleModel GetInfo2(SqlTransaction trans, int intArticleId, string DistributorId)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and IsClose=0");
            SqlQuery.Append(" and ArticleId=@ArticleId");
            SqlQuery.Append(" and DistributorId=@DistributorId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@ArticleId", intArticleId));
            listParams.Add(new SqlParameter("@DistributorId", DistributorId));
            return artDAL.GetModel(trans, SqlQuery, listParams);
        }
        #endregion

        #region 前台从缓存读取信息
        /// <summary>
        /// 前台从缓存读取信息
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        public ArticleModel GetCacheInfo2(SqlTransaction trans, int intArticleId, string DistributorId)
        {
            string key = "Cache_Article_Model_" + intArticleId;
            object value = CacheHelper.GetCache(key);
            if (value != null)
                return (ArticleModel)value;
            else
            {
                ArticleModel artModel = GetInfo2(trans, intArticleId, DistributorId);
                CacheHelper.AddCache(key, artModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return artModel;
            }
        }
        #endregion

        #region 访问数加1
        /// <summary>
        /// 访问数加1
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        public void Click(SqlTransaction trans, int intArticleId)
        {
            artDAL.Click(trans, intArticleId);
        }
        #endregion

        #region 取上一篇信息ID
        /// <summary>
        /// 取上一篇信息ID
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        public int GetPrevID(SqlTransaction trans, int intClassId, int intArticleId, string DistributorId)
        {
            return artDAL.GetPrevID(trans, intClassId, intArticleId, DistributorId);
        }
        #endregion

        #region 取下一篇信息ID
        /// <summary>
        /// 取下一篇信息ID
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        public int GetNextID(SqlTransaction trans, int intClassId, int intArticleId, string DistributorId)
        {
            return artDAL.GetNextID(trans, intClassId, intArticleId, DistributorId);
        }
        #endregion

        #region 取字段值
        /// <summary>
        /// 取取字段值
        /// </summary>
        public string GetValueByField(SqlTransaction trans, string strFieldName, int ArticleId, string DistributorId)
        {
            StringBuilder LeftJoin = new StringBuilder();
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and ArticleId=@ArticleId");
            SqlQuery.Append(" and DistributorId=@DistributorId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@ArticleId", ArticleId));
            listParams.Add(new SqlParameter("@DistributorId", DistributorId));
            string FieldShow = "a." + strFieldName;
            string FieldOrder = "a.ArticleId asc";
            DataTable dt = artDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
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
