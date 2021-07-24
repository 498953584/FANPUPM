namespace cn.justwin.Web
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Mail;
    using System.Runtime.CompilerServices;
    using System.Text;

    /// <summary>
    /// 邮件辅助类
    /// </summary>
    public class MailUtility
    {
        /// <summary>
        /// 发送失败执行的方法
        /// </summary>
        public Func<MailMessage, string, int> FailCallback;
        /// <summary>
        /// 发送成功执行的方法
        /// </summary>
        public Func<MailMessage, int> SuccessCallback;

        /// <summary>
        /// 构造方法
        /// </summary>
        public MailUtility()
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="from">发件人</param>
        /// <param name="toList">收件人列表</param>
        /// <param name="subject">主题</param>
        /// <param name="body">正文</param>
        public MailUtility(string from, IList<string> toList, string subject, string body)
        {
            this.From = from;
            this.ToList = toList;
            this.Subject = subject;
            this.Body = body;
        }

        /// <summary>
        /// 添加附件
        /// </summary>
        /// <param name="mail">MailMessage</param>
        /// <param name="attachLst">附件</param>
        private void AddAttachMent(MailMessage mail, IList<string> attachLst)
        {
            foreach (string str in attachLst)
            {
                Attachment item = new Attachment(str) {
                    NameEncoding = Encoding.UTF8
                };
                mail.Attachments.Add(item);
            }
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        public void Send()
        {
            MailMessage mail = new MailMessage();
            try
            {
                mail.From = new MailAddress(this.From);
                foreach (string str in this.ToList)
                {
                    if (!string.IsNullOrWhiteSpace(str.Trim()))
                    {
                        mail.To.Add(str);
                    }
                }
                mail.Subject = this.Subject;
                mail.Body = this.Body;
                if (this.AttachmentLst != null)
                {
                    this.AddAttachMent(mail, this.AttachmentLst);
                }
                using (SmtpClient client = new SmtpClient())
                {
                    client.Host = ConfigHelper.SmtpHost;
                    client.Credentials = new NetworkCredential(ConfigHelper.SmtpId, ConfigHelper.SmtpPwd);
                    client.EnableSsl = false;
                    client.Timeout = 0xf423f;
                    client.Send(mail);
                }
                if (this.SuccessCallback != null)
                {
                    this.SuccessCallback(mail);
                }
            }
            catch (Exception exception)
            {
                if (this.FailCallback != null)
                {
                    this.FailCallback(mail, exception.Message);
                }
                Log4netHelper.Error(exception, "Mail", string.Empty);
            }
        }

        /// <summary>
        /// 附件
        /// </summary>
        public List<string> AttachmentLst { get; set; }

        /// <summary>
        /// 正文
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 发件人
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 收件人列表
        /// </summary>
        public IList<string> ToList { get; set; }
    }
}

