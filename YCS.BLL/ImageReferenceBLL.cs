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
    /// 圖片路徑表 -业务逻辑类
    /// 创建人:楊小明
    /// 日期:2017/2/18 15:06:08
    /// </summary>

    public class ImageReferenceBLL : Base.ImageReference
    {

        private readonly ImageReferenceDAL imaDAL = new ImageReferenceDAL();

        #region 取信息分页列表
        /// <summary>
        /// 取信息分页列表
        /// </summary>
        public DataTable GetInfoPageList(SqlTransaction trans, Hashtable hs, PageHelper p, out StringBuilder PageStr)
        {
            return imaDAL.GetInfoPageList(trans, hs, p, out PageStr);
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
            return imaDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
        }
        #endregion

        #region 取列表图片集合
        /// <summary>
        /// 取列表图片集合
        /// </summary>
        public List<string> GetProductSpuImage(SqlTransaction trans,string ClassId,int Type,int TopNum)
        {
            List<string> listImage = new List<string>();
            StringBuilder LeftJoin = new StringBuilder();
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and ClassId=@ClassId");
            SqlQuery.Append(" and Type=@Type");
            //SqlQuery.Append(" and Status=@Status");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@ClassId", ClassId));
            listParams.Add(new SqlParameter("@Type", Type));
            //listParams.Add(new SqlParameter("@Status", EnumList.OpenStatus.Open.ToInt()));
            string FieldShow = (TopNum > 0 ? " top " + TopNum : "") + "a.PicThumb";
            string FieldOrder = "a.PicOrder asc";
            DataTable dt= imaDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
            foreach (DataRow dr in dt.Rows)
            {
                listImage.Add(dr["PicThumb"].ToString());
            }
            return listImage;
        }
        #endregion

        #region 取实体集合
        /// <summary>
        /// 取实体集合
        /// </summary>
        public List<ImageReferenceModel> GetModels(SqlTransaction trans, string ClassId, int Type, int TopNum)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and ClassId=@ClassId");
            SqlQuery.Append(" and Type=@Type");
            SqlQuery.Append(" and Status=@Status");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@ClassId", ClassId));
            listParams.Add(new SqlParameter("@Type", Type));
            listParams.Add(new SqlParameter("@Status", EnumList.CloseStatus.Open.ToInt()));
            string FieldOrder = "PicOrder asc";
            return imaDAL.GetModels(trans, SqlQuery, listParams, TopNum, FieldOrder);
        }

        /// <summary>
        /// 取实体集合
        /// </summary>
        public List<ImageReferenceModel> GetModelsByProductSpuId(SqlTransaction trans, string ClassId)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and ClassId=@ClassId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@ClassId", ClassId));
            string FieldOrder = "PicOrder asc";
            return imaDAL.GetModels(trans, SqlQuery, listParams,0, FieldOrder);
        }
        #endregion

        #region 取实体
        /// <summary>
        /// 取实体
        /// </summary>
        public ImageReferenceModel GetModel(SqlTransaction trans, string ClassId, int Type, int PicOrder)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and ClassId=@ClassId");
            SqlQuery.Append(" and Type=@Type");
            SqlQuery.Append(" and PicOrder=@PicOrder");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@ClassId", ClassId));
            listParams.Add(new SqlParameter("@Type", Type));
            listParams.Add(new SqlParameter("@PicOrder", PicOrder));
            return imaDAL.GetModel(trans, SqlQuery, listParams);
        }
        public ImageReferenceModel GetModel(SqlTransaction trans, string ClassId, int Type, string PicOrig)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and ClassId=@ClassId");
            SqlQuery.Append(" and Type=@Type");
            SqlQuery.Append(" and PicOrig=@PicOrig");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@ClassId", ClassId));
            listParams.Add(new SqlParameter("@Type", Type));
            listParams.Add(new SqlParameter("@PicOrig", PicOrig));
            return imaDAL.GetModel(trans, SqlQuery, listParams);
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
            return imaDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams);
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
            return imaDAL.GetAllSum(trans, LeftJoin, SqlQuery, listParams, "a.ClassId");
        }
        #endregion
        #region 读取排序号
        /// <summary>
        /// 读取排序号
        /// </summary>
        public int GetListID(SqlTransaction trans, string intClassId, int type)
        {
            return imaDAL.GetPicOrder(trans, intClassId, type);
        }
        #endregion

        #region 排序信息
        /// <summary>
        /// 排序信息
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(SqlTransaction trans, string intClassId, int type, int intListID, int intOldListID)
        {
            imaDAL.OrderInfo(trans, intClassId,type, intListID, intOldListID);
        }
        #endregion
        #region 根据classId和Type删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public int DeleteInfo(SqlTransaction trans, string ClassId, int Type)
        {
            int result = 0;
            List<ImageReferenceModel> imageList = GetModels(trans, ClassId, Type, 0);
            if (imageList != null && imageList.Count > 0)
            {
                foreach (ImageReferenceModel img in imageList)
                {
                    string key = "Cache_ImageReference_Model_" + img.SN;
                    CacheHelper.RemoveCache(key);
                    result += imaDAL.DeleteInfo(trans, img.SN);
                }
            }
            return result;
        }
        #endregion
    }
}
