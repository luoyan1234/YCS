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
    /// 產品資料表 -业务逻辑类
    /// 创建人:楊小明
    /// 日期:2017/2/18 15:06:09
    /// </summary>

    public class ProductSpuBLL : Base.ProductSpu
    {

        private readonly ProductSpuDAL proDAL = new ProductSpuDAL();

        #region 取信息分页列表
        /// <summary>
        /// 取信息分页列表
        /// </summary>
        public DataTable GetInfoPageList(SqlTransaction trans, Hashtable hs, PageHelper p, out StringBuilder PageStr)
        {
            return proDAL.GetInfoPageList(trans, hs, p, out PageStr);
        }
        /// <summary>
        /// 取信息分页列表（后台）
        /// </summary>
        public DataTable GetInfoPageListForAdmin(SqlTransaction trans, Hashtable hs, PageHelper p, out StringBuilder PageStr)
        {
            return proDAL.GetInfoPageListForAdmin(trans, hs, p, out PageStr);
        }
        /// <summary>
        /// 取信息分页列表（首页设置）
        /// </summary>
        /// <param name="FloorProductType">首页设置的商品分类</param>
        /// <param name="FloorId">楼层编码</param>
        /// <param name="Category">分类</param>
        /// <param name="DistributorId">经销商</param>
        /// <param name="trans"></param>
        /// <param name="hs"></param>
        /// <param name="p"></param>
        /// <param name="PageStr"></param>
        /// <returns></returns>
        public DataTable GetInfoPageListForFloor(int FloorProductType, int FloorId, string Category, string DistributorId, SqlTransaction trans, Hashtable hs, PageHelper p, out StringBuilder PageStr)
        {
            if (!string.IsNullOrEmpty(Category))
            {
                return proDAL.GetInfoPageListForFloor(FloorId, FloorProductType, Category, DistributorId, trans, hs, p, out PageStr);
            }
            else
            {
                //根据楼层编码获取有权限的分类
                List<FloorProductCategoryModel> CategoryModel = Factory.FloorProductCategory().GetModels(null, FloorId, false);
                if (CategoryModel != null && CategoryModel.Count > 0)
                {
                    string categoryStr = String.Join("','", CategoryModel.Select(c => c.ProductCategoryId).ToArray());
                    return proDAL.GetInfoPageListForFloor(FloorId, FloorProductType, categoryStr, DistributorId, trans, hs, p, out PageStr);
                }
                else
                {
                    PageStr = null;
                    return null;
                }
            }
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
            string FieldOrder = "a.ProductSpuId asc";
            return proDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
        }
        /// <summary>
        /// 取产品列表 
        /// </summary>
        public DataTable GetProduct(SqlTransaction trans, string ProductCategoryId)
        {
            StringBuilder LeftJoin = new StringBuilder();
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and a.IsDelete=@IsDelete");
            SqlQuery.Append(" and exists(select ProductSpuId from ProductRelationSetting where ProductCategoryId in(select id from f_GetChildCategory(@ProductCategoryId)) and ProductSpuId=a.ProductSpuId)");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@IsDelete", EnumList.CloseStatus.Open.ToInt()));
            listParams.Add(new SqlParameter("@ProductCategoryId", ProductCategoryId));
            string FieldShow = "a.ProductSpuId,a.ProductSpuName";
            string FieldOrder = "a.ProductSpuId asc";
            return proDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
        }
        #endregion

        #region 取实体集合
        /// <summary>
        /// 取实体集合
        /// </summary>
        public List<ProductSpuModel> GetModels(SqlTransaction trans)
        {
            StringBuilder SqlQuery = new StringBuilder();
            List<SqlParameter> listParams = new List<SqlParameter>();
            string FieldOrder = "ProductSpuId asc";
            return proDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
        }
        #endregion

        #region 取实体
        /// <summary>
        /// 取实体
        /// </summary>
        public ProductSpuModel GetModel(SqlTransaction trans, string ProductSpuId)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and ProductSpuId=@ProductSpuId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@ProductSpuId", ProductSpuId));
            return proDAL.GetModel(trans, SqlQuery, listParams);
        }
        /// <summary>
        /// 取实体(前台,带限制条件)
        /// </summary>
        public ProductSpuModel GetModel2(SqlTransaction trans, string ProductSpuId)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and ProductSpuId=@ProductSpuId");
            SqlQuery.Append(" and Status=@Status");
            SqlQuery.Append(" and IsDelete=@IsDelete");
            SqlQuery.Append(" and OnShelfDate<=@now");
            SqlQuery.Append(" and OffShelfDate>=@now");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@ProductSpuId", ProductSpuId));
            listParams.Add(new SqlParameter("@Status", EnumList.ProductSpuStatus.上架.ToInt()));
            listParams.Add(new SqlParameter("@IsDelete", EnumList.CloseStatus.Open.ToInt()));
            listParams.Add(new SqlParameter("@now", DateTimeOffset.Now));
            return proDAL.GetModel(trans, SqlQuery, listParams);
        }

        /// <summary>
        /// 从缓存读取信息
        /// </summary>
        public ProductSpuModel GetCacheModel(SqlTransaction trans, string ProductSpuId)
        {
            string key = "Cache_ProductSpu_Model_" + ProductSpuId;
            object value = CacheHelper.GetCache(key);
            if (value != null)
                return (ProductSpuModel)value;
            else
            {
                ProductSpuModel proSpuModel = GetModel2(trans, ProductSpuId);
                CacheHelper.AddCache(key, proSpuModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return proSpuModel;
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
            return proDAL.GetAllSum(trans, LeftJoin, SqlQuery, listParams, "a.ProductSpuId");
        }
        #endregion

        #region 取编号
        /// <summary>
        /// 取编号
        /// </summary>
        public string GetNo(SqlTransaction trans)
        {
            StringBuilder strNo = new StringBuilder("SPU");
            strNo.Append(DateTime.Now.ToString("yyyyMMdd"));

            StringBuilder LeftJoin = new StringBuilder();
            StringBuilder SqlQuery = new StringBuilder();
            List<SqlParameter> listParams = new List<SqlParameter>();
            string FieldShow = "top 1 a.ProductSpuId";
            string FieldOrder = "a.SN desc";
            DataTable dt = proDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
            if (dt.Rows.Count > 0)
            {
                string strTemp = dt.Rows[0][0].ToString();
                int len = strTemp.Length;
                int baseNo;
                if (len < 15)
                {
                    baseNo = 1;
                }
                else
                {
                    string strBaseNo = strTemp.Substring(11, 4);
                    if (int.TryParse(strBaseNo, out baseNo))
                    {
                        baseNo += 1;
                    }
                    else
                    {
                        baseNo = 1;
                    }
                }
                strNo.Append(Config.ShowZero(baseNo, 4));
            }
            else
            {
                strNo.Append("0001");
            }
            return strNo.ToString();
        }
        #endregion
    }
}
