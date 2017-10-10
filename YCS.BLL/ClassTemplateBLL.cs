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
    /// 栏目模板-业务逻辑类
    /// 创建人:楊小明
    /// 日期:2017/2/20 11:35:13
    /// </summary>

    public class ClassTemplateBLL : Base.ClassTemplate
    {

        private readonly ClassTemplateDAL claTemDAL = new ClassTemplateDAL();

        #region 取信息分页列表
        /// <summary>
        /// 取信息分页列表
        /// </summary>
        public DataTable GetInfoPageList(SqlTransaction trans, Hashtable hs, PageHelper p, out StringBuilder PageStr)
        {
            return claTemDAL.GetInfoPageList(trans, hs, p, out PageStr);
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
            string FieldOrder = "a.ClassTemplateId asc";
            return claTemDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
        }
        public DataTable GetDataTable(SqlTransaction trans, int intClassPropertyId)
        {
            StringBuilder LeftJoin = new StringBuilder();
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and IsClose=0");
            SqlQuery.Append(" and ClassPropertyId=@ClassPropertyId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@ClassPropertyId", intClassPropertyId));
            string FieldShow = "a.*";
            string FieldOrder = "a.SeqNo asc";
            return claTemDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
        }
        #endregion

        #region 取实体集合
        /// <summary>
        /// 取实体集合
        /// </summary>
        public List<ClassTemplateModel> GetModels(SqlTransaction trans)
        {
            StringBuilder SqlQuery = new StringBuilder();
            List<SqlParameter> listParams = new List<SqlParameter>();
            string FieldOrder = "ClassTemplateId asc";
            return claTemDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
        }
        #endregion

        #region 取实体
        /// <summary>
        /// 取实体
        /// </summary>
        public ClassTemplateModel GetModel(SqlTransaction trans, int ClassTemplateId)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and ClassTemplateId=@ClassTemplateId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@ClassTemplateId", ClassTemplateId));
            return claTemDAL.GetModel(trans, SqlQuery, listParams);
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
            return claTemDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams);
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
            return claTemDAL.GetAllSum(trans, LeftJoin, SqlQuery, listParams, "a.ClassTemplateId");
        }
        #endregion

        #region 读取排序号
        /// <summary>
        /// 读取排序号
        /// </summary>
        public int GetSeqNo(SqlTransaction trans)
        {
            return claTemDAL.GetSeqNo(trans);
        }
        #endregion

        #region 排序信息
        /// <summary>
        /// 排序信息
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(SqlTransaction trans, int intSeqNo, int intOldSeqNo)
        {
            claTemDAL.OrderInfo(trans, intSeqNo, intOldSeqNo);
        }
        #endregion

        #region 下拉列表
        /// <summary>
        /// 下拉列表
        /// </summary>
        public List<SelectListItem> GetSelectList(SqlTransaction trans, int intClassPropertyId)
        {
            DataTable dt = GetDataTable(trans, intClassPropertyId);

            List<SelectListItem> list = new List<SelectListItem>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new SelectListItem() { Text = dr["TemplateName"].ToString(), Value = dr["ClassTemplateId"].ToString() });
            }
            return list;
        }
        #endregion
    }
}
