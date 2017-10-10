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
    /// 後台角色個別模塊權限表-业务逻辑类
    /// 创建人:楊小明
    /// 日期:2017/2/20 11:35:17
    /// </summary>

    public class SysRoleLimitBLL : Base.SysRoleLimit
    {

        private readonly SysRoleLimitDAL sysDAL = new SysRoleLimitDAL();

        #region 取信息分页列表
        /// <summary>
        /// 取信息分页列表
        /// </summary>
        public DataTable GetInfoPageList(SqlTransaction trans, Hashtable hs, PageHelper p, out StringBuilder PageStr)
        {
            return sysDAL.GetInfoPageList(trans, hs, p, out PageStr);
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
            string FieldOrder = "a.SysRoleLimitId asc";
            return sysDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
        }
        #endregion

        #region 取实体集合
        /// <summary>
        /// 取实体集合
        /// </summary>
        public List<SysRoleLimitModel> GetModels(SqlTransaction trans)
        {
            StringBuilder SqlQuery = new StringBuilder();
            List<SqlParameter> listParams = new List<SqlParameter>();
            string FieldOrder = "SysRoleLimitId asc";
            return sysDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
        }
        #endregion

        #region 取实体
        /// <summary>
        /// 取实体
        /// </summary>
        public SysRoleLimitModel GetModel(SqlTransaction trans, int SysRoleLimitId)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and SysRoleLimitId=@SysRoleLimitId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@SysRoleLimitId", SysRoleLimitId));
            return sysDAL.GetModel(trans, SqlQuery, listParams);
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
            return sysDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams);
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
            return sysDAL.GetAllSum(trans, LeftJoin, SqlQuery, listParams, "a.SysRoleLimitId");
        }
        #endregion
        #region 批量保存权限配置
        /// <summary>
        /// 批量保存权限配置
        /// </summary>
        /// <param name="SysRoleId"></param>
        /// <param name="ModuleLimits"></param>
        public void SaveRoleLimit(int SysRoleId, string[] ModuleLimits, int adminId)
        {
            sysDAL.SaveRoleLimit(SysRoleId, ModuleLimits, adminId);
        }
        #endregion
        #region 读取模块权限标识
        /// <summary>
        /// 读取模块权限标识
        /// </summary>
        public string GetModuleLimits(SqlTransaction trans, int AdminId)
        {
            string key = "Cache_SysRoleLimit_ModuleLimits_" + AdminId;
            object value = CacheHelper.GetCache(key);
            if (value != null)
                return (string)value;
            else
            {
                string strModuleLimits = sysDAL.GetModuleLimits(trans, AdminId);
                CacheHelper.AddCache(key, strModuleLimits, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return strModuleLimits;
            }

        }
        #endregion
        #region 根据角色删除信息
        /// <summary>
        /// 根据角色删除信息
        /// </summary>
        public int DeleteInfoByRole(SqlTransaction trans, int SysRoleLimitId)
        {
            return sysDAL.DeleteInfoByRole(trans, SysRoleLimitId);
        }
        #endregion
    }
}
