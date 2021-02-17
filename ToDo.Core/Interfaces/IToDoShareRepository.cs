using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToDo.Core.DomainModels;

namespace ToDo.Core.Interfaces
{
    public interface IToDoShareRepository
    {
        void AddToDoShare(ToDoShare toDoShare);
        Task<IEnumerable<ToDoShare>> GetListsAsync(Guid userId);
    }
}
