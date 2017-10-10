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
    /// 代碼對應表 -业务逻辑类
    /// 创建人:楊小明
    /// 日期:2017/2/18 15:06:07
    /// </summary>

    public class CodeReferenceBLL : Base.CodeReference
    {

        private readonly CodeReferenceDAL codDAL = new CodeReferenceDAL();

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
        /// <summary>
        /// 取DataTable
        /// </summary>
        public DataTable GetDataTable(SqlTransaction trans, string ModuleId, string ConstantType)
        {
            StringBuilder LeftJoin = new StringBuilder();
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and ModuleId=@ModuleId");
            SqlQuery.Append(" and ConstantType=@ConstantType");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@ModuleId", ModuleId));
            listParams.Add(new SqlParameter("@ConstantType", ConstantType));
            string FieldShow = "a.*";
            string FieldOrder = "a.SeqNo asc";
            return codDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
        }
        #endregion

        #region 取实体集合
        /// <summary>
        /// 取实体集合
        /// </summary>
        public List<CodeReferenceModel> GetModels(SqlTransaction trans)
        {
            StringBuilder SqlQuery = new StringBuilder();
            List<SqlParameter> listParams = new List<SqlParameter>();
            string FieldOrder = "ModuleId asc";
            return codDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
        }
        /// <summary>
        /// 取实体集合
        /// </summary>
        public List<CodeReferenceModel> GetModels(SqlTransaction trans, string ModuleId, string ConstantType)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and ModuleId=@ModuleId");
            SqlQuery.Append(" and ConstantType=@ConstantType");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@ModuleId", ModuleId));
            listParams.Add(new SqlParameter("@ConstantType", ConstantType));
            string FieldOrder = "ModuleId asc";
            return codDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
        }
        #endregion

        #region 取实体
        /// <summary>
        /// 取实体
        /// </summary>
        public CodeReferenceModel GetModel(SqlTransaction trans, string ModuleId, string ConstantType, string Value)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and ModuleId=@ModuleId");
            SqlQuery.Append(" and ConstantType=@ConstantType");
            SqlQuery.Append(" and Value=@Value");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@ModuleId", ModuleId));
            listParams.Add(new SqlParameter("@ConstantType", ConstantType));
            listParams.Add(new SqlParameter("@Value", Value));
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
        #region 读取排序号
        /// <summary>
        /// 读取排序号
        /// </summary>
        public int GetSeqNo(SqlTransaction trans)
        {
            return codDAL.GetSeqNo(trans);
        }
        #endregion

        #region 排序信息
        /// <summary>
        /// 排序信息
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(SqlTransaction trans, int intSeqNo, int intOldSeqNo)
        {
            codDAL.OrderInfo(trans, intSeqNo, intOldSeqNo);
        }
        #endregion
        public List<SelectListItem> GetSlectList(SqlTransaction trans, string ModuleId, string ConstantType)
        {
            DataTable dt = GetDataTable(trans,ModuleId,ConstantType);

            List<SelectListItem> list = new List<SelectListItem>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new SelectListItem() { Text = dr["Name"].ToString(), Value = dr["Value"].ToString() });
            }
            return list;
        }
    }
}
