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
    /// 栏目属性-业务逻辑类
    /// 创建人:楊小明
    /// 日期:2017/2/20 11:35:13
    /// </summary>

    public class ClassPropertyBLL : Base.ClassProperty
    {

        private readonly ClassPropertyDAL claProDAL = new ClassPropertyDAL();

        #region 取信息分页列表
        /// <summary>
        /// 取信息分页列表
        /// </summary>
        public DataTable GetInfoPageList(SqlTransaction trans, Hashtable hs, PageHelper p, out StringBuilder PageStr)
        {
            return claProDAL.GetInfoPageList(trans, hs, p, out PageStr);
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
            SqlQuery.Append(" and IsClose=@IsClose");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@IsClose", EnumList.CloseStatus.Open.ToInt()));
            string FieldShow = "a.*";
            string FieldOrder = "a.ClassPropertyId asc";
            return claProDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
        }
        #endregion

        #region 取实体集合
        /// <summary>
        /// 取实体集合
        /// </summary>
        public List<ClassPropertyModel> GetModels(SqlTransaction trans)
        {
            StringBuilder SqlQuery = new StringBuilder();
            List<SqlParameter> listParams = new List<SqlParameter>();
            string FieldOrder = "ClassPropertyId asc";
            return claProDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
        }
        #endregion

        #region 取实体
        /// <summary>
        /// 取实体
        /// </summary>
        public ClassPropertyModel GetModel(SqlTransaction trans, int ClassPropertyId)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and ClassPropertyId=@ClassPropertyId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@ClassPropertyId", ClassPropertyId));
            return claProDAL.GetModel(trans, SqlQuery, listParams);
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
            return claProDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams);
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
            return claProDAL.GetAllSum(trans, LeftJoin, SqlQuery, listParams, "a.ClassPropertyId");
        }
        #endregion

        #region 读取排序号
        /// <summary>
        /// 读取排序号
        /// </summary>
        public int GetSeqNo(SqlTransaction trans)
        {
            return claProDAL.GetSeqNo(trans);
        }
        #endregion

        #region 排序信息
        /// <summary>
        /// 排序信息
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(SqlTransaction trans, int intSeqNo, int intOldSeqNo)
        {
            claProDAL.OrderInfo(trans, intSeqNo, intOldSeqNo);
        }
        #endregion

        #region 下拉列表
        /// <summary>
        /// 下拉列表
        /// </summary>
        public List<SelectListItem> GetSelectList(SqlTransaction trans)
        {
            DataTable dt = GetDataTable(trans);

            List<SelectListItem> list = new List<SelectListItem>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new SelectListItem() { Text = dr["PropertyName"].ToString(), Value = dr["ClassPropertyId"].ToString() });
            }
            return list;
        }
        #endregion
    }
}
