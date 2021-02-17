using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo.Core.DomainModels;
using ToDo.Core.Interfaces;
using ToDo.Infrastructure.Identity;
using ToDo.Infrastructure.Resources;

namespace ToDo.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ToDoListController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IToDoListRepository _toDoListRepository;
        private readonly IMapper _mapper;
        private readonly IOperatorUser _operatorUser;
        public ToDoListController(IUnitOfWork unitOfWork,
            IToDoListRepository toDoListRepository,
            IMapper mapper,
            IOperatorUser operatorUser)
        {
            _unitOfWork = unitOfWork;
            _toDoListRepository = toDoListRepository;
            _mapper = mapper;
            _operatorUser = operatorUser;
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="toDoList"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ToDoListAddResource toDoList)
        {
            if (toDoList == null)
            {
                return BadRequest();
            }
            string userId = _operatorUser.Id;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return new UnprocessableEntityObjectResult(ModelState);
            }
            var toDoModel = _mapper.Map<ToDoList>(toDoList);
            toDoModel.UserId = new Guid(userId);
            toDoModel.Id = Guid.NewGuid();
            toDoModel.CreateTime = DateTime.Now;
            _toDoListRepository.AddToDoList(toDoModel);
            if (!await _unitOfWork.SaveAsync())
            {
                throw new Exception("Error occurred when adding");
            }
            return Ok(toDoList);
        }
        /// <summary>
        /// 获取列表
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
            var lists = await _toDoListRepository.GetListsAsync(new Guid(userId));
            var result = lists.Select(x => new ToDoListResource
            {
                Id = x.Id,
                Title = x.Title,
                UserId = x.UserId,
                Date=x.CreateTime
            });
            return Ok(result);
        }
    }
}