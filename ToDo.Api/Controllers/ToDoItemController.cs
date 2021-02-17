using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ToDo.Core.DomainModels;
using ToDo.Core.Interfaces;
using ToDo.Infrastructure.Identity;
using ToDo.Infrastructure.Resources;

namespace ToDo.Api.Controllers
{
    [Authorize]
    [Route("api/todolist/{listid}/[controller]")]
    [ApiController]
    public class ToDoItemController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IToDoListRepository _toDoListRepository;
        private readonly IToDoItemRepository _toDoItemRepository;
        private readonly IMapper _mapper;
        private readonly IOperatorUser _operatorUser;
        public ToDoItemController(IUnitOfWork unitOfWork,
            IToDoListRepository toDoListRepository,
            IToDoItemRepository toDoItemRepository,
            IMapper mapper,
            IOperatorUser operatorUser)
        {
            _unitOfWork = unitOfWork;
            _toDoListRepository = toDoListRepository;
            _toDoItemRepository = toDoItemRepository;
            _mapper = mapper;
            _operatorUser = operatorUser;
        }
        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="listId"></param>
        /// <param name="toDoItem"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(string listId,[FromBody]ToDoItemAddResource toDoItem)
        {
            if (toDoItem == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return new UnprocessableEntityObjectResult(ModelState);
            }
            if (!await _toDoListRepository.ToDoListExistAsync(new Guid(listId)))
            {
                return NotFound();
            }
            var toDoModel = _mapper.Map<ToDoItem>(toDoItem);
            toDoModel.ListId=new Guid(listId);
            toDoModel.Id = Guid.NewGuid();
            toDoModel.CreateTime = DateTime.Now;
            toDoModel.IsDone = false;
            _toDoItemRepository.AddToDoItem(toDoModel);
            if (!await _unitOfWork.SaveAsync())
            {
                throw new Exception("Error occurred when adding");
            }
            return Ok(toDoItem);
        }
        /// <summary>
        /// 完成项目
        /// </summary>
        /// <param name="listId"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/api/todolist/{listid}/[controller]/done")]
        public async Task<IActionResult> Done(string listId,string itemId)
        {
            if (!await _toDoListRepository.ToDoListExistAsync(new Guid(listId)))
            {
                return NotFound();
            }
            if (string.IsNullOrEmpty(itemId))
            {
                return NotFound();
            }
            _toDoItemRepository.CompleteToDoItem(new Guid(itemId));
            if (!await _unitOfWork.SaveAsync())
            {
                throw new Exception("Error occurred when adding");
            }
            return Ok();
        }
        /// <summary>
        /// 获取项目列表
        /// </summary>
        /// <param name="listId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get(string listId)
        {
            if (string.IsNullOrEmpty(listId))
            {
                return NotFound();
            }
            var lists = await _toDoItemRepository.GetListsAsync(new Guid(listId));

            return Ok(lists);
        }
    }
}