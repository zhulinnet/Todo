using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.Core.DomainModels;
using ToDo.Infrastructure.Resources;

namespace ToDo.Api.Queries
{
    public class GetAllToDoListQuery: IRequest<List<ToDoListResource>>
    {
    }
}
