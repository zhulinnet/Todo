using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace ToDo.Infrastructure.Mail
{
    public class MailService : IMailService
    {
        public IConfiguration _configuration { get; }
        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void send(MailOptions mails)
        {
            string fromPerson = _configuration["SMTP:mail"];
            string host = _configuration["SMTP:host"];
            //将发件人邮箱带入MailAddress中初始化
            MailAddress mailAddress = new MailAddress(fromPerson);
            //创建Email的Message对象
            MailMessage mailMessage = new MailMessage();

            //判断收件人数组中是否有数据
            if (mails.recipientArry.Any())
            {
                //循环添加收件人地址
                foreach (var item in mails.recipientArry)
                {
                    if (!string.IsNullOrEmpty(item))
                        mailMessage.To.Add(item.ToString());
                }
            }
            //发件人邮箱
            mailMessage.From = mailAddress;
            //标题
            mailMessage.Subject = mails.mailTitle;
            //编码
            mailMessage.SubjectEncoding = Encoding.UTF8;
            //正文
            mailMessage.Body = mails.mailBody;
            //正文编码
            mailMessage.BodyEncoding = Encoding.Default;
            //邮件优先级
            mailMessage.Priority = MailPriority.High;
            //正文是否是html格式
            mailMessage.IsBodyHtml = mails.isbodyHtml;
            //取得Web根目录和内容根目录的物理路径
            string webRootPath = string.Empty;

            //实例化一个Smtp客户端
            SmtpClient smtp = new SmtpClient();
            //将发件人的邮件地址和客户端授权码带入以验证发件人身份
            smtp.Credentials = new System.Net.NetworkCredential(fromPerson, _configuration["SMTP:code"]);
            //指定SMTP邮件服务器
            smtp.Host = host;

            //邮件发送到SMTP服务器
            smtp.Send(mailMessage);
        }
    }
}
