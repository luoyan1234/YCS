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
    /// 產品分類表-业务逻辑类
    /// 创建人:楊小明
    /// 日期:2017/2/18 15:06:09
    /// </summary>

    public class ProductCategorySettingBLL : Base.ProductCategorySetting
    {

        private readonly ProductCategorySettingDAL proDAL = new ProductCategorySettingDAL();

        #region 取信息分页列表
        /// <summary>
        /// 取信息分页列表
        /// </summary>
        public DataTable GetInfoPageList(SqlTransaction trans, Hashtable hs, PageHelper p, out StringBuilder PageStr)
        {
            return proDAL.GetInfoPageList(trans, hs, p, out PageStr);
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
            string FieldOrder = "a.SeqNo asc";
            return proDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
        }
        /// <summary>
        /// 取DataTable
        /// </summary>
        public DataTable GetDataTable(SqlTransaction trans, string ParentKey)
        {
            StringBuilder LeftJoin = new StringBuilder();
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and ParentKey=@ParentKey");
            SqlQuery.Append(" and Status=@Status");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@ParentKey", ParentKey));
            listParams.Add(new SqlParameter("@Status", EnumList.OpenStatus.Open.ToInt()));
            string FieldShow = "a.*";
            string FieldOrder = "a.ProductCategoryId asc";
            return proDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
        }
        /// <summary>
        /// 取DataTable
        /// </summary>
        public DataTable GetDataProductCategoryTable(SqlTransaction trans, string ParentKey)
        {
            StringBuilder LeftJoin = new StringBuilder();
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and ParentKey=@ParentKey");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@ParentKey", ParentKey));
            string FieldShow = "a.*";
            string FieldOrder = "a.ProductCategoryId asc";
            return proDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
        }
        #endregion

        #region 取实体集合
        /// <summary>
        /// 取实体集合
        /// </summary>
        public List<ProductCategorySettingModel> GetModels(SqlTransaction trans)
        {
            StringBuilder SqlQuery = new StringBuilder();
            List<SqlParameter> listParams = new List<SqlParameter>();
            string FieldOrder = "ProductCategoryId asc";
            return proDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
        }
        #endregion

        #region 取实体
        /// <summary>
        /// 取实体
        /// </summary>
        public ProductCategorySettingModel GetModel(SqlTransaction trans, string ProductCategoryId)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and ProductCategoryId=@ProductCategoryId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@ProductCategoryId", ProductCategoryId));
            return proDAL.GetModel(trans, SqlQuery, listParams);
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
            return proDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams);
        }
        /// <summary>
        /// 取子类别数
        /// </summary>
        public int GetAllCount(SqlTransaction trans,string ParentKey)
        {
            StringBuilder LeftJoin = new StringBuilder();
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and ParentKey=@ParentKey");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@ParentKey", ParentKey));
            return proDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams);
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
            return proDAL.GetAllSum(trans, LeftJoin, SqlQuery, listParams, "a.ProductCategoryId");
        }
        #endregion

        #region 取商品分类列表
        /// <summary>
        /// 取商品分类列表(含二级)
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        public StringBuilder GetProductCategoryList(SqlTransaction trans,string DistributorId, string ParentKey, string strAppUrl)
        {
            return proDAL.GetProductCategoryList(trans,DistributorId, ParentKey, strAppUrl);
        }
        /// <summary>
        /// 取商品分类列表(含三级)
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        public StringBuilder GetProductCategoryListForThirdLevel(SqlTransaction trans, string DistributorId, string ParentKey, string strAppUrl)
        {
            return proDAL.GetProductCategoryListForThirdLevel(trans, DistributorId, ParentKey, strAppUrl);
        }
        #endregion
        #region 获取商品分类的children
        /// <summary>
        /// 获取商品分类的children
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="parentKey">父级</param>
        /// <param name="IncludSelf">是否包含本身</param>
        /// <returns></returns>
        public List<ProductCategorySettingModel> GetChildrenModels(SqlTransaction trans, string DistributorId, string parentKey,bool IncludSelf)
        {
            List<ProductCategorySettingModel> list = new List<ProductCategorySettingModel>();
            list.AddRange(proDAL.GetChildrenModels(trans, DistributorId, parentKey));
            if (IncludSelf)
            {
                list.Add(GetModel(null, parentKey));
            }
            return list;
        }
        #endregion
        #region 取字段值
        /// <summary>
        /// 取取字段值
        /// </summary>
        public string GetValueByField(SqlTransaction trans, string strFieldName, string ProductCategoryId)
        {
            StringBuilder LeftJoin = new StringBuilder();
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and ProductCategoryId=@ProductCategoryId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@ProductCategoryId", ProductCategoryId));
            string FieldShow = "a." + strFieldName;
            string FieldOrder = "a.SN asc";
            DataTable dt = proDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
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

        #region 读取排序号
        /// <summary>
        /// 读取排序号
        /// </summary>
        public int GetSeqNo(SqlTransaction trans, string ParentKey)
        {
            return proDAL.GetSeqNo(trans, ParentKey);
        }
        #endregion

        #region 排序信息
        /// <summary>
        /// 排序信息
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(SqlTransaction trans, string ParentKey, int intSeqNo, int intOldSeqNo)
        {
            proDAL.OrderInfo(trans, ParentKey, intSeqNo, intOldSeqNo);
        }
        #endregion

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(SqlTransaction trans,string DistributorId, string ParentKey, string strFieldName, string strFieldValue, int intSN)
        {
            return proDAL.CheckInfo(trans,DistributorId, ParentKey, strFieldName, strFieldValue, intSN);
        }
        #endregion

        #region 显示下拉树形列表
        /// <summary>
        /// 显示下拉树形列表
        /// </summary>
        public List<SelectListItem> GetSelectTreeList(SqlTransaction trans, string ParentKey, int intLevel, string strSql)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            return proDAL.GetSelectTreeList(trans, ParentKey, intLevel, list, strSql);
        }
        /// <summary>
        /// 显示下拉树形列表
        /// </summary>
        public List<SelectListItem> GetSelectTreeList(SqlTransaction trans, string ParentKey, int intLevel, List<SelectListItem> list, string strSql)
        {
            return proDAL.GetSelectTreeList(trans, ParentKey, intLevel, list, strSql);
        }
        #endregion

        #region 读取地区名称
        /// <summary>
        /// 读取地区名称
        /// </summary>
        public string GetProductCategorySettingNames(SqlTransaction trans, string strProductCategoryIds)
        {
            return proDAL.GetProductCategorySettingNames(trans, strProductCategoryIds);
        }
        #endregion

        #region 取路径
        /// <summary>
        /// 取路径
        /// </summary>
        public StringBuilder GetPath(SqlTransaction trans, string ProductCategoryId)
        {
            return proDAL.GetPath(trans, ProductCategoryId);
        }
        #endregion
        #region 导航栏显示
        /// <summary>
        /// 导航栏显示
        /// </summary>
        public StringBuilder ShowNav(SqlTransaction trans, string ProductCategoryId)
        {
            return proDAL.ShowNav(trans, ProductCategoryId);
        }
        #endregion
        #region 显示路径
        /// <summary>
        /// 显示路径
        /// </summary>
        public StringBuilder ShowPath(SqlTransaction trans, string ProductCategoryId)
        {
            StringBuilder tempStr = new StringBuilder("根结点");
            ProductCategorySettingModel proModel = new ProductCategorySettingModel();
            proModel = this.GetModel(trans, ProductCategoryId);
            if (proModel == null)
            {
                return tempStr;
            }
            else
            {
                string strPath = proDAL.GetPath(trans, ProductCategoryId).ToString();
                string[] arrPath = strPath.Split(',');
                foreach (var item in arrPath)
                {
                    ProductCategorySettingModel areModel_2 = new ProductCategorySettingModel();
                    areModel_2 = this.GetModel(trans, item);
                    if (areModel_2 != null)
                    {
                        tempStr.Append(" > " + areModel_2.ProductCategoryName);
                    }
                }
                return tempStr;
            }
        }
        #endregion

        #region 取属性模板中的分类
        /// <summary>
        /// 取属性模板中的分类
        /// </summary>
        public string GetProductCategorys(SqlTransaction trans, string ProductTemplateId)
        {
            StringBuilder LeftJoin = new StringBuilder();
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and exists(select ProductCategoryId from ProductTemplateRelationSetting where ProductCategoryId=a.ProductCategoryId and ProductTemplateId=@ProductTemplateId)");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@ProductTemplateId", ProductTemplateId));
            string FieldShow = "a.ProductCategoryName";
            string FieldOrder = "a.SN asc";
            DataTable dt = proDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
            StringBuilder strReturn = new StringBuilder();
            foreach (DataRow dr in dt.Rows)
            {
                strReturn.Append(dr["ProductCategoryName"].ToString() + "、");
            }
            return strReturn.ToString().TrimEnd('、');
        }
        #endregion

        #region zTree
        /// <summary>
        /// zTree
        /// </summary>
        public List<object> GetProductCategoryForzTree(SqlTransaction trans, string strSql)
        {
            return proDAL.GetProductCategoryForzTree(trans, strSql);
        }
        #endregion

        #region 无限级下拉
        /// <summary>
        /// 无限级下拉
        /// </summary>
        public StringBuilder GetUnlimitedSelect(SqlTransaction trans, string ParentKey)
        {
            DataTable dt = GetDataTable(trans, ParentKey);
            StringBuilder list = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                list.Append("{");
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    i++;
                    list.Append("\"" + dr["ProductCategoryId"] + "\"" + ":{\"name\":\"" + dr["ProductCategoryName"] + "\"}");
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
        public StringBuilder GetUnlimitedSelectAll(SqlTransaction trans, string ParentKey, int intLevel, string strSql)
        {
            StringBuilder tempList = new StringBuilder();
            tempList.Append(proDAL.GetUnlimitedSelectAll(trans, ParentKey, intLevel, strSql));
            return tempList;
        }
        #endregion

        #region 修改状态
        /// <summary>
        /// 更新Status
        /// </summary>
        public int UpdateInfo(SqlTransaction trans, ProductCategorySettingModel proCateModel)
        {
            return proDAL.UpdateInfo(trans, proCateModel);
        }
        #endregion


    }
}
