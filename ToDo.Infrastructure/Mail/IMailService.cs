using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo.Infrastructure.Mail
{
    public interface IMailService
    {
        void send(MailOptions mail);
    }
}
