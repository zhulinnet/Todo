using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo.Core.DomainModels;
using ToDo.Core.Interfaces;
using ToDo.Infrastructure.Identity;
using ToDo.Infrastructure.Mail;
using ToDo.Infrastructure.Resources;

namespace ToDo.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoShareController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISysUserRepository _sysUserRepository;
        private readonly IToDoListRepository _toDoListRepository;
        private readonly IToDoShareRepository _toDoShareRepository;
        private readonly IMapper _mapper;
        private readonly IOperatorUser _operatorUser;
        private readonly IMailService _mailService;
        public ToDoShareController(IUnitOfWork unitOfWork,
            ISysUserRepository sysUserRepository,
            IToDoListRepository toDoListRepository,
            IToDoShareRepository toDoShareRepository,
            IMapper mapper,
            IOperatorUser operatorUser,
            IMailService mailService)
        {
            _unitOfWork = unitOfWork;
            _sysUserRepository = sysUserRepository;
            _toDoListRepository = toDoListRepository;
            _toDoShareRepository = toDoShareRepository;
            _mapper = mapper;
            _operatorUser = operatorUser;
            _mailService = mailService;
        }
        /// <summary>
        /// 新增分享
        /// </summary>
        /// <param name="toDoShare"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ToDoShareAddResource toDoShare)
        {
            if (toDoShare == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return new UnprocessableEntityObjectResult(ModelState);
            }
            var userModel = await _sysUserRepository.GetUserByAccountAync(toDoShare.Account);
            if (userModel == null)
            {
                userModel = await  _sysUserRepository.GetUserByEmailAync(toDoShare.Account);
            }
            if (userModel == null)
            {
                return NotFound("用户不存在或已删除");
            }
            var listModel = await _toDoListRepository.GetListById(toDoShare.ListId);
            if (listModel == null)
            {
                return NotFound("内容不存在或已删除");
            }
            if (userModel.Id.Equals(listModel.UserId))
            {
                return BadRequest("不能分享给自己");
            }
            var toDoModel =new  ToDoShare();
            toDoModel.Id = Guid.NewGuid();
            toDoModel.ListId = listModel.Id;
            toDoModel.UserId = userModel.Id;
            toDoModel.ShareTime = DateTime.Now;
            _toDoShareRepository.AddToDoShare(toDoModel);
            if (!await _unitOfWork.SaveAsync())
            {
                throw new Exception("Error occurred when adding");
            }
            _mailService.send(new MailOptions() { isbodyHtml=false,mailBody=$"有用户分享了待办列表《{listModel.Title}》给你",mailTitle="待办列表分享",recipientArry= new string[1] { userModel.Email } });
            return Ok(toDoShare);
        }
        /// <summary>
        /// 获取分享列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string userId = _operatorUser.Id;
            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }
            var lists = await _toDoShareRepository.GetListsAsync(new Guid(userId));
            var result = lists.Select(x=>new ToDoListResource
            {
                Id=x.List.Id,
                Title=x.List.Title,
                UserId=x.List.UserId,
                Date=x.ShareTime
            });
            return Ok(result);
        }
    }
}