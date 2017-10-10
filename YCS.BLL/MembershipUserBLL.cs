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
    /// 會員-业务逻辑类
    /// 创建人:楊小明
    /// 日期:2017/2/18 15:06:08
    /// </summary>

    public class MembershipUserBLL : Base.MembershipUser
    {

        private readonly MembershipUserDAL memDAL = new MembershipUserDAL();
        private readonly MembershipProfileDAL profileDAL = new MembershipProfileDAL();

        #region 取信息分页列表
        /// <summary>
        /// 取信息分页列表
        /// </summary>
        public DataTable GetInfoPageList(SqlTransaction trans, Hashtable hs, PageHelper p, out StringBuilder PageStr)
        {
            return memDAL.GetInfoPageList(trans, hs, p, out PageStr);
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
            string FieldOrder = "a.DistributorId asc";
            return memDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
        }
        #endregion

        #region 取实体集合
        /// <summary>
        /// 取实体集合
        /// </summary>
        public List<MembershipUserModel> GetModels(SqlTransaction trans)
        {
            StringBuilder SqlQuery = new StringBuilder();
            List<SqlParameter> listParams = new List<SqlParameter>();
            string FieldOrder = "DistributorId asc";
            return memDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
        }
        #endregion

        #region 取实体
        /// <summary>
        /// 取实体
        /// </summary>
        public MembershipUserModel GetModel(SqlTransaction trans, long MemberId)
        {
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and MemberId=@MemberId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@MemberId", MemberId));
            return memDAL.GetModel(trans, SqlQuery, listParams);
        }
        /// <summary>
        /// 取实体(用户名/手机/E-mail)
        /// </summary>
        public MembershipUserModel GetModelByLoginName(SqlTransaction trans, string DistributorId, string strLoginName, bool IsAll)
        {
            StringBuilder SqlQuery = new StringBuilder();
            List<SqlParameter> listParams = new List<SqlParameter>();
            if (!IsAll)
            {
                SqlQuery.Append(" and Status=@Status");
                listParams.Add(new SqlParameter("@Status", EnumList.MemberStatus.启用.ToInt()));
            }
            SqlQuery.Append(" and DistributorId=@DistributorId");
            listParams.Add(new SqlParameter("@DistributorId", DistributorId));
            if (strLoginName.IsEmail())
            {
                SqlQuery.Append(" and PrimaryEmail=@LoginName");
            }
            else if (strLoginName.IsMobile())
            {
                SqlQuery.Append(" and PrimaryCellPhone=@LoginName");
            }
            else
            {
                SqlQuery.Append(" and UserName=@LoginName");
            }
            listParams.Add(new SqlParameter("@LoginName", strLoginName));
            return memDAL.GetModel(trans, SqlQuery, listParams);
        }

        /// <summary>
        /// 取实体(用户名/手机/E-mail)(找回密码)
        /// </summary>
        public MembershipUserModel GetModelByLoginNameForFindPass(SqlTransaction trans, string DistributorId, string strLoginName)
        {
            List<int> status = new List<int> { EnumList.MemberStatus.启用.ToInt(), EnumList.MemberStatus.锁定.ToInt() };
            StringBuilder SqlQuery = new StringBuilder();
            List<SqlParameter> listParams = new List<SqlParameter>();
            SqlQuery.Append(" and Status in(" + status.ListToStr() + ")");
            if (strLoginName.IsEmail())
            {
                SqlQuery.Append(" and PrimaryEmail=@LoginName");
            }
            else if (strLoginName.IsMobile())
            {
                SqlQuery.Append(" and PrimaryCellPhone=@LoginName");
            }
            else
            {
                SqlQuery.Append(" and UserName=@LoginName");
            }
            listParams.Add(new SqlParameter("@LoginName", strLoginName));
            SqlQuery.Append(" and DistributorId=@DistributorId");
            listParams.Add(new SqlParameter("@DistributorId", DistributorId));
            return memDAL.GetModel(trans, SqlQuery, listParams);
        }

        /// <summary>
        /// 取实体(手机登录)
        /// </summary>
        public MembershipUserModel GetModelByMobileForLogin(SqlTransaction trans, string DistributorId, string PrimaryCellPhone)
        {
            List<int> status = new List<int> { EnumList.MemberStatus.启用.ToInt(), EnumList.MemberStatus.锁定.ToInt() };
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and Status in(" + status.ListToStr() + ")");
            SqlQuery.Append(" and PrimaryCellPhone=@PrimaryCellPhone");
            SqlQuery.Append(" and DistributorId=@DistributorId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@PrimaryCellPhone", PrimaryCellPhone));
            listParams.Add(new SqlParameter("@DistributorId", DistributorId));
            return memDAL.GetModel(trans, SqlQuery, listParams);
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
            return memDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams);
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
            return memDAL.GetAllSum(trans, LeftJoin, SqlQuery, listParams, "a.DistributorId");
        }
        #endregion

        #region 登录
        /// <summary>
        /// 登录
        /// </summary>
        public bool Login(SqlTransaction trans, string DistributorId, string strLoginName, byte[] btyLoginPass, out EnumList.LoginStatus LoginStatus, out int LoginFailedCount)
        {
            bool IsLogin = memDAL.Login(trans, DistributorId, strLoginName, btyLoginPass, out LoginStatus);
            LoginFailedCount = 0;
            //记录错误密码
            if (LoginStatus == EnumList.LoginStatus.帐号或密码有误)
            {
                MembershipUserModel memModel = GetModelByLoginName(trans, DistributorId, strLoginName, true);
                if (memModel != null)
                {
                    Factory.MemberEventLog().InsertLog(trans, DistributorId, EnumList.MemberEventLogType.Login, memModel.MemberId, EnumList.OpStatus.失败.ToBoolean(), "密码有误!");

                    LoginFailedCount = Factory.MemberEventLog().GetLoginFailedCount(trans, memModel.MemberId);
                    //锁定帐号
                    if (LoginFailedCount >= Config.LockLoginFailedCount)
                    {
                        memModel.Status = (byte)EnumList.MemberStatus.锁定;
                        memDAL.UpdateInfo(trans, memModel, memModel.SN);
                    }

                }
            }
            return IsLogin;
        }
        /// <summary>
        /// 登录APP
        /// </summary>
        public bool LoginApp(SqlTransaction trans, string DistributorId, string strLoginName, byte[] btyLoginPass, out EnumList.LoginStatus LoginStatus)
        {
            return memDAL.LoginApp(trans, DistributorId, strLoginName, btyLoginPass, out LoginStatus);
        }
        /// <summary>
        /// 登录(手机)
        /// </summary>
        public bool LoginByMobile(SqlTransaction trans, string DistributorId, string strMobile)
        {
            return memDAL.LoginByMobile(trans, DistributorId, strMobile);
        }

        /// <summary>
        /// 登录(第三方登录)
        /// </summary>
        public bool LoginByThirdParty(SqlTransaction trans, string DistributorId, string strProvider, string strOAuthId)
        {
            return memDAL.LoginByThirdParty(trans, DistributorId, strProvider, strOAuthId);
        }
        #endregion

        #region 是否登录
        /// <summary>
        /// 是否登录
        /// </summary>
        public bool IsLogin(SqlTransaction trans, string DistributorId)
        {
            return memDAL.IsLogin(trans, DistributorId);
        }
        /// <summary>
        /// 是否登录
        /// </summary>
        public bool IsLogin(SqlTransaction trans, string DistributorId, string i, string p)
        {
            return memDAL.IsLogin(trans, DistributorId, i, p);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public int UpdateInfoForMember(SqlTransaction trans, MembershipUserModel memModel, long MemberId)
        {
            string key = "Cache_MembershipUser_Model_" + MemberId;
            CacheHelper.RemoveCache(key);
            return memDAL.UpdateInfoForMember(trans, memModel, MemberId);
        }
        #endregion

        #region 取资料完整度
        /// <summary>
        /// 取资料完整度
        /// </summary>
        public int GetComplete(SqlTransaction trans, long MemberId)
        {
            int dec = 0;
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and MemberId=@MemberId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@MemberId", MemberId));
            MembershipUserModel userModel = memDAL.GetModel(trans, SqlQuery, listParams);
            if (userModel != null)
            {
                if (userModel.IsPhone == Convert.ToBoolean(EnumList.IsStatus.Yes.ToInt()))
                {
                    dec += 10;
                }
                if (userModel.IsEmail == Convert.ToBoolean(EnumList.IsStatus.Yes.ToInt()))
                {
                    dec += 10;
                }
                if (userModel.Subscription)
                {
                    dec += 10;
                }
            }
            MembershipProfileModel profileModel = Factory.MembershipProfile().GetModel(null, MemberId);
            if (profileModel != null)
            {
                if (!string.IsNullOrEmpty(profileModel.Name))
                {
                    dec += 10;
                }
                if (!string.IsNullOrEmpty(profileModel.Alias))
                {
                    dec += 10;
                }
                //if (profileModel.Gender != null)
                //{
                //    dec += 10;
                //}
                if (profileModel.Birth != null)
                {
                    dec += 10;
                }
                if (profileModel.Zip != null)
                {
                    dec += 10;
                }
                if (!string.IsNullOrEmpty(profileModel.Country) && profileModel.State > 0 && profileModel.County > 0 && profileModel.CityArea > 0 && !string.IsNullOrEmpty(profileModel.Street))
                {
                    dec += 10;
                }
                if (!string.IsNullOrEmpty(profileModel.ContactEmail) && !string.IsNullOrEmpty(profileModel.Tel) && !string.IsNullOrEmpty(profileModel.CellPhoneNumber) && !string.IsNullOrEmpty(profileModel.Fax))
                {
                    dec += 10;
                }
            }
            return dec;
        }
        #endregion

        #region 用户名/手机/邮箱是否存在
        /// <summary>
        /// 用户名/手机/邮箱是否存在
        /// </summary>
        public bool LoginNameIsExist(SqlTransaction trans, string DistributorId, string strLoginName)
        {
            StringBuilder LeftJoin = new StringBuilder();
            StringBuilder SqlQuery = new StringBuilder();
            List<SqlParameter> listParams = new List<SqlParameter>();
            SqlQuery.Append(" and DistributorId=@DistributorId");
            listParams.Add(new SqlParameter("@DistributorId", DistributorId));
            if (strLoginName.IsEmail())
            {
                SqlQuery.Append(" and PrimaryEmail=@LoginName");
            }
            else if (strLoginName.IsMobile())
            {
                SqlQuery.Append(" and PrimaryCellPhone=@LoginName");
            }
            else
            {
                SqlQuery.Append(" and UserName=@LoginName");
            }
            listParams.Add(new SqlParameter("@LoginName", strLoginName));
            return memDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams) > 0;
        }
        #endregion

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(SqlTransaction trans, string strFieldName, string strFieldValue, long MemberId, string DistributorId)
        {
            return memDAL.CheckInfo(trans, strFieldName, strFieldValue, MemberId, DistributorId);
        }
        #endregion

        #region 第三方登录是否存在
        /// <summary>
        /// 第三方登录是否存在(登录)
        /// </summary>
        public bool ThirdPartyLoginIsExist(SqlTransaction trans, string DistributorId, string strProvider, string strOAuthId)
        {
            StringBuilder LeftJoin = new StringBuilder();
            LeftJoin.Append(" right join MembershipOAuth as b on b.MemberId=a.MemberId");
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and a.DistributorId=@DistributorId");
            SqlQuery.Append(" and b.Provider=@Provider");
            SqlQuery.Append(" and b.OAuthId=@OAuthId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@DistributorId", DistributorId));
            listParams.Add(new SqlParameter("@Provider", strProvider));
            listParams.Add(new SqlParameter("@OAuthId", strOAuthId));
            return memDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams) > 0;
        }
        /// <summary>
        /// 第三方登录是否存在(绑定)
        /// </summary>
        public bool ThirdPartyLoginIsExist(SqlTransaction trans, string DistributorId, string strProvider, long MemberId)
        {
            StringBuilder LeftJoin = new StringBuilder();
            LeftJoin.Append(" right join MembershipOAuth as b on b.MemberId=a.MemberId");
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append(" and a.DistributorId=@DistributorId");
            SqlQuery.Append(" and b.Provider=@Provider");
            SqlQuery.Append(" and b.MemberId=@MemberId");
            List<SqlParameter> listParams = new List<SqlParameter>();
            listParams.Add(new SqlParameter("@DistributorId", DistributorId));
            listParams.Add(new SqlParameter("@Provider", strProvider));
            listParams.Add(new SqlParameter("@MemberId", MemberId));
            return memDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams) > 0;
        }
        #endregion

        #region 第三方登录注册绑定事务
        /// <summary>
        /// 提交第三方登录注册绑定事务
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="listInput"></param>
        public bool SubmitThirdPartyRegister(string DistributorId,string PrimaryCellPhone, string openid, string Provider, string nickname, int sex, string HeadPic)
        {
            List<object> listOut = new List<object>();
            //执行事务
            if (DeleHelpler.RunTrans(SubmitThirdPartyRegisterTrans, new List<object>() { DistributorId, PrimaryCellPhone,openid, Provider, nickname, sex, HeadPic }, out listOut))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 提交第三方登录注册绑定事务
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="listInput"></param>
        private void SubmitThirdPartyRegisterTrans(SqlTransaction trans, List<object> listInput, out List<object> listOut)
        {
            //获取参数
            string DistributorId = (string)listInput[0];
            string PrimaryCellPhone = (string)listInput[1];
            string openid = (string)listInput[2];
            string Provider = (string)listInput[3];
            string nickname = (string)listInput[4];
            int sex = (int)listInput[5];
            string HeadPic = (string)listInput[6];

            //事务处理
            //注册会员
            MembershipUserModel memModel = new MembershipUserModel();
            memModel.MemberId = 0;
            memModel.DistributorId = DistributorId;
            memModel.UserName = Config.GetRndString(1, 4) + PrimaryCellPhone;
            memModel.PrimaryEmail = "";
            memModel.IsEmail = EnumList.IsStatus.No.ToBoolean();
            memModel.PrimaryCellPhone = PrimaryCellPhone;
            memModel.IsPhone = EnumList.IsStatus.Yes.ToBoolean();
            memModel.Password = new byte[] { };
            memModel.MemberLevel = "";
            memModel.Status = (byte)EnumList.MemberStatus.启用;
            memModel.FailedPasswordAttemptCount = 0;
            memModel.Subscription = EnumList.IsStatus.No.ToBoolean();
            memModel.CreationDate = DateTimeOffset.Now;
            memModel.LastUpdateDate = DateTimeOffset.Now;
            long SN = Factory.MembershipUser().InsertInfo(trans, memModel);

            //获取MemberId
            long MemberId = Factory.MembershipUser().GetValueByField(trans, "MemberId", SN).ToLong();

            //会员资料
            MembershipProfileModel memProModel = new MembershipProfileModel();
            memProModel.DistributorId = DistributorId;
            memProModel.HeadPic = HeadPic;
            memProModel.MemberId = MemberId;
            memProModel.Name = nickname;
            memProModel.Alias = "";
            memProModel.Gender = (byte)sex;
            memProModel.Birth = Convert.ToDateTime("1900-1-1");
            memProModel.Zip = "";
            memProModel.Country = "";
            memProModel.Street = "";
            memProModel.ContactEmail = "";
            memProModel.Tel = "";
            memProModel.CellPhoneNumber = PrimaryCellPhone;
            memProModel.Fax = "";
            memProModel.CreationDate = DateTimeOffset.Now;
            memProModel.LastUpdateDate = DateTimeOffset.Now;
            memProModel.State = -1;
            memProModel.County = -1;
            memProModel.CityArea = -1;
            Factory.MembershipProfile().InsertInfo(trans,memProModel);


            //绑定第三方帐号
            MembershipOAuthModel memOauthModel = new MembershipOAuthModel();
            memOauthModel.MemberId = MemberId;
            memOauthModel.Provider = Provider;
            memOauthModel.OAuthId = openid;
            memOauthModel.CreationDate = DateTimeOffset.Now;
            int result = Factory.MembershipOAuth().InsertInfo(trans, memOauthModel);

            //日志
            Factory.MemberEventLog().InsertLog(trans, DistributorId, EnumList.MemberEventLogType.Add, "会员SN为" + SN + "的会员注册成功!");

            //返回值
            listOut = new List<object>() { };
        }

        #endregion

    }
}
