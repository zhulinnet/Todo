using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToDo.Core.DomainModels;

namespace ToDo.Core.Interfaces
{
    public interface IToDoListRepository
    {
        void AddToDoList(ToDoList toDoList);
        Task<IEnumerable<ToDoList>> GetListsAsync(Guid userId);
        Task<ToDoList> GetListById(Guid Id);
        Task<bool> ToDoListExistAsync(Guid Id);
    }
}
