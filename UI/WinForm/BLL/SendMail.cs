using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Net.Mime;

namespace SFIS_V2
{
    public class MyGMail
    {

        private MailMessage mailMessage;
        private SmtpClient smtpClient;
        private string password;//发件人密码  
        /**/
        /// <summary>  
        /// 处审核后类的实例  
        /// </summary>  
        /// <param name="To">收件人地址</param>  
        /// <param name="From">发件人地址</param>  
        /// <param name="Body">邮件正文</param>  
        /// <param name="Title">邮件的主题</param>  
        /// <param name="Password">发件人密码</param>  
        public void SendMail(string To, string From, string Body, string Title, string Password)
        {
            mailMessage = new MailMessage();
            mailMessage.To.Add(To);
            mailMessage.From = new System.Net.Mail.MailAddress(From);
            mailMessage.Subject = Title;
            mailMessage.Body = Body;
            mailMessage.IsBodyHtml = true;
            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            mailMessage.Priority = System.Net.Mail.MailPriority.Normal;
            this.password = Password;
        }

        /**/
        /// <summary>  
        /// 添加附件  
        /// </summary>  
        public void Attachments(string Path)
        {
            string[] path = Path.Split(',');
            Attachment data;
            ContentDisposition disposition;
            for (int i = 0; i < path.Length; i++)
            {
                data = new Attachment(path[i], MediaTypeNames.Application.Octet);//实例化附件  
                disposition = data.ContentDisposition;
                disposition.CreationDate = System.IO.File.GetCreationTime(path[i]);//获取附件的创建日期  
                disposition.ModificationDate = System.IO.File.GetLastWriteTime(path[i]);//获取附件的修改日期  
                disposition.ReadDate = System.IO.File.GetLastAccessTime(path[i]);//获取附件的读取日期  
                mailMessage.Attachments.Add(data);//添加到附件中  
            }
        }

        /**/
        /// <summary>  
        /// 异步发送邮件  
        /// </summary>  
        /// <param name="CompletedMethod"></param>  
        public void SendAsync(SendCompletedEventHandler CompletedMethod)
        {
            if (mailMessage != null)
            {
                smtpClient = new SmtpClient();
                smtpClient.Credentials = new System.Net.NetworkCredential(mailMessage.From.Address, password);//设置发件人身份的票据  
                smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtpClient.Host = "smtp." + mailMessage.From.Host;
                smtpClient.SendCompleted += new SendCompletedEventHandler(CompletedMethod);//注册异步发送邮件完成时的事件  
                smtpClient.SendAsync(mailMessage, mailMessage.Body);
            }
        }

        /**/
        /// <summary>  
        /// 发送邮件  
        /// </summary>  
        public void Send()
        {
            if (mailMessage != null)
            {
                smtpClient = new SmtpClient();
                smtpClient.Credentials = new System.Net.NetworkCredential(mailMessage.From.Address, password);//设置发件人身份的票据  
                smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtpClient.Host = "smtp." + mailMessage.From.Host;
                smtpClient.Send(mailMessage);
            }
        }

        private void btnSendMail_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    this.SendMail("fgzcard:21cn.com", "lmm4227261:126.com", this.textBox1.Text.Trim(), "你好", "6693065");
            //    this.Send();
            //    MessageBox.Show("发送成功！");
            //}
            //catch (Exception myException)
            //{
            //    MessageBox.Show(myException.ToString());
            //}
        }

    }
    public class ExchangeMail
    {
        private MailMessage msg = new MailMessage();
        #region 属性
        private List<string> mToMailAddress = new List<string>();
        private List<string> mccMailAddress = new List<string>();
        /// <summary>
        /// 接受邮件地址列表
        /// </summary>
        public List<string> ToMailAddress
        {
            get { return mToMailAddress; }
            set { mToMailAddress = value; }
        }
        /// <summary>
        /// 抄送邮件地址列表
        /// </summary>
        public List<string> ccMailAddress
        {
            get { return mccMailAddress; }
            set { mccMailAddress = value; }
        }
        /// <summary>
        /// 发送邮件账户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 发送邮件账户密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 账户域
        /// </summary>
        public string Domain { get; set; }
        /// <summary>
        /// 发送邮件的邮件地址
        /// </summary>
        public string UserMail { get; set; }
        /// <summary>
        /// 服务器地址
        /// </summary>
        public string ExchangeHost { get; set; }
        /// <summary>
        /// 邮件标题
        /// </summary>
        public string Mailtitle { get; set; }
        /// <summary>
        /// 邮件内容
        /// </summary>
        public string MailBody { get; set; }
        #endregion
        /// <summary>
        /// 发送邮件
        /// </summary>
        public void SendMail()
        {
            foreach (string tomail in this.ToMailAddress)
            {
                msg.To.Add(tomail);
            }
            foreach (string ccmail in this.ccMailAddress)
            {
                msg.CC.Add(ccmail);
            }

            msg.From = new MailAddress(this.UserMail, this.UserName, System.Text.Encoding.UTF8);
            msg.Subject = this.Mailtitle;
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            msg.Body = this.MailBody;
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = false;
            msg.Priority = MailPriority.High;

            SmtpClient sc = new SmtpClient();
            sc.Host = this.ExchangeHost;
            sc.Port = 25;
            sc.UseDefaultCredentials = false;
            sc.Credentials = new System.Net.NetworkCredential(this.UserName, this.Password, this.Domain);
            sc.EnableSsl = false;
            sc.DeliveryMethod = SmtpDeliveryMethod.Network;

            object userState = msg;

            try
            {
                sc.Send(msg);
                //sc.SendAsync(msg, userState);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 添加邮件附件
        /// </summary>
        /// <param name="Path">附件路径</param>
        public void Attachments(string Path)
        {
            string[] path = Path.Split(',');
            Attachment data;
            ContentDisposition disposition;
            for (int i = 0; i < path.Length; i++)
            {
                data = new Attachment(path[i], MediaTypeNames.Application.Octet);//实例化附件  
                disposition = data.ContentDisposition;
                disposition.CreationDate = System.IO.File.GetCreationTime(path[i]);//获取附件的创建日期  
                disposition.ModificationDate = System.IO.File.GetLastWriteTime(path[i]);//获取附件的修改日期  
                disposition.ReadDate = System.IO.File.GetLastAccessTime(path[i]);//获取附件的读取日期  
                msg.Attachments.Add(data);//添加到附件中  
            }
        }


        public static void SendMail(List<string> _ToMail, List<string> _ccMail, string _attachments)
        {
            ExchangeMail mm = new ExchangeMail()
            {
                Password = "Yanghc123456",
                UserName = "huchun.yang",
                Domain = "freecomm",
                UserMail = "huchun.yang:feixun.com.cn",
                ExchangeHost = "172.16.100.40",
                MailBody = "FQC检验记录表\n该邮件由系统自动发送,请不要回复",
                Mailtitle = "FQC检验记录表",
                ToMailAddress = _ToMail,
                ccMailAddress = _ccMail
            };
            mm.Attachments(_attachments);
            mm.SendMail();
        }

    }
}
