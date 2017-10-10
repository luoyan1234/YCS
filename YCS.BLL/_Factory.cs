using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YCS.BLL
{
/// <summary>
/// 工厂类
/// </summary>
public class Factory:Base.BaseFactory
{
#region 广告
private static AdBLL _adbll;
/// <summary>
/// 广告
/// </summary>
/// <returns></returns>
public static AdBLL Ad()
{
if (_adbll == null)
{
_adbll = new AdBLL();
}
return _adbll;
}
#endregion
#region 後台帳號表
private static AdminBLL _adminbll;
/// <summary>
/// 後台帳號表
/// </summary>
/// <returns></returns>
public static AdminBLL Admin()
{
if (_adminbll == null)
{
_adminbll = new AdminBLL();
}
return _adminbll;
}
#endregion
#region 後台管理員日誌表
private static AdminLogBLL _adminlogbll;
/// <summary>
/// 後台管理員日誌表
/// </summary>
/// <returns></returns>
public static AdminLogBLL AdminLog()
{
if (_adminlogbll == null)
{
_adminlogbll = new AdminLogBLL();
}
return _adminlogbll;
}
#endregion
#region 广告位
private static AdPositionBLL _adpositionbll;
/// <summary>
/// 广告位
/// </summary>
/// <returns></returns>
public static AdPositionBLL AdPosition()
{
if (_adpositionbll == null)
{
_adpositionbll = new AdPositionBLL();
}
return _adpositionbll;
}
#endregion
#region 
private static AIPS_CodeReferenceBLL _aips_codereferencebll;
/// <summary>
/// 
/// </summary>
/// <returns></returns>
public static AIPS_CodeReferenceBLL AIPS_CodeReference()
{
if (_aips_codereferencebll == null)
{
_aips_codereferencebll = new AIPS_CodeReferenceBLL();
}
return _aips_codereferencebll;
}
#endregion
#region 
private static AIPS_ProductSkuBLL _aips_productskubll;
/// <summary>
/// 
/// </summary>
/// <returns></returns>
public static AIPS_ProductSkuBLL AIPS_ProductSku()
{
if (_aips_productskubll == null)
{
_aips_productskubll = new AIPS_ProductSkuBLL();
}
return _aips_productskubll;
}
#endregion
#region 地区
private static AreaBLL _areabll;
/// <summary>
/// 地区
/// </summary>
/// <returns></returns>
public static AreaBLL Area()
{
if (_areabll == null)
{
_areabll = new AreaBLL();
}
return _areabll;
}
#endregion
#region 文章
private static ArticleBLL _articlebll;
/// <summary>
/// 文章
/// </summary>
/// <returns></returns>
public static ArticleBLL Article()
{
if (_articlebll == null)
{
_articlebll = new ArticleBLL();
}
return _articlebll;
}
#endregion
#region 购物车
private static CartBLL _cartbll;
/// <summary>
/// 购物车
/// </summary>
/// <returns></returns>
public static CartBLL Cart()
{
if (_cartbll == null)
{
_cartbll = new CartBLL();
}
return _cartbll;
}
#endregion
#region 栏目
private static ClassBLL _classbll;
/// <summary>
/// 栏目
/// </summary>
/// <returns></returns>
public static ClassBLL Class()
{
if (_classbll == null)
{
_classbll = new ClassBLL();
}
return _classbll;
}
#endregion
#region 栏目属性
private static ClassPropertyBLL _classpropertybll;
/// <summary>
/// 栏目属性
/// </summary>
/// <returns></returns>
public static ClassPropertyBLL ClassProperty()
{
if (_classpropertybll == null)
{
_classpropertybll = new ClassPropertyBLL();
}
return _classpropertybll;
}
#endregion
#region 栏目模板
private static ClassTemplateBLL _classtemplatebll;
/// <summary>
/// 栏目模板
/// </summary>
/// <returns></returns>
public static ClassTemplateBLL ClassTemplate()
{
if (_classtemplatebll == null)
{
_classtemplatebll = new ClassTemplateBLL();
}
return _classtemplatebll;
}
#endregion
#region 代码对应表 
private static CodeReferenceBLL _codereferencebll;
/// <summary>
/// 代码对应表 
/// </summary>
/// <returns></returns>
public static CodeReferenceBLL CodeReference()
{
if (_codereferencebll == null)
{
_codereferencebll = new CodeReferenceBLL();
}
return _codereferencebll;
}
#endregion
#region 代码对应类型表
private static CodeReferenceTypeBLL _codereferencetypebll;
/// <summary>
/// 代码对应类型表
/// </summary>
/// <returns></returns>
public static CodeReferenceTypeBLL CodeReferenceType()
{
if (_codereferencetypebll == null)
{
_codereferencetypebll = new CodeReferenceTypeBLL();
}
return _codereferencetypebll;
}
#endregion
#region 系统配置
private static ConfigBLL _configbll;
/// <summary>
/// 系统配置
/// </summary>
/// <returns></returns>
public static ConfigBLL Config()
{
if (_configbll == null)
{
_configbll = new ConfigBLL();
}
return _configbll;
}
#endregion
#region 配送方式
private static DeliverBLL _deliverbll;
/// <summary>
/// 配送方式
/// </summary>
/// <returns></returns>
public static DeliverBLL Deliver()
{
if (_deliverbll == null)
{
_deliverbll = new DeliverBLL();
}
return _deliverbll;
}
#endregion
#region 配送费用
private static DeliverCostBLL _delivercostbll;
/// <summary>
/// 配送费用
/// </summary>
/// <returns></returns>
public static DeliverCostBLL DeliverCost()
{
if (_delivercostbll == null)
{
_delivercostbll = new DeliverCostBLL();
}
return _delivercostbll;
}
#endregion
#region 经销商
private static DistributorBLL _distributorbll;
/// <summary>
/// 经销商
/// </summary>
/// <returns></returns>
public static DistributorBLL Distributor()
{
if (_distributorbll == null)
{
_distributorbll = new DistributorBLL();
}
return _distributorbll;
}
#endregion
#region 經銷商對應產品資料表
private static DistributorProductSettingBLL _distributorproductsettingbll;
/// <summary>
/// 經銷商對應產品資料表
/// </summary>
/// <returns></returns>
public static DistributorProductSettingBLL DistributorProductSetting()
{
if (_distributorproductsettingbll == null)
{
_distributorproductsettingbll = new DistributorProductSettingBLL();
}
return _distributorproductsettingbll;
}
#endregion
#region 經銷商資料
private static DistributorProfileBLL _distributorprofilebll;
/// <summary>
/// 經銷商資料
/// </summary>
/// <returns></returns>
public static DistributorProfileBLL DistributorProfile()
{
if (_distributorprofilebll == null)
{
_distributorprofilebll = new DistributorProfileBLL();
}
return _distributorprofilebll;
}
#endregion
#region 經銷商外部服務設定
private static DistributorServiceSettingBLL _distributorservicesettingbll;
/// <summary>
/// 經銷商外部服務設定
/// </summary>
/// <returns></returns>
public static DistributorServiceSettingBLL DistributorServiceSetting()
{
if (_distributorservicesettingbll == null)
{
_distributorservicesettingbll = new DistributorServiceSettingBLL();
}
return _distributorservicesettingbll;
}
#endregion
#region 經銷商外部服務設定類型
private static DistributorServiceTypeBLL _distributorservicetypebll;
/// <summary>
/// 經銷商外部服務設定類型
/// </summary>
/// <returns></returns>
public static DistributorServiceTypeBLL DistributorServiceType()
{
if (_distributorservicetypebll == null)
{
_distributorservicetypebll = new DistributorServiceTypeBLL();
}
return _distributorservicetypebll;
}
#endregion
#region 經銷商網站屬性配置
private static DistributorWebSitePropertySettingBLL _distributorwebsitepropertysettingbll;
/// <summary>
/// 經銷商網站屬性配置
/// </summary>
/// <returns></returns>
public static DistributorWebSitePropertySettingBLL DistributorWebSitePropertySetting()
{
if (_distributorwebsitepropertysettingbll == null)
{
_distributorwebsitepropertysettingbll = new DistributorWebSitePropertySettingBLL();
}
return _distributorwebsitepropertysettingbll;
}
#endregion
#region 错误日志
private static ErrorBLL _errorbll;
/// <summary>
/// 错误日志
/// </summary>
/// <returns></returns>
public static ErrorBLL Error()
{
if (_errorbll == null)
{
_errorbll = new ErrorBLL();
}
return _errorbll;
}
#endregion
#region 楼层
private static FloorBLL _floorbll;
/// <summary>
/// 楼层
/// </summary>
/// <returns></returns>
public static FloorBLL Floor()
{
if (_floorbll == null)
{
_floorbll = new FloorBLL();
}
return _floorbll;
}
#endregion
#region 楼层个性化商品设置
private static FloorPersonalProductBLL _floorpersonalproductbll;
/// <summary>
/// 楼层个性化商品设置
/// </summary>
/// <returns></returns>
public static FloorPersonalProductBLL FloorPersonalProduct()
{
if (_floorpersonalproductbll == null)
{
_floorpersonalproductbll = new FloorPersonalProductBLL();
}
return _floorpersonalproductbll;
}
#endregion
#region 楼层产品分类
private static FloorProductCategoryBLL _floorproductcategorybll;
/// <summary>
/// 楼层产品分类
/// </summary>
/// <returns></returns>
public static FloorProductCategoryBLL FloorProductCategory()
{
if (_floorproductcategorybll == null)
{
_floorproductcategorybll = new FloorProductCategoryBLL();
}
return _floorproductcategorybll;
}
#endregion
#region 楼层热卖推荐商品设置
private static FloorProductSettingBLL _floorproductsettingbll;
/// <summary>
/// 楼层热卖推荐商品设置
/// </summary>
/// <returns></returns>
public static FloorProductSettingBLL FloorProductSetting()
{
if (_floorproductsettingbll == null)
{
_floorproductsettingbll = new FloorProductSettingBLL();
}
return _floorproductsettingbll;
}
#endregion
#region 商品圖片描述表 
private static ImageDescriptionBLL _imagedescriptionbll;
/// <summary>
/// 商品圖片描述表 
/// </summary>
/// <returns></returns>
public static ImageDescriptionBLL ImageDescription()
{
if (_imagedescriptionbll == null)
{
_imagedescriptionbll = new ImageDescriptionBLL();
}
return _imagedescriptionbll;
}
#endregion
#region 图片路径表 
private static ImageReferenceBLL _imagereferencebll;
/// <summary>
/// 图片路径表 
/// </summary>
/// <returns></returns>
public static ImageReferenceBLL ImageReference()
{
if (_imagereferencebll == null)
{
_imagereferencebll = new ImageReferenceBLL();
}
return _imagereferencebll;
}
#endregion
#region 发票
private static InvoiceBLL _invoicebll;
/// <summary>
/// 发票
/// </summary>
/// <returns></returns>
public static InvoiceBLL Invoice()
{
if (_invoicebll == null)
{
_invoicebll = new InvoiceBLL();
}
return _invoicebll;
}
#endregion
#region 链接
private static LinkBLL _linkbll;
/// <summary>
/// 链接
/// </summary>
/// <returns></returns>
public static LinkBLL Link()
{
if (_linkbll == null)
{
_linkbll = new LinkBLL();
}
return _linkbll;
}
#endregion
#region 印刷商
private static ManuFacturerBLL _manufacturerbll;
/// <summary>
/// 印刷商
/// </summary>
/// <returns></returns>
public static ManuFacturerBLL ManuFacturer()
{
if (_manufacturerbll == null)
{
_manufacturerbll = new ManuFacturerBLL();
}
return _manufacturerbll;
}
#endregion
#region 會員事件紀錄
private static MemberEventLogBLL _membereventlogbll;
/// <summary>
/// 會員事件紀錄
/// </summary>
/// <returns></returns>
public static MemberEventLogBLL MemberEventLog()
{
if (_membereventlogbll == null)
{
_membereventlogbll = new MemberEventLogBLL();
}
return _membereventlogbll;
}
#endregion
#region 
private static MemberReferenceAIPSBLL _memberreferenceaipsbll;
/// <summary>
/// 
/// </summary>
/// <returns></returns>
public static MemberReferenceAIPSBLL MemberReferenceAIPS()
{
if (_memberreferenceaipsbll == null)
{
_memberreferenceaipsbll = new MemberReferenceAIPSBLL();
}
return _memberreferenceaipsbll;
}
#endregion
#region 
private static MembershipOAuthBLL _membershipoauthbll;
/// <summary>
/// 
/// </summary>
/// <returns></returns>
public static MembershipOAuthBLL MembershipOAuth()
{
if (_membershipoauthbll == null)
{
_membershipoauthbll = new MembershipOAuthBLL();
}
return _membershipoauthbll;
}
#endregion
#region 
private static MembershipProfileBLL _membershipprofilebll;
/// <summary>
/// 
/// </summary>
/// <returns></returns>
public static MembershipProfileBLL MembershipProfile()
{
if (_membershipprofilebll == null)
{
_membershipprofilebll = new MembershipProfileBLL();
}
return _membershipprofilebll;
}
#endregion
#region 会员安保问题
private static MembershipSecIssuesBLL _membershipsecissuesbll;
/// <summary>
/// 会员安保问题
/// </summary>
/// <returns></returns>
public static MembershipSecIssuesBLL MembershipSecIssues()
{
if (_membershipsecissuesbll == null)
{
_membershipsecissuesbll = new MembershipSecIssuesBLL();
}
return _membershipsecissuesbll;
}
#endregion
#region 
private static MembershipUserBLL _membershipuserbll;
/// <summary>
/// 
/// </summary>
/// <returns></returns>
public static MembershipUserBLL MembershipUser()
{
if (_membershipuserbll == null)
{
_membershipuserbll = new MembershipUserBLL();
}
return _membershipuserbll;
}
#endregion
#region 編輯器模板
private static ModuleBLL _modulebll;
/// <summary>
/// 編輯器模板
/// </summary>
/// <returns></returns>
public static ModuleBLL Module()
{
if (_modulebll == null)
{
_modulebll = new ModuleBLL();
}
return _modulebll;
}
#endregion
#region 訂單紀錄表
private static OrderBLL _orderbll;
/// <summary>
/// 訂單紀錄表
/// </summary>
/// <returns></returns>
public static OrderBLL Order()
{
if (_orderbll == null)
{
_orderbll = new OrderBLL();
}
return _orderbll;
}
#endregion
#region 訂單歷史紀錄表			
private static OrderHistoryBLL _orderhistorybll;
/// <summary>
/// 訂單歷史紀錄表			
/// </summary>
/// <returns></returns>
public static OrderHistoryBLL OrderHistory()
{
if (_orderhistorybll == null)
{
_orderhistorybll = new OrderHistoryBLL();
}
return _orderhistorybll;
}
#endregion
#region 訂單明細表
private static OrderItemBLL _orderitembll;
/// <summary>
/// 訂單明細表
/// </summary>
/// <returns></returns>
public static OrderItemBLL OrderItem()
{
if (_orderitembll == null)
{
_orderitembll = new OrderItemBLL();
}
return _orderitembll;
}
#endregion
#region 編輯器模式
private static PatternBLL _patternbll;
/// <summary>
/// 編輯器模式
/// </summary>
/// <returns></returns>
public static PatternBLL Pattern()
{
if (_patternbll == null)
{
_patternbll = new PatternBLL();
}
return _patternbll;
}
#endregion
#region 編輯器模式組件
private static PatternComponentBLL _patterncomponentbll;
/// <summary>
/// 編輯器模式組件
/// </summary>
/// <returns></returns>
public static PatternComponentBLL PatternComponent()
{
if (_patterncomponentbll == null)
{
_patterncomponentbll = new PatternComponentBLL();
}
return _patterncomponentbll;
}
#endregion
#region 編輯器模式組件樣式
private static PatternComponentStyleBLL _patterncomponentstylebll;
/// <summary>
/// 編輯器模式組件樣式
/// </summary>
/// <returns></returns>
public static PatternComponentStyleBLL PatternComponentStyle()
{
if (_patterncomponentstylebll == null)
{
_patterncomponentstylebll = new PatternComponentStyleBLL();
}
return _patterncomponentstylebll;
}
#endregion
#region 支付方式
private static PayBLL _paybll;
/// <summary>
/// 支付方式
/// </summary>
/// <returns></returns>
public static PayBLL Pay()
{
if (_paybll == null)
{
_paybll = new PayBLL();
}
return _paybll;
}
#endregion
#region 支付日志
private static PayLogBLL _paylogbll;
/// <summary>
/// 支付日志
/// </summary>
/// <returns></returns>
public static PayLogBLL PayLog()
{
if (_paylogbll == null)
{
_paylogbll = new PayLogBLL();
}
return _paylogbll;
}
#endregion
#region 
private static PortfolioBLL _portfoliobll;
/// <summary>
/// 
/// </summary>
/// <returns></returns>
public static PortfolioBLL Portfolio()
{
if (_portfoliobll == null)
{
_portfoliobll = new PortfolioBLL();
}
return _portfoliobll;
}
#endregion
#region 商品属性值資料表
private static ProductAttributeLibraryBLL _productattributelibrarybll;
/// <summary>
/// 商品属性值資料表
/// </summary>
/// <returns></returns>
public static ProductAttributeLibraryBLL ProductAttributeLibrary()
{
if (_productattributelibrarybll == null)
{
_productattributelibrarybll = new ProductAttributeLibraryBLL();
}
return _productattributelibrarybll;
}
#endregion
#region 商品屬性類別表
private static ProductAttributeTypeLibraryBLL _productattributetypelibrarybll;
/// <summary>
/// 商品屬性類別表
/// </summary>
/// <returns></returns>
public static ProductAttributeTypeLibraryBLL ProductAttributeTypeLibrary()
{
if (_productattributetypelibrarybll == null)
{
_productattributetypelibrarybll = new ProductAttributeTypeLibraryBLL();
}
return _productattributetypelibrarybll;
}
#endregion
#region 产品分类
private static ProductCategorySettingBLL _productcategorysettingbll;
/// <summary>
/// 产品分类
/// </summary>
/// <returns></returns>
public static ProductCategorySettingBLL ProductCategorySetting()
{
if (_productcategorysettingbll == null)
{
_productcategorysettingbll = new ProductCategorySettingBLL();
}
return _productcategorysettingbll;
}
#endregion
#region 产品收藏
private static ProductCollectBLL _productcollectbll;
/// <summary>
/// 产品收藏
/// </summary>
/// <returns></returns>
public static ProductCollectBLL ProductCollect()
{
if (_productcollectbll == null)
{
_productcollectbll = new ProductCollectBLL();
}
return _productcollectbll;
}
#endregion
#region 
private static ProductEditorTemplateBLL _producteditortemplatebll;
/// <summary>
/// 
/// </summary>
/// <returns></returns>
public static ProductEditorTemplateBLL ProductEditorTemplate()
{
if (_producteditortemplatebll == null)
{
_producteditortemplatebll = new ProductEditorTemplateBLL();
}
return _producteditortemplatebll;
}
#endregion
#region 
private static ProductEditorTemplateTypeBLL _producteditortemplatetypebll;
/// <summary>
/// 
/// </summary>
/// <returns></returns>
public static ProductEditorTemplateTypeBLL ProductEditorTemplateType()
{
if (_producteditortemplatetypebll == null)
{
_producteditortemplatetypebll = new ProductEditorTemplateTypeBLL();
}
return _producteditortemplatetypebll;
}
#endregion
#region 商品選項類別表
private static ProductOptionTypesBLL _productoptiontypesbll;
/// <summary>
/// 商品選項類別表
/// </summary>
/// <returns></returns>
public static ProductOptionTypesBLL ProductOptionTypes()
{
if (_productoptiontypesbll == null)
{
_productoptiontypesbll = new ProductOptionTypesBLL();
}
return _productoptiontypesbll;
}
#endregion
#region 商品選項資料表
private static ProductOptionValuesBLL _productoptionvaluesbll;
/// <summary>
/// 商品選項資料表
/// </summary>
/// <returns></returns>
public static ProductOptionValuesBLL ProductOptionValues()
{
if (_productoptionvaluesbll == null)
{
_productoptionvaluesbll = new ProductOptionValuesBLL();
}
return _productoptionvaluesbll;
}
#endregion
#region 產品價格
private static ProductPriceBLL _productpricebll;
/// <summary>
/// 產品價格
/// </summary>
/// <returns></returns>
public static ProductPriceBLL ProductPrice()
{
if (_productpricebll == null)
{
_productpricebll = new ProductPriceBLL();
}
return _productpricebll;
}
#endregion
#region 商品參照AIPS編號
private static ProductReferenceAIPSBLL _productreferenceaipsbll;
/// <summary>
/// 商品參照AIPS編號
/// </summary>
/// <returns></returns>
public static ProductReferenceAIPSBLL ProductReferenceAIPS()
{
if (_productreferenceaipsbll == null)
{
_productreferenceaipsbll = new ProductReferenceAIPSBLL();
}
return _productreferenceaipsbll;
}
#endregion
#region 產品關係表 
private static ProductRelationSettingBLL _productrelationsettingbll;
/// <summary>
/// 產品關係表 
/// </summary>
/// <returns></returns>
public static ProductRelationSettingBLL ProductRelationSetting()
{
if (_productrelationsettingbll == null)
{
_productrelationsettingbll = new ProductRelationSettingBLL();
}
return _productrelationsettingbll;
}
#endregion
#region 產品主表
private static ProductSkuBLL _productskubll;
/// <summary>
/// 產品主表
/// </summary>
/// <returns></returns>
public static ProductSkuBLL ProductSku()
{
if (_productskubll == null)
{
_productskubll = new ProductSkuBLL();
}
return _productskubll;
}
#endregion
#region 产品SKU与版型的对应表 
private static ProductSkuEditorTemplateReferenceBLL _productskueditortemplatereferencebll;
/// <summary>
/// 产品SKU与版型的对应表 
/// </summary>
/// <returns></returns>
public static ProductSkuEditorTemplateReferenceBLL ProductSkuEditorTemplateReference()
{
if (_productskueditortemplatereferencebll == null)
{
_productskueditortemplatereferencebll = new ProductSkuEditorTemplateReferenceBLL();
}
return _productskueditortemplatereferencebll;
}
#endregion
#region 產品資料對應主表
private static ProductSkuReferenceBLL _productskureferencebll;
/// <summary>
/// 產品資料對應主表
/// </summary>
/// <returns></returns>
public static ProductSkuReferenceBLL ProductSkuReference()
{
if (_productskureferencebll == null)
{
_productskureferencebll = new ProductSkuReferenceBLL();
}
return _productskureferencebll;
}
#endregion
#region 產品資料表 
private static ProductSpuBLL _productspubll;
/// <summary>
/// 產品資料表 
/// </summary>
/// <returns></returns>
public static ProductSpuBLL ProductSpu()
{
if (_productspubll == null)
{
_productspubll = new ProductSpuBLL();
}
return _productspubll;
}
#endregion
#region 
private static ProductSpuPropertySettingBLL _productspupropertysettingbll;
/// <summary>
/// 
/// </summary>
/// <returns></returns>
public static ProductSpuPropertySettingBLL ProductSpuPropertySetting()
{
if (_productspupropertysettingbll == null)
{
_productspupropertysettingbll = new ProductSpuPropertySettingBLL();
}
return _productspupropertysettingbll;
}
#endregion
#region 商品模板表
private static ProductTemplateLibraryBLL _producttemplatelibrarybll;
/// <summary>
/// 商品模板表
/// </summary>
/// <returns></returns>
public static ProductTemplateLibraryBLL ProductTemplateLibrary()
{
if (_producttemplatelibrarybll == null)
{
_producttemplatelibrarybll = new ProductTemplateLibraryBLL();
}
return _producttemplatelibrarybll;
}
#endregion
#region 模板對應分類表
private static ProductTemplateRelationSettingBLL _producttemplaterelationsettingbll;
/// <summary>
/// 模板對應分類表
/// </summary>
/// <returns></returns>
public static ProductTemplateRelationSettingBLL ProductTemplateRelationSetting()
{
if (_producttemplaterelationsettingbll == null)
{
_producttemplaterelationsettingbll = new ProductTemplateRelationSettingBLL();
}
return _producttemplaterelationsettingbll;
}
#endregion
#region 商品模板項目表
private static ProductTemplateVirtualSkuColumnBLL _producttemplatevirtualskucolumnbll;
/// <summary>
/// 商品模板項目表
/// </summary>
/// <returns></returns>
public static ProductTemplateVirtualSkuColumnBLL ProductTemplateVirtualSkuColumn()
{
if (_producttemplatevirtualskucolumnbll == null)
{
_producttemplatevirtualskucolumnbll = new ProductTemplateVirtualSkuColumnBLL();
}
return _producttemplatevirtualskucolumnbll;
}
#endregion
#region 商品模板項目對應屬性
private static ProductTemplateVirtualSkuIDcorrespondBLL _producttemplatevirtualskuidcorrespondbll;
/// <summary>
/// 商品模板項目對應屬性
/// </summary>
/// <returns></returns>
public static ProductTemplateVirtualSkuIDcorrespondBLL ProductTemplateVirtualSkuIDcorrespond()
{
if (_producttemplatevirtualskuidcorrespondbll == null)
{
_producttemplatevirtualskuidcorrespondbll = new ProductTemplateVirtualSkuIDcorrespondBLL();
}
return _producttemplatevirtualskuidcorrespondbll;
}
#endregion
#region 询价管理
private static QuotationBLL _quotationbll;
/// <summary>
/// 询价管理
/// </summary>
/// <returns></returns>
public static QuotationBLL Quotation()
{
if (_quotationbll == null)
{
_quotationbll = new QuotationBLL();
}
return _quotationbll;
}
#endregion
#region 短信标签表
private static SMSLabelBLL _smslabelbll;
/// <summary>
/// 短信标签表
/// </summary>
/// <returns></returns>
public static SMSLabelBLL SMSLabel()
{
if (_smslabelbll == null)
{
_smslabelbll = new SMSLabelBLL();
}
return _smslabelbll;
}
#endregion
#region 短信日志
private static SMSLogBLL _smslogbll;
/// <summary>
/// 短信日志
/// </summary>
/// <returns></returns>
public static SMSLogBLL SMSLog()
{
if (_smslogbll == null)
{
_smslogbll = new SMSLogBLL();
}
return _smslogbll;
}
#endregion
#region 短信模板表
private static SMSTemplateBLL _smstemplatebll;
/// <summary>
/// 短信模板表
/// </summary>
/// <returns></returns>
public static SMSTemplateBLL SMSTemplate()
{
if (_smstemplatebll == null)
{
_smstemplatebll = new SMSTemplateBLL();
}
return _smstemplatebll;
}
#endregion
#region 後台模塊權限列舉表
private static SysLimitBLL _syslimitbll;
/// <summary>
/// 後台模塊權限列舉表
/// </summary>
/// <returns></returns>
public static SysLimitBLL SysLimit()
{
if (_syslimitbll == null)
{
_syslimitbll = new SysLimitBLL();
}
return _syslimitbll;
}
#endregion
#region 後台模塊表
private static SysModuleBLL _sysmodulebll;
/// <summary>
/// 後台模塊表
/// </summary>
/// <returns></returns>
public static SysModuleBLL SysModule()
{
if (_sysmodulebll == null)
{
_sysmodulebll = new SysModuleBLL();
}
return _sysmodulebll;
}
#endregion
#region 後台角色表
private static SysRoleBLL _sysrolebll;
/// <summary>
/// 後台角色表
/// </summary>
/// <returns></returns>
public static SysRoleBLL SysRole()
{
if (_sysrolebll == null)
{
_sysrolebll = new SysRoleBLL();
}
return _sysrolebll;
}
#endregion
#region 後台角色分組表
private static SysRoleGroupBLL _sysrolegroupbll;
/// <summary>
/// 後台角色分組表
/// </summary>
/// <returns></returns>
public static SysRoleGroupBLL SysRoleGroup()
{
if (_sysrolegroupbll == null)
{
_sysrolegroupbll = new SysRoleGroupBLL();
}
return _sysrolegroupbll;
}
#endregion
#region 後台角色個別模塊權限表
private static SysRoleLimitBLL _sysrolelimitbll;
/// <summary>
/// 後台角色個別模塊權限表
/// </summary>
/// <returns></returns>
public static SysRoleLimitBLL SysRoleLimit()
{
if (_sysrolelimitbll == null)
{
_sysrolelimitbll = new SysRoleLimitBLL();
}
return _sysrolelimitbll;
}
#endregion
#region 
private static UploadToAipsBLL _uploadtoaipsbll;
/// <summary>
/// 
/// </summary>
/// <returns></returns>
public static UploadToAipsBLL UploadToAips()
{
if (_uploadtoaipsbll == null)
{
_uploadtoaipsbll = new UploadToAipsBLL();
}
return _uploadtoaipsbll;
}
#endregion
#region 会员地址
private static UserAddrBLL _useraddrbll;
/// <summary>
/// 会员地址
/// </summary>
/// <returns></returns>
public static UserAddrBLL UserAddr()
{
if (_useraddrbll == null)
{
_useraddrbll = new UserAddrBLL();
}
return _useraddrbll;
}
#endregion
#region 验证日志
private static VerifyLogBLL _verifylogbll;
/// <summary>
/// 验证日志
/// </summary>
/// <returns></returns>
public static VerifyLogBLL VerifyLog()
{
if (_verifylogbll == null)
{
_verifylogbll = new VerifyLogBLL();
}
return _verifylogbll;
}
#endregion
#region 网站版型风格档
private static WebSiteTemplateBLL _websitetemplatebll;
/// <summary>
/// 网站版型风格档
/// </summary>
/// <returns></returns>
public static WebSiteTemplateBLL WebSiteTemplate()
{
if (_websitetemplatebll == null)
{
_websitetemplatebll = new WebSiteTemplateBLL();
}
return _websitetemplatebll;
}
#endregion
}
}
