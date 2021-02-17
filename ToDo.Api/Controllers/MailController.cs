using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo.Infrastructure.Mail;

namespace ToDo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMailService _mailService;
        public MailController(IMailService mailService)
        {
            _mailService = mailService;
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="mails"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody]MailOptions mails)
        {
            _mailService.send(mails);
            return Ok();
        }
    }
}