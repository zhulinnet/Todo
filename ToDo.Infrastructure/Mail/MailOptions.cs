using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo.Infrastructure.Mail
{
    public class MailOptions
    {
        /// <summary>
        /// 收件人地址(多人)
        /// </summary>
        public string[] recipientArry { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string mailTitle { get; set; }

        /// <summary>
        /// 正文
        /// </summary>
        public string mailBody { get; set; }

        /// <summary>
        /// 正文是否是html格式
        /// </summary>
        public bool isbodyHtml { get; set; }
    }
}
