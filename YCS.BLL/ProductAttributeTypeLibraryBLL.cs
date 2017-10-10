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
    /// 商品屬性類別表-业务逻辑类
    /// 创建人:杨小明
    /// 日期:2017/3/13 10:18:02 +08:00
    /// </summary>

    public class ProductAttributeTypeLibraryBLL : Base.ProductAttributeTypeLibrary
    {

        private readonly ProductAttributeTypeLibraryDAL proDAL = new ProductAttributeTypeLibraryDAL();

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
        public DataTable GetDataTable(SqlTransaction trans, Hashtable hs)
        {
            StringBuilder LeftJoin = new StringBuilder();
            StringBuilder SqlQuery = new StringBuilder();
            List<SqlParameter> listParams = new List<SqlParameter>();
            #region 查询条件
            if (hs.Contains("ProductAttributeTypeId"))
            {
                SqlQuery.Append(" and a.ProductAttributeTypeId like @ProductAttributeTypeId");
                listParams.Add(new SqlParameter("@ProductAttributeTypeId", "%" + hs["ProductAttributeTypeId"] + "%"));
            }
            if (hs.Contains("ProductAttributeTypeName"))
            {
                SqlQuery.Append(" and a.ProductAttributeTypeName like @ProductAttributeTypeName");
                listParams.Add(new SqlParameter("@ProductAttributeTypeName", "%" + hs["ProductAttributeTypeName"] + "%"));
            }
            if (hs.Contains("HavePicture"))
            {
                SqlQuery.Append(" and a.HavePicture = @HavePicture");
                listParams.Add(new SqlParameter("@HavePicture", hs["HavePicture"]));
            }
            if (hs.Contains("SeqNo"))
            {
                SqlQuery.Append(" and a.SeqNo = @SeqNo");
                listParams.Add(new SqlParameter("@SeqNo", hs["SeqNo"]));
            }
            if (hs.Contains("Status"))
            {
                SqlQuery.Append(" and a.Status = @Status");
                listParams.Add(new SqlParameter("@Status", hs["Status"]));
            }
            if (hs.Contains("CreationDate"))
            {
                SqlQuery.Append(" and a.CreationDate = @CreationDate");
                listParams.Add(new SqlParameter("@CreationDate", hs["CreationDate"]));
            }
            if (hs.Contains("LastUpdateDate"))
            {
                SqlQuery.Append(" and a.LastUpdateDate = @LastUpdateDate");
                listParams.Add(new SqlParameter("@LastUpdateDate", hs["LastUpdateDate"]));
            }
            #endregion
            string FieldShow = "a.*";
            string FieldOrder = "a.SN asc";
            return proDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
        }
        #endregion

        #region 取实体集合
        /// <summary>
        /// 取实体集合
        /// </summary>
        public List<ProductAttributeTypeLibraryModel> GetModels(SqlTransaction trans)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and Status=@Status");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@Status", EnumList.CloseStatus.Open.ToInt()));
            string FieldOrder = "SN asc";
            return proDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
        }
        /// <summary>
        /// 取实体集合
        /// </summary>
        public List<ProductAttributeTypeLibraryModel> GetModels(SqlTransaction trans, string strTypeSNs)
        {
            if (string.IsNullOrEmpty(strTypeSNs))
            {
                strTypeSNs = "-1,-1";
            }
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and Status=@Status");
            SqlQuery.Append(" and SN in(" + strTypeSNs + ")");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@Status", EnumList.CloseStatus.Open.ToInt()));
            string FieldOrder = "SeqNo asc";
            return proDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
        }
        #endregion

        #region 取实体
        /// <summary>
        /// 取实体
        /// </summary>
        public ProductAttributeTypeLibraryModel GetModel(SqlTransaction trans, int SN)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and SN=@SN");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@SN", SN));
            return proDAL.GetModel(trans, SqlQuery, listParams);
        }
        /// <summary>
        /// 取实体
        /// </summary>
        public ProductAttributeTypeLibraryModel GetModel(SqlTransaction trans, string ProductAttributeTypeId)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and ProductAttributeTypeId=@ProductAttributeTypeId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@ProductAttributeTypeId", ProductAttributeTypeId));
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
            return proDAL.GetAllSum(trans, LeftJoin, SqlQuery, listParams, "a.SN");
        }
        #endregion
        #region 读取排序号
        /// <summary>
        /// 读取排序号
        /// </summary>
        public int GetSeqNo(SqlTransaction trans)
        {
            return proDAL.GetSeqNo(trans);
        }
        #endregion

        #region 排序信息
        /// <summary>
        /// 排序信息
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(SqlTransaction trans, int intSeqNo, int intOldSeqNo)
        {
            proDAL.OrderInfo(trans, intSeqNo, intOldSeqNo);
        }
        #endregion

        #region 下拉列表
        /// <summary>
        /// 下拉列表
        /// </summary>
        public List<SelectListItem> GetSelectList(SqlTransaction trans)
        {
            List<ProductAttributeTypeLibraryModel> delModels = GetModels(trans);
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (ProductAttributeTypeLibraryModel delModel in delModels)
            {
                list.Add(new SelectListItem() { Text = delModel.ProductAttributeTypeId, Value = delModel.ProductAttributeTypeName.ToString() });
            }
            return list;
        }
        #endregion

        #region 取编号
        /// <summary>
        /// 取编号
        /// </summary>
        public string GetNo(SqlTransaction trans)
        {
            StringBuilder strNo = new StringBuilder("SXFL");
            strNo.Append(DateTime.Now.ToString("yyyyMMdd"));

            StringBuilder LeftJoin = new StringBuilder();
            StringBuilder SqlQuery = new StringBuilder();
            List<SqlParameter> listParams = new List<SqlParameter>();
            string FieldShow = "top 1 a.ProductAttributeTypeId";
            string FieldOrder = "a.SN desc";
            DataTable dt = proDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
            if (dt.Rows.Count > 0)
            {
                string strTemp = dt.Rows[0][0].ToString();
                int len = strTemp.Length;
                int baseNo;
                if (len < 16)
                {
                    baseNo = 1;
                }
                else
                {
                    string strBaseNo = strTemp.Substring(12, 4);
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
