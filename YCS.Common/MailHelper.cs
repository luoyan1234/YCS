using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace YCS.Common
{
    /// <summary>
    /// 邮件类
    /// </summary>
    public class MailHelper
    {

        /// <summary>
        /// 邮件模板地址
        /// </summary>
        public const string MailTemplatePath = "/Template/Mail.xml";

        #region 使用System.Net.Mail发送邮件
        /// <summary>
        /// 使用System.Net.Mail发送邮件
        /// </summary>
        /// <param name="SmtpServer">SMTP服务器</param>
        /// <param name="UserName">用户名</param>
        /// <param name="Password">密码</param>
        /// <param name="ReceiveAddress">收信地址</param>
        /// <param name="CcAddress">抄送地址</param>
        /// <param name="BccAddress">暗送地址</param>
        /// <param name="Subject">邮件主题</param>
        /// <param name="MailBody">邮件内容</param>
        /// <param name="Attachment">附件地址</param>
        /// <param name="IsHTML">是否发送HTML邮件</param>
        /// <param name="IsSSL">是否需要服务器验证</param>
        public static void SendMail(string SmtpServer, string UserName, string Password, string ReceiveAddress, string CcAddress, string BccAddress, string Subject, string MailBody, string Attachment, bool IsHTML, bool IsSSL)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(UserName);
            mail.To.Add(ReceiveAddress);
            if (CcAddress != string.Empty) mail.CC.Add(CcAddress);
            if (BccAddress != string.Empty) mail.Bcc.Add(BccAddress);
            if (File.Exists(Attachment) == true) mail.Attachments.Add(new Attachment(Attachment));
            mail.Subject = Subject;
            mail.IsBodyHtml = IsHTML;
            mail.Body = MailBody;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = SmtpServer;
            smtp.Credentials = new NetworkCredential(UserName, Password);
            smtp.EnableSsl = IsSSL;
            smtp.Send(mail);

        }
        #endregion

    }
}
