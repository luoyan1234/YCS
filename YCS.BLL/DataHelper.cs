using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YCS.Common;
using YCS.Model;

namespace YCS.BLL
{
    public class DataHelper
    {
       
        #region 提交支付成功事务(主站/分站)
        /// <summary>
        /// 提交支付成功事务
        /// </summary>
        public bool SubmitOrderPay(string PayNo, int PayType, decimal ActualPayAmount)
        {
            List<object> listOut = new List<object>();
            //执行事务
            if (DeleHelpler.RunTrans(SubmitOrderPayTrans, new List<object>() { PayNo, PayType, ActualPayAmount }, out listOut))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 提交支付成功事务
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="listInput"></param>
        private void SubmitOrderPayTrans(SqlTransaction trans, List<object> listInput, out List<object> listOut)
        {
            //获取参数
            string PayNo = (string)listInput[0];
            int PayType = (int)listInput[1];
            decimal ActualPayAmount = (decimal)listInput[2];
            //事务处理
            OrderModel ordModel = Factory.Order().GetModelByOrderNo(trans, PayNo);
            if (ordModel != null && ordModel.Status == EnumList.OrderStatus.待付款.ToInt())
            {
                //支付状态
              //  ordModel.PaymentType = (short)PayType;
                ordModel.ActualPayAmount = ActualPayAmount;
                ordModel.PaymentDate = DateTime.Now;
                ordModel.LastUpdateDate = DateTime.Now;
                //判断商品是Type:1,在线自助设计订制流程;2,设计文件下载订制流程
                //Order表订单状态
                //判断订单是paymentType： 1,支付宝支付;2,微信支付;3,货到付款
                if (ordModel.PaymentType == (short)EnumList.PayType.货到付款) 
                {
                    ordModel.Status = (short)EnumList.OrderStatus.已完成.ToInt();
                }
                else
                {
                    if (ordModel.Type == (EnumList.OrderStatus.待上传设计文件.ToInt()).ToString())
                    {
                        ordModel.Status = (short)EnumList.OrderStatus.待上传设计文件.ToInt();
                    }
                    else
                    {
                        ordModel.Status = (short)EnumList.OrderStatus.待发货.ToInt();
                    }
                }
                //OrderItem订单明细表状态
                List<OrderItemModel> orderItemList = Factory.OrderItem().GetModels(trans, ordModel.OrderId);
                if (orderItemList != null && orderItemList.Count > 0)
                {
                    foreach (OrderItemModel item in orderItemList)
                    {
                        item.Status = ordModel.Status;
                    }
                }
                
                //更新订单表
                Factory.Order().UpdateInfo(trans, ordModel, ordModel.OrderId);
                //更新订单明细表
                if (orderItemList != null && orderItemList.Count > 0)
                {
                    foreach (OrderItemModel item in orderItemList)
                    {
                        Factory.OrderItem().UpdateInfo(trans,item,item.SN);//更新订单明细
                    }
                }
           
                //更新订单历史记录状态
                OrderHistoryModel orderHistoryModel = Factory.OrderHistory().GetModelByOrderId(trans, ordModel.OrderId);
                orderHistoryModel.NewValue = Config.GetEnumDesc(typeof(EnumList.OrderStatus), ordModel.Status);
                Factory.OrderHistory().UpdateInfo(trans, orderHistoryModel, orderHistoryModel.SN);

                ////更新标准商品pdf
                //Factory.OrderProduct().UpdateStandardPDF(trans, ordModel.OrderID);

                //复制标准商品pdf
                try
                {
                    //foreach (var item in Factory.Product().GetModelsForStandardPDF(trans, ordModel.OrderID))
                    //{
                    //    string strTimeDir = DateTime.Now.ToString("yyyyMMdd");
                    //    string strFolderPath = Config.FileUploadPath + "Order/" + Config.UpLoadFile + "/" + strTimeDir + "/";
                    //    string strUploadFile = IOHelper.FileCopy(item.PdfFile, strFolderPath, "");
                    //}
                }
                catch (Exception ex)
                {

                    Config.Err(ex);
                }

                //提成记录
                //AgentModel ageModel = Factory.Agent().GetInfo(trans, ordModel.AgentID);
                //if (ageModel != null)
                //{
                //    Factory.OrderDivided().InsertLog(trans, ordModel, ageModel.Percentage);
                //}

           
                

                //消费记录
             //   Factory.TransactionDetails().InsertLog(trans, ordModel, false);

                //用户日志
                Factory.MemberEventLog().InsertLog(trans, ordModel.DistributorId, EnumList.MemberEventLogType.Pay, "支付ID为" + ordModel.OrderId + "的订单!");

            }

            //返回值
            listOut = new List<object>() { };
        }
        #endregion

       
    }
}
