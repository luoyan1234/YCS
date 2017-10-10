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
    /// 栏目-业务逻辑类
    /// 创建人:杨小明
    /// 日期:2015/7/9 15:46:19
    /// </summary>

    public class ClassBLL : Base.Class
    {

        private readonly ClassDAL claDAL = new ClassDAL();

        #region 取信息分页列表
        /// <summary>
        /// 取信息分页列表
        /// </summary>
        public DataTable GetInfoPageList(SqlTransaction trans, Hashtable hs, PageHelper p, out StringBuilder PageStr)
        {
            return claDAL.GetInfoPageList(trans, hs, p, out PageStr);
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
            string FieldOrder = "a.ClassId asc";
            return claDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
        }
        /// <summary>
        /// 取DataTable
        /// </summary>
        public DataTable GetDataTable(SqlTransaction trans, int ParentId, int TopNum)
        {
            StringBuilder LeftJoin = new StringBuilder();
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and IsClose=0");
            SqlQuery.Append(" and ParentId=@ParentId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@ParentId", ParentId));
            string FieldShow = (TopNum > 0 ? " top " + TopNum : "") + "a.*";
            string FieldOrder = "a.SeqNo asc";
            return claDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
        }
        /// <summary>
        /// 取DataTable
        /// </summary>
        public DataTable GetDataTable(SqlTransaction trans, int ParentId, int TopNum, bool IsRecommend)
        {
            StringBuilder LeftJoin = new StringBuilder();
            StringBuilder SqlQuery = new StringBuilder();
            List<SqlParameter> listParams = new List<SqlParameter>();
            SqlQuery.Append(" and IsClose=0");
            if (IsRecommend)
            {
                SqlQuery.Append(" and IsRecommend=@IsRecommend");
                listParams.Add(new SqlParameter("@IsRecommend", EnumList.IsStatus.Yes.ToInt()));
            }
            SqlQuery.Append(" and ParentId=@ParentId");
            listParams.Add(new SqlParameter("@ParentId", ParentId));
            string FieldShow = (TopNum > 0 ? " top " + TopNum : "") + "a.*";
            string FieldOrder = "a.SeqNo asc";
            return claDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
        }
        /// <summary>
        /// 取DataTable
        /// </summary>
        public DataTable GetDataTableForSelectList(SqlTransaction trans, int ParentId, int ClassPropertyId)
        {
            StringBuilder LeftJoin = new StringBuilder();
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and IsClose=0");
            SqlQuery.Append(" and ParentId=@ParentId");
            SqlQuery.Append(" and ClassPropertyId=@ClassPropertyId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@ParentId", ParentId));
            listParams.Add(new SqlParameter("@ClassPropertyId", ClassPropertyId));
            string FieldShow = "a.*";
            string FieldOrder = "a.SeqNo asc";
            return claDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
        }

        /// <summary>
        /// 取实体集合
        /// </summary>
        public DataTable GetClassModelByClassPropertyIn(SqlTransaction trans, string ClassPropertyId)
        {
            StringBuilder LeftJoin = new StringBuilder();
            StringBuilder SqlQuery = new StringBuilder();
            List<SqlParameter> listParams = new List<SqlParameter>();
            SqlQuery.Append(" and IsClose=0");
            SqlQuery.Append(" and ClassPropertyId in(@ClassPropertyId)");
            listParams.Add(new SqlParameter("@ClassPropertyId", ClassPropertyId));
            string FieldShow = " a.*";
            string FieldOrder = " a.SeqNo asc";
            return claDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
        }
        #endregion

        #region 取实体集合
        /// <summary>
        /// 取实体集合
        /// </summary>
        public List<ClassModel> GetModels(SqlTransaction trans)
        {
            StringBuilder SqlQuery = new StringBuilder();
            List<SqlParameter> listParams = new List<SqlParameter>();
            string FieldOrder = "ClassId asc";
            return claDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
        }
        /// <summary>
        /// 取实体集合
        /// </summary>
        public List<ClassModel> GetModels(SqlTransaction trans, int ParentId)
        {
            StringBuilder SqlQuery = new StringBuilder();
            List<SqlParameter> listParams = new List<SqlParameter>();
            SqlQuery.Append(" and IsClose=0");
            SqlQuery.Append(" and ParentId=@ParentId");
            listParams.Add(new SqlParameter("@ParentId", ParentId));
            string FieldOrder = "SeqNo asc";
            return claDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
        }
        /// <summary>
        /// 取实体集合
        /// </summary>
        public List<ClassModel> GetModelsForNav(SqlTransaction trans, int ParentId)
        {
            StringBuilder SqlQuery = new StringBuilder();
            List<SqlParameter> listParams = new List<SqlParameter>();
            SqlQuery.Append(" and IsClose=0");
            SqlQuery.Append(" and IsShowNav=1");
            SqlQuery.Append(" and ParentId=@ParentId");
            listParams.Add(new SqlParameter("@ParentId", ParentId));
            string FieldOrder = "SeqNo asc";
            return claDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
        }
        /// <summary>
        /// 取实体集合
        /// </summary>
        public List<ClassModel> GetModelsForzTree(SqlTransaction trans, int ClassPropertyId)
        {
            StringBuilder SqlQuery = new StringBuilder();
            List<SqlParameter> listParams = new List<SqlParameter>();
            SqlQuery.Append(" and IsClose=0");
            SqlQuery.Append(" and ClassPropertyId=@ClassPropertyId");
            listParams.Add(new SqlParameter("@ClassPropertyId", ClassPropertyId));
            string FieldOrder = "SeqNo asc";
            return claDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
        }

        #endregion

        #region 取实体
        /// <summary>
        /// 取实体
        /// </summary>
        public ClassModel GetModel(SqlTransaction trans, int intClassId)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and ClassId=@ClassId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@ClassId", intClassId));
            return claDAL.GetModel(trans, SqlQuery, listParams);
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
            return claDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams);
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
            return claDAL.GetAllSum(trans, LeftJoin, SqlQuery, listParams, "a.ClassId");
        }
        #endregion

        #region 读取排序号
        /// <summary>
        /// 读取排序号
        /// </summary>
        public int GetSeqNo(SqlTransaction trans, int intParentId)
        {
            return claDAL.GetSeqNo(trans, intParentId);
        }
        #endregion

        #region 排序信息
        /// <summary>
        /// 排序信息
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(SqlTransaction trans, int intParentId, int intSeqNo, int intOldSeqNo)
        {
            claDAL.OrderInfo(trans, intParentId, intSeqNo, intOldSeqNo);
        }
        #endregion

        #region 增加子级数
        /// <summary>
        /// 增加子级数
        /// </summary>
        public void AddChildNum(SqlTransaction trans, int intSysModuleId)
        {
            claDAL.AddChildNum(trans, intSysModuleId);
        }
        #endregion

        #region 减少子级数
        /// <summary>
        /// 减少子级数
        /// </summary>
        public void CutChildNum(SqlTransaction trans, int intSysModuleId)
        {
            claDAL.CutChildNum(trans, intSysModuleId);
        }
        #endregion

        #region 取路径
        /// <summary>
        /// 取路径
        /// </summary>
        public StringBuilder GetPath(SqlTransaction trans, int intClassId)
        {
            return claDAL.GetPath(trans, intClassId);
        }
        /// <summary>
        /// 取路径
        /// </summary>
        public StringBuilder GetPath(SqlTransaction trans, int intClassId, string DistributorId)
        {
            return claDAL.GetPath(trans, intClassId, DistributorId);
        }
        /// <summary>
        /// 取路径
        /// </summary>
        public StringBuilder GetPath(SqlTransaction trans, int intClassId, int start)
        {
            string strPath = claDAL.GetPath(trans, intClassId).ToString();
            string[] ArrPath = strPath.Split(',');
            StringBuilder strReturn = new StringBuilder();
            if (start < ArrPath.Length)
            {
                for (int i = start; i < ArrPath.Length; i++)
                {
                    strReturn.Append(ArrPath[i]);
                    if (i < ArrPath.Length - 1)
                        strReturn.Append(",");
                }
            }
            return strReturn;
        }
        #endregion

        #region +显示路径
        /// <summary>
        /// 显示路径
        /// </summary>
        public StringBuilder ShowPath(SqlTransaction trans, int intClassId)
        {
            StringBuilder tempStr = new StringBuilder("根结点");
            ClassModel areModel = new ClassModel();
            areModel = claDAL.GetInfo(trans, intClassId);
            if (areModel == null)
            {
                return tempStr;
            }
            else
            {
                string strPath = claDAL.GetPath(trans, intClassId).ToString();
                string[] arrPath = strPath.Split(',');
                foreach (var item in arrPath)
                {
                    ClassModel areModel_2 = new ClassModel();
                    areModel_2 = claDAL.GetInfo(trans, Convert.ToInt32(item));
                    if (areModel_2 != null)
                    {
                        tempStr.Append(" > " + areModel_2.ClassName);
                    }
                }
                return tempStr;
            }
        }
        #endregion

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(SqlTransaction trans, int intParentId, string strFieldName, string strFieldValue, int intClassId)
        {
            return claDAL.CheckInfo(trans, intParentId, strFieldName, strFieldValue, intClassId);
        }
        #endregion

        #region 显示下拉树形列表
        /// <summary>
        /// 显示下拉树形列表
        /// </summary>
        public List<SelectListItem> GetSelectTreeList(SqlTransaction trans, int intParentId, int intLevel, string strSql, int intClassPropertyId)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            return claDAL.GetSelectTreeList(trans, intParentId, intLevel, list, strSql, intClassPropertyId);
        }
        /// <summary>
        /// 显示下拉树形列表
        /// </summary>
        public List<SelectListItem> GetSelectTreeList(SqlTransaction trans, int intParentId, int intLevel, List<SelectListItem> list, string strSql, int intClassPropertyId)
        {
            return claDAL.GetSelectTreeList(trans, intParentId, intLevel, list, strSql, intClassPropertyId);
        }
        #endregion

        #region 读取栏目名称
        /// <summary>
        /// 读取栏目名称
        /// </summary>
        public string GetClassNames(SqlTransaction trans, string strClassIds)
        {
            return claDAL.GetClassNames(trans, strClassIds);
        }
        #endregion

        #region +转移时更新子级数
        /// <summary>
        /// 修改时更新子级数
        /// </summary>
        public void UpdateChildNum(SqlTransaction trans, int intTargetParentId, int intOldParentId)
        {
            if (intTargetParentId != intOldParentId)
            {
                claDAL.AddChildNum(trans, intTargetParentId);
                claDAL.CutChildNum(trans, intOldParentId);
            }
        }
        #endregion

        #region 取头部导航菜单
        /// <summary>
        /// 取头部导航菜单(li)
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        public StringBuilder GetHeadMenu(SqlTransaction trans, string DistributorId,int intParentId, int intLevel, int intCurrentParentId, string strStyleClass, int intShowLen, string strAppUrl)
        {
            return claDAL.GetHeadMenu(trans,DistributorId, intParentId, intLevel, intCurrentParentId, strStyleClass, intShowLen, strAppUrl);
        }
        #endregion

        #region 取栏目列表
        /// <summary>
        /// 取栏目列表(a)
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        public StringBuilder GetClassList(SqlTransaction trans,string DistributorId, int intParentId, string strSeparator, int intShowLen, string strAppUrl)
        {
            return claDAL.GetClassList(trans,DistributorId, intParentId, strSeparator, intShowLen, strAppUrl);
        }
        /// <summary>
        /// 取栏目列表(ul-li)
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        public StringBuilder GetClassList(SqlTransaction trans,string DistributorId, int intParentId, string strStyleClass, int intClassId, int intShowLen, string strAppUrl)
        {
            StringBuilder strReturn = new StringBuilder();
            strReturn.Append("<ul class=\"submenu\">\n");
            strReturn.Append(claDAL.GetClassListLi(trans,DistributorId, intParentId, strStyleClass, intClassId, intShowLen, strAppUrl));
            strReturn.Append("</ul>\n");
            return strReturn;
        }
        /// <summary>
        /// 取栏目列表(a-classpath)
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        public StringBuilder GetClassList(SqlTransaction trans,string DistributorId, int intParentId, string strStyleClass, string strClassPath, int intShowLen, string strAppUrl)
        {
            StringBuilder strReturn = new StringBuilder();
            strReturn.Append(claDAL.GetClassList(trans,DistributorId, intParentId, strStyleClass, strClassPath, intShowLen, strAppUrl));
            return strReturn;
        }
        /// <summary>
        /// 取栏目列表(递归)
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        public StringBuilder GetClassList(SqlTransaction trans,string DistributorId, int intParentId, string strStyleClass, string strClassPath, int intLevel, int intShowLen, string strAppUrl)
        {
            return claDAL.GetClassList(trans,DistributorId, intParentId, strStyleClass, strClassPath, intLevel, intShowLen, strAppUrl);
        }
        #endregion

        #region 取底部导航菜单
        /// <summary>
        /// 取底部导航菜单(dl dt dd-递归1次)
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        public StringBuilder GetFootMenu(SqlTransaction trans,string DistributorId, int intParentId, int intLevel, int intShowLen, string strAppUrl)
        {
            return claDAL.GetFootMenu(trans,DistributorId, intParentId, intLevel, intShowLen, strAppUrl);
        }
        #endregion

        #region 前台读取信息
        /// <summary>
        /// 前台读取信息
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        public ClassModel GetInfo2(SqlTransaction trans, int intClassId, string DistributorId)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and IsClose=0");
            SqlQuery.Append(" and ClassId=@ClassId");
            SqlQuery.Append(" and DistributorId=@DistributorId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@ClassId", intClassId));
            listParams.Add(new SqlParameter("@DistributorId", DistributorId));
            return claDAL.GetModel(trans, SqlQuery, listParams);
        }
        #endregion

        #region +前台从缓存读取信息
        /// <summary>
        /// 前台从缓存读取信息
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        public ClassModel GetCacheInfo2(SqlTransaction trans, int intClassId, string DistributorId)
        {
            string key = "Cache_Class_Model_" + intClassId;
            object value = CacheHelper.GetCache(key);
            if (value != null)
                return (ClassModel)value;
            else
            {
                ClassModel claModel = GetInfo2(trans, intClassId, DistributorId);
                CacheHelper.AddCache(key, claModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return claModel;
            }
        }
        #endregion

        #region 取第一个类别ID
        /// <summary>
        /// 取第一个类别ID
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        public int GetFirstClassId(SqlTransaction trans, int intParentId, string DistributorId)
        {
            return claDAL.GetFirstClassId(trans, intParentId, DistributorId);
        }
        #endregion

        #region +根据栏目路径取ClassId
        /// <summary>
        /// 根据栏目路径取ClassId
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public int GetClassIdByPath(string strClassPath, int index, int intClassId)
        {
            string[] ArrClassPath = strClassPath.Split(new char[] { ',' });
            if (ArrClassPath.Length > index + 1)
            {
                return Convert.ToInt32(ArrClassPath[index]);
            }
            else
            {
                return intClassId;
            }
        }
        #endregion

        #region +根据栏目路径取栏目层级
        /// <summary>
        /// 根据栏目路径取栏目层级
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public int GetPosByPath(string strClassPath, int intClassId)
        {
            string[] ArrClassPath = strClassPath.Split(new char[] { ',' });
            return ArrClassPath.Length;
        }
        #endregion

        #region 根据栏目路径取位置导航
        /// <summary>
        /// 根据栏目路径取位置导航
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        public StringBuilder GetClassNav(SqlTransaction trans, string DistributorId, string strClassPath, int intDepth, string strNavStr, string strAppUrl)
        {
            return claDAL.GetClassNav(trans, DistributorId, strClassPath, intDepth, strNavStr, strAppUrl);
        }
        #endregion

        #region +根据栏目路径取当前栏目
        /// <summary>
        /// 根据栏目路径取当前栏目
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        public StringBuilder GetClassBlock(string strClassPath, int intDepth)
        {
            StringBuilder strScript = new StringBuilder();
            strScript.Append("<script language=\"javascript\">\n");
            strScript.Append("function ShowClassBlock(){\n");
            strScript.Append("try{\n");
            string[] ArrClassPath = strClassPath.Split(new char[] { ',' });
            for (int i = intDepth; i < ArrClassPath.Length; i++)
            {
                strScript.Append("document.getElementById(\"Class" + ArrClassPath[i] + "\").style.display=\"block\";\n");
            }
            strScript.Append("}\n");
            strScript.Append("catch(e){\n");
            strScript.Append("//do nothing\n");
            strScript.Append("}\n");
            strScript.Append("}\n");
            strScript.Append("ShowClassBlock();\n");
            strScript.Append("</script>\n");
            return strScript;
        }
        #endregion

        #region 取Banner图片
        /// <summary>
        /// 取Banner图片(递归)
        /// Author:Jimy
        /// Create Time:2015-07-17 11:08:23
        /// </summary>
        public string GetBannerPic(SqlTransaction trans, int intClassId, string DistributorId)
        {
            return claDAL.GetBannerPic(trans, intClassId, DistributorId);
        }
        #endregion

        #region 取栏目访问链接
        /// <summary>
        /// 取栏目访问链接
        /// Author:Jimy
        /// Create Time:2015-07-17 15:26:11
        /// </summary>
        public string GetClassUrl(SqlTransaction trans, string DistributorId,int intClassId, string strAppUrl)
        {
            return claDAL.GetClassUrl(trans,DistributorId, intClassId, strAppUrl);
        }
        #endregion

        #region 取商品分类列表
        /// <summary>
        /// 取商品分类列表
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        public StringBuilder GetProductClassList(SqlTransaction trans, int intParentId, string strStyleClass, int intClassId, int intShowLen, string strAppUrl)
        {
            return claDAL.GetProductClassList(trans, intParentId, strStyleClass, intClassId, intShowLen, strAppUrl);
        }
        public StringBuilder GetProductUseClassList(SqlTransaction trans, int intParentId, string strStyleClass, int intClassId, int intShowLen, string strAppUrl)
        {
            return claDAL.GetProductUseClassList(trans, intParentId, strStyleClass, intClassId, intShowLen, strAppUrl);
        }

        public StringBuilder GetProductSubClassList(SqlTransaction trans, int intParentId, int intShowLen, string strAppUrl)
        {
            return claDAL.GetProductSubClassList(trans, intParentId, intShowLen, strAppUrl);
        }

        public StringBuilder GetProductSecClassList(SqlTransaction trans, int intParentId, string strStyleClass, int intClassId, int intShowLen, string strAppUrl)
        {
            return claDAL.GetProductSecClassList(trans, intParentId, strStyleClass, intClassId, intShowLen, strAppUrl);
        }
        #endregion

        #region 下拉列表
        /// <summary>
        /// 下拉列表
        /// </summary>
        public List<SelectListItem> GetSelectList(SqlTransaction trans, int ParentId)
        {
            DataTable dt = GetDataTable(trans, ParentId, 0);
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new SelectListItem() { Text = dr["ClassName"].ToString(), Value = dr["ClassId"].ToString() });
            }
            return list;
        }

        /// <summary>
        /// 下拉列表
        /// </summary>
        public List<SelectListItem> GetSelectList(SqlTransaction trans, int ParentId, int ClassPropertyId, string ClassPath)
        {
            ClassPath = "," + ClassPath + ",";
            DataTable dt = GetDataTableForSelectList(trans, ParentId, ClassPropertyId);
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (DataRow dr in dt.Rows)
            {
                bool IsSel = ClassPath.Contains("," + dr["ClassId"].ToString() + ",");
                list.Add(new SelectListItem() { Text = dr["ClassName"].ToString(), Value = dr["ClassId"].ToString(), Selected = IsSel });
            }
            return list;
        }
        #endregion

        #region 无限级下拉
        /// <summary>
        /// 无限级下拉
        /// </summary>
        public StringBuilder GetUnlimitedSelect(SqlTransaction trans, int ParentId, int ClassPropertyId)
        {
            DataTable dt = GetDataTable(trans, ParentId, ClassPropertyId);
            StringBuilder list = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                list.Append("{");
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    i++;
                    list.Append("\"" + dr["ClassId"] + "\"" + ":{\"name\":\"" + dr["ClassName"] + "\"}");
                    if (i < dt.Rows.Count)
                        list.Append(",");
                }
                list.Append("}");
            }
            return list;
        }

        /// <summary>
        /// 无限级下拉
        /// </summary>
        public StringBuilder GetUnlimitedSelectAll(SqlTransaction trans, int intParentId, int intLevel, string strSql)
        {
            StringBuilder tempList = new StringBuilder();
            tempList.Append(claDAL.GetUnlimitedSelectAll(trans, intParentId, intLevel, strSql));
            return tempList;
        }
        #endregion

        #region zTree
        /// <summary>
        /// zTree
        /// </summary>
        public List<object> GetClassForzTree(SqlTransaction trans, string strSql)
        {
            return claDAL.GetClassForzTree(trans, strSql);
        }
        #endregion

        #region 上级分类关闭，其下级不显示
        public List<object> GetClassForzTreeByOpen(SqlTransaction trans, string strSql, int intParentId)
        {
            return claDAL.GetClassForzTreeByOpen(trans, strSql, intParentId);
        }
        #endregion

        #region 取字段值
        /// <summary>
        /// 取取字段值
        /// </summary>
        public string GetValueByField(SqlTransaction trans, string strFieldName,int ClassId, string DistributorId)
        {
            StringBuilder LeftJoin = new StringBuilder();
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and ClassId=@ClassId");
            SqlQuery.Append(" and DistributorId=@DistributorId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@ClassId", ClassId));
            listParams.Add(new SqlParameter("@DistributorId", DistributorId));
            string FieldShow = "a." + strFieldName;
            string FieldOrder = "a.ClassId asc";
            DataTable dt = claDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
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

