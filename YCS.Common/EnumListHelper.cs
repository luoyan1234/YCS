using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YCS.Common
{
    /// <summary>
    /// 枚举类
    /// Create by Jimy
    /// </summary>
    public static class EnumList
    {
        //==============================类型
        #region 分页字符类型
        /// <summary>
        /// 分页字符类型
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        public enum PageStrType
        {
            /// <summary>
            /// 1-分页-后台
            /// </summary>
            Ma = 1,
            /// <summary>
            /// 2-分页-中文
            /// </summary>
            Cn = 2,
            /// <summary>
            /// 3-分页-数字
            /// </summary>
            Num = 3,
            /// <summary>
            /// 4-分页-无刷
            /// </summary>
            Refsh = 4,
        }
        #endregion

        #region 链接类型
        /// <summary>
        /// 链接类型
        /// </summary>
        public enum LinkType
        {
            /// <summary>
            /// 1-友情链接
            /// </summary>
            [Description("友情链接")]
            FriendLink = 1,
            /// <summary>
            /// 2-合作伙伴
            /// </summary>
            [Description("合作伙伴")]
            Partner = 2
        }
        #endregion

        #region 验证类型
        /// <summary>
        /// 验证类型
        /// </summary>
        public enum VerifyType
        {
            /// <summary>
            /// 1-手机验证
            /// </summary>
            [Description("手机验证")]
            Mobile = 1,
            /// <summary>
            /// 2-邮箱验证
            /// </summary>
            [Description("邮箱验证")]
            Email = 2
        }
        #endregion

        #region 验证动作类型
        /// <summary>
        /// 验证动作类型
        /// </summary>
        public enum VerifyActionType
        {
            /// <summary>
            /// 1-注册
            /// </summary>
            [Description("注册")]
            Register = 1,
            /// <summary>
            /// 2-登录
            /// </summary>
            [Description("登录")]
            Login = 2,
            /// <summary>
            /// 3-找回密码
            /// </summary>
            [Description("找回密码")]
            FindPass = 3,
            /// <summary>
            /// 4-绑定手机
            /// </summary>
            [Description("绑定手机")]
            BindMobile = 4,
            /// <summary>
            /// 5-更换手机
            /// </summary>
            [Description("更换手机")]
            ChangeMobile = 5,
            /// <summary>
            /// 6-绑定邮箱
            /// </summary>
            [Description("绑定邮箱")]
            BindEmail = 6,
            /// <summary>
            /// 7-更换邮箱
            /// </summary>
            [Description("更换邮箱")]
            ChangeEmail = 7,
            /// <summary>
            /// 8-第三方登录
            /// </summary>
            [Description("第三方登录")]
            ThirdParty = 8,
            /// <summary>
            /// 9-安保问题
            /// </summary>
            [Description("安保问题")]
            SecIssues = 9,
            /// <summary>
            /// 10-修改密码
            /// </summary>
            [Description("修改密码")]
            SetPassword = 10,
        }
        #endregion

        #region 地域分类
        /// <summary>
        /// 地域分类
        /// </summary>
        public enum AreaClass
        {
            /// <summary>
            /// 1-华北
            /// </summary>
            [Description("华北")]
            华北 = 1,
            /// <summary>
            /// 2-华东
            /// </summary>
            [Description("华东")]
            华东 = 2,
            /// <summary>
            /// 3-华中
            /// </summary>
            [Description("华中")]
            华中 = 3,
            /// <summary>
            /// 4-华南
            /// </summary>
            [Description("华南")]
            华南 = 4,
            /// <summary>
            /// 5-西南
            /// </summary>
            [Description("西南")]
            西南 = 5,
            /// <summary>
            /// 6-西北
            /// </summary>
            [Description("西北")]
            西北 = 6,
            /// <summary>
            /// 7-东北
            /// </summary>
            [Description("东北")]
            东北 = 7,
            /// <summary>
            /// 8-港澳台
            /// </summary>
            [Description("港澳台")]
            港澳台 = 8,
            /// <summary>
            /// 9-海外
            /// </summary>
            [Description("海外")]
            海外 = 9,
        }
        #endregion

        #region 区域类型
        /// <summary>
        /// 区域类型
        /// </summary>
        public enum AreaType
        {
            /// <summary>
            /// -1-同城
            /// </summary>
            [Description("同城")]
            同城 = -1,
            /// <summary>
            /// -2-省内
            /// </summary>
            [Description("省内")]
            省内 = -2,
            /// <summary>
            /// -3-跨省
            /// </summary>
            [Description("跨省")]
            跨省 = -3,
        }
        #endregion

        #region 发票抬头类型
        /// <summary>
        /// 发票抬头类型
        /// </summary>
        public enum InvoiceHeaderType
        {
            /// <summary>
            /// 1-个人
            /// </summary>
            [Description("个人")]
            个人 = 0,
            /// <summary>
            /// 2-公司
            /// </summary>
            [Description("公司")]
            公司 = 1,
        }
        #endregion

        #region 管理员日志动作类型
        /// <summary>
        /// 管理员日志动作类型
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        public enum AdminLogActionType
        {
            /// <summary>
            /// 1-添加
            /// </summary>
            [Description("添加")]
            Add = 1,
            /// <summary>
            /// 2-修改
            /// </summary>
            [Description("修改")]
            Edit = 2,
            /// <summary>
            /// 3-删除
            /// </summary>
            [Description("删除")]
            Del = 3,
            /// <summary>
            /// 4-登录
            /// </summary>
            [Description("登录")]
            Login = 4,
            /// <summary>
            /// 5-注销
            /// </summary>
            [Description("注销")]
            Logout = 5,
            /// <summary>
            /// 6-开启
            /// </summary>
            [Description("开启")]
            Open = 6,
            /// <summary>
            /// 7-关闭
            /// </summary>
            [Description("关闭")]
            Close = 7,
            /// <summary>
            /// 8-移动
            /// </summary>
            [Description("移动")]
            Move = 8,
            /// <summary>
            /// 9-授权
            /// </summary>
            [Description("授权")]
            Authorize = 9,
            /// <summary>
            /// 10-上传
            /// </summary>
            [Description("上传")]
            Upload = 10,
            /// <summary>
            /// 11-审核
            /// </summary>
            [Description("审核")]
            Audit = 11,
            /// <summary>
            /// 12-报价
            /// </summary>
            [Description("报价")]
            Price = 12,
            /// <summary>
            /// 13-开票
            /// </summary>
            [Description("开票")]
            Invoice = 13,
        }
        #endregion

        #region 图片类型
        /// <summary>
        /// 图片类型
        /// </summary>
        public enum ImageType
        {
            /// <summary>
            /// 3-商品SPU表
            /// </summary>
            [Description("商品SPU表")]
            ProductSpu = 3,
        }
        #endregion

        #region 经销商类型
        public enum DistributorType
        {
            /// <summary>
            /// 1-合作工厂
            /// </summary>
            [Description("合作工厂")]
            Factory = 1,
            /// <summary>
            /// 2-经销商
            /// </summary>
            [Description("经销商")]
            Distributor = 2,
            /// <summary>
            /// 3-图文店
            /// </summary>
            [Description("图文店")]
            Store = 3,
        }
        #endregion

        #region 会员日志类型
        /// <summary>
        /// 会员日志类型
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        public enum MemberEventLogType
        {
            /// <summary>
            /// 1-添加
            /// </summary>
            [Description("添加")]
            Add = 1,
            /// <summary>
            /// 2-修改
            /// </summary>
            [Description("修改")]
            Edit = 2,
            /// <summary>
            /// 3-删除
            /// </summary>
            [Description("删除")]
            Del = 3,
            /// <summary>
            /// 4-登录
            /// </summary>
            [Description("登录")]
            Login = 4,
            /// <summary>
            /// 5-注销
            /// </summary>
            [Description("注销")]
            Logout = 5,
            /// <summary>
            /// 6-找回密码
            /// </summary>
            [Description("找回密码")]
            FindPass = 6,
            /// <summary>
            /// 7-绑定手机
            /// </summary>
            [Description("绑定手机")]
            BindPhone = 7,
            /// <summary>
            /// 8-更换手机
            /// </summary>
            [Description("更换手机")]
            ChangePhone = 8,
            /// <summary>
            /// 9-绑定邮箱
            /// </summary>
            [Description("绑定邮箱")]
            BindEmail = 9,
            /// <summary>
            /// 10-更换邮箱
            /// </summary>
            [Description("更换邮箱")]
            ChangeEmail = 10,
            /// <summary>
            /// 11-修改密码
            /// </summary>
            [Description("修改密码")]
            EditPwd = 11,
            /// <summary>
            /// 12-第三方登录
            /// </summary>
            [Description("第三方登录")]
            ThirdPartyLogin = 12,
            /// <summary>
            /// 13-设置安保问题
            /// </summary>
            [Description("设置安保问题")]
            SetSecIssues = 13,
            /// <summary>
            /// 14-修改安保问题
            /// </summary>
            [Description("修改安保问题")]
            UpdateSecIssues = 14,
            /// <summary>
            /// 15-关注
            /// </summary>
            [Description("关注")]
            Attention = 8,
            /// <summary>
            /// 15-支付
            /// </summary>
            [Description("支付")]
            Pay = 15,
        }
        #endregion

        #region 第三方登录类型
        /// <summary>
        /// 第三方登录类型
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        public enum ThirdPartyLoginProvider
        {
            /// <summary>
            /// 1-QQ
            /// </summary>
            [Description("QQ")]
            QQ = 1,
            /// <summary>
            /// 2-微信
            /// </summary>
            [Description("微信")]
            WeChat = 2,
        }
        #endregion
        //==============================状态
        #region  显示状态
        /// <summary>
        /// 显示状态
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        public enum ShowStatus
        {
            /// <summary>
            /// 0-隐藏
            /// </summary>
            [Description("<span style=\"color:#FF0004\">隐藏</span>")]
            Hide = 0,
            /// <summary>
            /// 1-显示
            /// </summary>
            [Description("显示")]
            Show = 1

        }
        #endregion

        #region  启用状态
        /// <summary>
        /// 启用状态
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        public enum OpenStatus
        {
            /// <summary>
            /// 0-关闭
            /// </summary>
            [Description("<span style=\"color:#FF0004\">关闭</span>")]
            Close = 0,
            /// <summary>
            /// 1-开启
            /// </summary>
            [Description("开启")]
            Open = 1,

        }
        #endregion
        
        #region 关闭状态
        /// <summary>
        /// 关闭状态
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        public enum CloseStatus
        {
            /// <summary>
            /// 0-开启
            /// </summary>
            [Description("开启")]
            Open = 0,

            /// <summary>
            /// 1-关闭
            /// </summary>
            [Description("<span style=\"color:#FF0004\">关闭</span>")]
            Close = 1
        }
        #endregion

        #region 是否状态
        /// <summary>
        /// 是否状态
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        public enum IsStatus
        {
            /// <summary>
            /// 0-否
            /// </summary>
            [Description("<span style=\"color:#FF0004\">否</span>")]
            No = 0,

            /// <summary>
            /// 1-是
            /// </summary>
            [Description("是")]
            Yes = 1
        }
        #endregion

        #region 验证状态
        /// <summary>
        /// 验证状态
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        public enum VerifyStatus
        {
            /// <summary>
            /// 0-未验证
            /// </summary>
            [Description("<span style=\"color:#FF0004\">未验证</span>")]
            未验证 = 0,

            /// <summary>
            /// 1-已验证
            /// </summary>
            [Description("已验证")]
            已验证 = 1
        }
        #endregion

        #region 付款状态
        /// <summary>
        /// 付款状态
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        public enum PayStatus
        {
            /// <summary>
            /// 0-未付款
            /// </summary>
            [Description("<span style=\"color:#FF0004\">未付款</span>")]
            未付款 = 0,

            /// <summary>
            /// 1-已付款
            /// </summary>
            [Description("已付款")]
            已付款 = 1
        }
        #endregion

        #region 商品SPU状态
        /// <summary>
        /// 商品SPU状态
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        public enum ProductSpuStatus
        {
            /// <summary>
            /// 0-上架
            /// </summary>
            [Description("上架")]
            上架 = 0,
            /// <summary>
            /// 1-下架
            /// </summary>
            [Description("下架")]
            下架 = 1,
            /// <summary>
            /// 2-停售
            /// </summary>
            [Description("停售")]
            停售 = 2,
            /// <summary>
            /// 3-缺貨
            /// </summary>
            [Description("缺货")]
            缺貨 = 3,
        }
        #endregion

        #region 询价状态
        /// <summary>
        /// 询价状态
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        public enum QuotationStatus
        {
            /// <summary>
            /// 1-未报价
            /// </summary>
            [Description("<span style=\"color:#FF0004\">未报价</span>")]
            未报价 = 1,

            /// <summary>
            /// 2-已报价
            /// </summary>
            [Description("已报价")]
            已报价 = 2,

            /// <summary>
            /// -10-已过期
            /// </summary>
            [Description("已过期")]
            已过期 = -10,

            /// <summary>
            /// -99-已取消
            /// </summary>
            [Description("已取消")]
            已取消 = -99,
        }
        #endregion

        #region 会员状态
        /// <summary>
        /// 会员状态
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        public enum MemberStatus
        {
            /// <summary>
            /// 0-未启用
            /// </summary>
            [Description("<span style=\"color:#FF0004\">未启用</span>")]
            未启用 = 0,

            /// <summary>
            /// 10-启用
            /// </summary>
            [Description("启用")]
            启用 = 10,

            /// <summary>
            /// 40-锁定
            /// </summary>
            [Description("锁定")]
            锁定 = 40,

            /// <summary>
            /// -1-停用
            /// </summary>
            [Description("停用")]
            停用 = -1,
        }
        #endregion

        #region 登录状态
        /// <summary>
        /// 登录状态
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        public enum LoginStatus
        {
            /// <summary>
            /// -1-帐号或密码有误
            /// </summary>
            [Description("帐号或密码有误")]
            帐号或密码有误 = -1,

            /// <summary>
            /// 0-登录成功
            /// </summary>
            [Description("登录成功")]
            登录成功 = 0,

            /// <summary>
            /// 1-帐号禁用
            /// </summary>
            [Description("帐号禁用")]
            帐号禁用 = 1,

            /// <summary>
            /// 2-帐号锁定
            /// </summary>
            [Description("帐号锁定")]
            帐号锁定 = 2,

            /// <summary>
            /// 3-帐号未启用
            /// </summary>
            [Description("帐号未启用")]
            帐号未启用 = 3,
        }
        #endregion

        #region 操作状态
        /// <summary>
        /// 操作状态
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        public enum OpStatus
        {
            /// <summary>
            /// 0-失败
            /// </summary>
            [Description("<span style=\"color:#FF0004\">失败</span>")]
            失败 = 0,

            /// <summary>
            /// 1-成功
            /// </summary>
            [Description("成功")]
            成功 = 1
        }
        #endregion

        #region 经销商状态
        public enum DistributorStatus
        {
            /// <summary>
            /// 1-未启用
            /// </summary>
            [Description("未启用")]
            Unable = 1,
            /// <summary>
            /// 10-启用
            /// </summary>
            [Description("启用")]
            Enable = 10,
            /// <summary>
            /// 40-锁定
            /// </summary>
            [Description("锁定")]
            Lock = 40,
            /// <summary>
            /// -1-停用
            /// </summary>
            [Description("停用")]
            Block = -1,
        }
        #endregion

        #region 传送状态
        /// <summary>
        /// 传送状态
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        public enum SendStatus
        {
            /// <summary>
            /// 0-待传送
            /// </summary>
            [Description("待传送")]
            待传送 = 0,

            /// <summary>
            /// 1-传送完成
            /// </summary>
            [Description("传送完成")]
            传送完成 = 1,

            /// <summary>
            /// 2-传送失败
            /// </summary>
            [Description("传送失败")]
            传送失败 = 2,
        }
        #endregion
        //==============================其他
        #region 性别
        /// <summary>
        /// 性别
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        public enum Sex
        {
            /// <summary>
            /// 0-保密
            /// </summary>
            [Description("保密")]
            保密 = 0,

            /// <summary>
            /// 1-男
            /// </summary>
            [Description("男")]
            男 = 1,

            /// <summary>
            /// 2-女
            /// </summary>
            [Description("女")]
            女 = 2
        }
        #endregion

        #region 文件命名方式
        /// <summary>
        /// 文件命名方式
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        public enum FileNameType
        {
            /// <summary>
            /// 0-原文件名
            /// </summary>
            [Description("原文件名")]
            OriginalName = 0,

            /// <summary>
            /// 1-自动命名
            /// </summary>
            [Description("自动命名")]
            AutoName = 1
        }
        #endregion

        #region 密码强度
        /// <summary>
        /// 密码强度
        /// </summary>
        public enum Strength
        {
            /// <summary>
            /// 0-无效密码
            /// </summary>
            [Description("无效")]
            Invalid = 0,
            /// <summary>
            /// 1-低强度密码
            /// </summary>
            [Description("低")]
            Weak = 1,
            /// <summary>
            /// 2-中强度密码
            /// </summary>
            [Description("中")]
            Normal = 2, 
            /// <summary>
            /// 3-高强度密码
            /// </summary>
            [Description("高")]
            Strong = 3 
        }
        #endregion

        //==============================会员中心
        #region 发票
        public enum InvoiceType
        {
            [Description("普通发票")]
            普通发票 = 1,
            [Description("增值税发票")]
            增值税发票 = 2
        }
        public enum InvoiceStatusType
        {
            [Description("申请中")]
            申请中 = 0,
            [Description("已开")]
            已开 = 1
        }
        public enum IdEntityVerifyType
        {
            [Description("未审核")]
            未审核 = 0,
            [Description("审核中")]
            审核中 = 1,
            [Description("已审核")]
            已审核 = 2,
            [Description("审核不通过")]
            审核不通过 = -1
        }
        #endregion

        //=================商品相关
        #region 商品类型
        /// <summary>
        /// 商品类型
        /// </summary>
        public enum ProSpuType
        {
            [Description("可线上编辑的商品")]
            可线上编辑的商品 = 1,
            [Description("样品成品")]
            样品成品 = 2,
            [Description("活动商品")]
            活动商品 = 3,
            [Description("用来查询尺寸的商品")]
            用来查询尺寸的商品 = 4,
            [Description("可发档送印的商品")]
            可发档送印的商品 = 510,
            [Description("可发档送印的样品成品")]
            可发档送印的样品成品 = 520,
            [Description("可发档送印的配件")]
            可发档送印的配件 = 521,
            [Description("编辑器App商品")]
            编辑器App商品 = 610,
            [Description("沖印App商品")]
            沖印App商品 = 620,
            [Description("可同时制作与发印")]
            可同时制作与发印 = 700
        }
        #endregion

        #region 订制流程
        /// <summary>
        /// 订制流程
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        public enum ProductProcessType
        {
            /// <summary>
            /// 1-在线自助设计订制流程
            /// </summary>
            [Description("在线自助设计订制流程")]
            DiyProcess = 1,

            /// <summary>
            /// 2-设计文件下载订制流程
            /// </summary>
            [Description("设计文件下载订制流程")]
            UploadProcess = 2,
        }
        #endregion

        #region 楼层所能设置的商品类型
        /// <summary>
        /// 楼层所能设置的商品类型
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        public enum FloorProductType
        {
            /// <summary>
            /// 99-个性化商品
            /// </summary>
            [Description("个性化商品")]
            Personal = 99,
            /// <summary>
            /// 1-热卖商品
            /// </summary>
            [Description("热卖商品")]
            Hot = 1,

            /// <summary>
            /// 2-推荐商品
            /// </summary>
            [Description("推荐商品")]
            Recommend = 2,
        }
        #endregion

        #region 编辑器类型
        /// <summary>
        /// 编辑器类型
        /// </summary>
        public enum EditorProvider
        {

            [Description("名片编辑器(GEditor)")]
            GEditor,
            [Description("海报编辑器(MaxmEditor)")]
            MaxmEditor
        }
        #endregion
        
        //=================订单相关
        #region 订单状态
        public enum OrderStatus
        {
            /// <summary>
            /// 1-待付款
            /// </summary>
            [Description("待付款")]
            待付款 = 1,
            /// <summary>
            /// 2-待上传设计文件
            /// </summary>
            [Description("待上传设计文件")]
            待上传设计文件 = 2,
            /// <summary>
            /// 9-待审核
            /// </summary>
            [Description("待审核")]
            待审核 = 9,
            /// <summary>
            /// 3-待发货
            /// </summary>
            [Description("待发货")]
            待发货 = 3,
            /// <summary>
            /// 4-待收货
            /// </summary>
            [Description("待收货")]
            待收货 = 4,
            /// <summary>
            /// 5-已完成
            /// </summary>
            [Description("已完成")]
            已完成 = 5,
            /// <summary>
            /// 6-已取消
            /// </summary>
            [Description("已取消")]
            已取消 = 6,
            /// <summary>
            /// 7-交易失败
            /// </summary>
            [Description("交易失败")]
            交易失败 = 7,
        }
        //public enum OrderStatusAdmin
        //{
        //    /// <summary>
        //    /// 1-待付款
        //    /// </summary>
        //    [Description("待付款")]
        //    待付款 = 1,
        //    /// <summary>
        //    /// 2-待上传设计文件
        //    /// </summary>
        //    [Description("待上传设计文件")]
        //    待上传设计文件 = 2,
        //    /// <summary>
        //    /// 3-待发货
        //    /// </summary>
        //    [Description("待发货")]
        //    待发货 = 3,
        //    /// <summary>
        //    /// 4-待收货
        //    /// </summary>
        //    [Description("待收货")]
        //    待收货 = 4,
        //    /// <summary>
        //    /// 5-已完成
        //    /// </summary>
        //    [Description("已完成")]
        //    已完成 = 5,
        //    /// <summary>
        //    /// 6-已取消
        //    /// </summary>
        //    [Description("已取消")]
        //    已取消 = 6,
        //    /// <summary>
        //    /// 7-交易失败
        //    /// </summary>
        //    [Description("交易失败")]
        //    交易失败 = 7,
        //}
        #endregion

        #region 付款状态
        /// <summary>
        /// 1支付宝；2微信支付；3货到付款
        /// </summary>
        public enum PayType
        {
            /// <summary>
            /// 1-支付宝支付
            /// </summary>
            [Description("支付宝支付")]
            支付宝支付 = 1,
            /// <summary>
            /// 2-微信支付
            /// </summary>
            [Description("微信支付")]
            微信支付 = 2,
            /// <summary>
            /// 3-货到付款
            /// </summary>
            [Description("货到付款")]
            货到付款 = 3,
        }
        #endregion

        #region 订单类型
        public enum OrderType
        {
            /// <summary>
            /// 1-在线设计订单
            /// </summary>
            [Description("在线设计订单")]
            在线设计订单 = 1,
            /// <summary>
            /// 2-下载设计订单
            /// </summary>
            [Description("下载设计订单")]
            下载设计订单 = 2,
        }
        #endregion

        #region 文件状态
        /// <summary>
        /// 文件状态
        /// Author:Jimy
        /// Create Time:2015-07-16 14:11:35
        /// </summary>
        public enum FileStatus
        {
            /// <summary>
            /// 0-未生成
            /// </summary>
            [Description("未生成")]
            未生成 = 0,

            /// <summary>
            /// 1-已生成
            /// </summary>
            [Description("已生成")]
            已生成 = 1,

            /// <summary>
            /// 2-生成失败
            /// </summary>
            [Description("生成失败")]
            生成失败 = 2,

            /// <summary>
            /// 3-已上传
            /// </summary>
            [Description("已上传")]
            已上传 = 3,

            /// <summary>
            /// 4-上传失败
            /// </summary>
            [Description("上传失败")]
            上传失败 = 4,
        }
        #endregion

    }
}
