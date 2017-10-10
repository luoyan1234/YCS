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
    /// 地区-业务逻辑类
    /// 创建人:楊小明
    /// 日期:2017/2/18 15:06:07
    /// </summary>

    public class AreaBLL : Base.Area
    {

        private readonly AreaDAL areDAL = new AreaDAL();

        #region 取信息分页列表
        /// <summary>
        /// 取信息分页列表
        /// </summary>
        public DataTable GetInfoPageList(SqlTransaction trans, Hashtable hs, PageHelper p, out StringBuilder PageStr)
        {
            return areDAL.GetInfoPageList(trans, hs, p, out PageStr);
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
            string FieldOrder = "a.AreaId asc";
            return areDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
        }

        /// <summary>
        /// 取DataTable
        /// </summary>
        public DataTable GetDataTable(SqlTransaction trans, int ParentId)
        {
            DataSet ds = areDAL.GetCacheDateSet(trans);
            DataView dv = ds.Tables[0].DefaultView;
            dv.RowFilter = "IsClose=0 and ParentId=" + ParentId;
            dv.Sort = "SeqNo asc";
            DataTable dt = dv.ToTable();
            return dt;
        }
        /// <summary>
        /// 取DataTable
        /// </summary>
        public DataTable GetDataTableForUserAddr(SqlTransaction trans, int ParentId, int Level)
        {
            DataSet ds = areDAL.GetCacheDateSet(trans);
            DataView dv = ds.Tables[0].DefaultView;
            string strSql = "";
            if (Level == 1)
            {
                strSql = " and AreaId in(-1," + Config.DeliverProvinces + ",-1)";
            }
            else if (Level == 2)
            {
                strSql = " and AreaId in(-1," + Config.DeliverCitys + ",-1)";
            }
            else
            {

            }
            dv.RowFilter = "IsClose=0 and ParentId=" + ParentId + strSql;
            dv.Sort = "SeqNo asc";
            DataTable dt = dv.ToTable();
            return dt;
        }
        #endregion

        #region 取实体集合
        /// <summary>
        /// 取实体集合
        /// </summary>
        public List<AreaModel> GetModels(SqlTransaction trans)
        {
            StringBuilder SqlQuery = new StringBuilder();
            List<SqlParameter> listParams = new List<SqlParameter>();
            string FieldOrder = "AreaId asc";
            return areDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
        }
        #endregion

        #region 取实体
        /// <summary>
        /// 取实体
        /// </summary>
        public AreaModel GetModel(SqlTransaction trans, int AreaId)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and AreaId=@AreaId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@AreaId", AreaId));
            return areDAL.GetModel(trans, SqlQuery, listParams);
        }
        public AreaModel GetModel(SqlTransaction trans, string AreaName)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and AreaName like @AreaName");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@AreaName", AreaName));
            return areDAL.GetModel(trans, SqlQuery, listParams);
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
            return areDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams);
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
            return areDAL.GetAllSum(trans, LeftJoin, SqlQuery, listParams, "a.AreaId");
        }
        #endregion
        #region 读取排序号
        /// <summary>
        /// 读取排序号
        /// </summary>
        public int GetSeqNo(SqlTransaction trans, int intParentId)
        {
            return areDAL.GetSeqNo(trans, intParentId);
        }
        #endregion
        #region 排序信息
        /// <summary>
        /// 排序信息
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(SqlTransaction trans, int intParentId, int intSeqNo, int intOldSeqNo)
        {
            areDAL.OrderInfo(trans, intParentId, intSeqNo, intOldSeqNo);
        }
        #endregion

        #region 增加子级数
        /// <summary>
        /// 增加子级数
        /// </summary>
        public void AddChildNum(SqlTransaction trans, int intAreaId)
        {
            areDAL.AddChildNum(trans, intAreaId);
        }
        #endregion

        #region 减少子级数
        /// <summary>
        /// 减少子级数
        /// </summary>
        public void CutChildNum(SqlTransaction trans, int intAreaId)
        {
            areDAL.CutChildNum(trans, intAreaId);
        }
        #endregion
        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(SqlTransaction trans, int intParentId, string strFieldName, string strFieldValue, int intAreaId)
        {
            return areDAL.CheckInfo(trans, intParentId, strFieldName, strFieldValue, intAreaId);
        }
        #endregion
        #region 显示下拉树形列表
        /// <summary>
        /// 显示下拉树形列表
        /// </summary>
        public List<SelectListItem> GetSelectTreeList(SqlTransaction trans, int intParentId, int intLevel, List<SelectListItem> list, string strSql)
        {
            return areDAL.GetSelectTreeList(trans, intParentId, intLevel, list, strSql);
        }
        #endregion

        #region 读取地区名称
        /// <summary>
        /// 读取地区名称
        /// </summary>
        public string GetAreaNames(SqlTransaction trans, string strAreaIds)
        {
            return areDAL.GetAreaNames(trans, strAreaIds);
        }
        #endregion

        #region 转移时更新子级数
        /// <summary>
        /// 修改时更新子级数
        /// </summary>
        public void UpdateChildNum(SqlTransaction trans, int intTargetParentId, int intOldParentId)
        {
            if (intTargetParentId != intOldParentId)
            {
                areDAL.AddChildNum(trans, intTargetParentId);
                areDAL.CutChildNum(trans, intOldParentId);
            }
        }
        #endregion

        #region 下拉列表
        /// <summary>
        /// 下拉列表
        /// </summary>
        public List<SelectListItem> GetSelectList(SqlTransaction trans, object ParentId)
        {
            try
            {
                if (ParentId != null)
                {
                    DataTable dt = GetDataTable(trans, ParentId.ToInt());
                    List<SelectListItem> list = new List<SelectListItem>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        list.Add(new SelectListItem() { Text = dr["AreaName"].ToString(), Value = dr["AreaId"].ToString() });
                    }
                    return list;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 下拉列表
        /// </summary>
        public List<SelectListItem> GetSelectListForUserAddr(SqlTransaction trans, object ParentId,int Level)
        {
            try
            {
                if (ParentId != null)
                {
                    DataTable dt = GetDataTableForUserAddr(trans, ParentId.ToInt(), Level);
                    List<SelectListItem> list = new List<SelectListItem>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        list.Add(new SelectListItem() { Text = dr["AreaName"].ToString(), Value = dr["AreaId"].ToString() });
                    }
                    return list;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
        #endregion
        #region 显示路径
        /// <summary>
        /// 显示路径
        /// </summary>
        public StringBuilder ShowPath(SqlTransaction trans, int intAreaId)
        {
            StringBuilder tempStr = new StringBuilder("根结点");
            AreaModel areModel = new AreaModel();
            areModel = areDAL.GetInfo(trans, intAreaId);
            if (areModel == null)
            {
                return tempStr;
            }
            else
            {
                string strPath = areDAL.GetPath(trans, intAreaId).ToString();
                string[] arrPath = strPath.Split(',');
                foreach (var item in arrPath)
                {
                    AreaModel areModel_2 = new AreaModel();
                    areModel_2 = areDAL.GetInfo(trans, Convert.ToInt32(item));
                    if (areModel_2 != null)
                    {
                        tempStr.Append(" > " + areModel_2.AreaName);
                    }
                }
                return tempStr;
            }
        }
        #endregion
    }
}
