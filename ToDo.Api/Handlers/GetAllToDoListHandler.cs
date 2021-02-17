using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ToDo.Api.Queries;
using ToDo.Core.DomainModels;
using ToDo.Core.Interfaces;
using ToDo.Infrastructure.Identity;
using ToDo.Infrastructure.Resources;

namespace ToDo.Api.Handlers
{
    public class GetAllToDoListHandler : IRequestHandler<GetAllToDoListQuery, List<ToDoListResource>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IToDoListRepository _toDoListRepository;
        private readonly IOperatorUser _operatorUser;
        public GetAllToDoListHandler(IUnitOfWork unitOfWork,
            IToDoListRepository toDoListRepository,
             IOperatorUser operatorUser)
        {
            _unitOfWork = unitOfWork;
            _toDoListRepository = toDoListRepository;
            _operatorUser = operatorUser;
        }
        public async Task<List<ToDoListResource>> Handle(GetAllToDoListQuery request, CancellationToken cancellationToken)
        {
            string userId = _operatorUser.Id;
            var lists = await _toDoListRepository.GetListsAsync(new Guid(userId));
            var result = lists.Select(x => new ToDoListResource
            {
                Id = x.Id,
                Title = x.Title,
                UserId = x.UserId,
                Date = x.CreateTime
            }).ToList();
            return result;
        }
    }
}
