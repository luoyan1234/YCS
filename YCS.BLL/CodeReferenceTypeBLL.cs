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
    /// 代碼對應類型表-业务逻辑类
    /// 创建人:楊小明
    /// 日期:2017/2/18 15:06:07
    /// </summary>

    public class CodeReferenceTypeBLL : Base.CodeReferenceType
    {

        private readonly CodeReferenceTypeDAL codDAL = new CodeReferenceTypeDAL();

        #region 取信息分页列表
        /// <summary>
        /// 取信息分页列表
        /// </summary>
        public DataTable GetInfoPageList(SqlTransaction trans, Hashtable hs, PageHelper p, out StringBuilder PageStr)
        {
            return codDAL.GetInfoPageList(trans, hs, p, out PageStr);
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
            string FieldOrder = "a.ModuleId asc";
            return codDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
        }
        #endregion

        #region 取实体集合
        /// <summary>
        /// 取实体集合
        /// </summary>
        public List<CodeReferenceTypeModel> GetModels(SqlTransaction trans)
        {
            StringBuilder SqlQuery = new StringBuilder();
            List<SqlParameter> listParams = new List<SqlParameter>();
            string FieldOrder = "ModuleId asc";
            return codDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
        }
        #endregion

        #region 取实体
        /// <summary>
        /// 取实体
        /// </summary>
        public CodeReferenceTypeModel GetModel(SqlTransaction trans, string ModuleId, string ConstantType)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and ModuleId=@ModuleId");
            SqlQuery.Append(" and ConstantType=@ConstantType");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@ModuleId", ModuleId));
            listParams.Add(new SqlParameter("@ConstantType", ConstantType));
            return codDAL.GetModel(trans, SqlQuery, listParams);
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
            return codDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams);
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
            return codDAL.GetAllSum(trans, LeftJoin, SqlQuery, listParams, "a.ModuleId");
        }
        #endregion

        public List<SelectListItem> GetSlectList(SqlTransaction trans)
        {
            DataTable dt = GetDataTable(trans);

            List<SelectListItem> list = new List<SelectListItem>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new SelectListItem() { Text = dr["ModuleId"].ToString() + "_" + dr["ConstantType"].ToString(), Value = dr["ModuleId"].ToString() + "_" + dr["ConstantType"].ToString() });
            }
            return list;
        }

    }
}
