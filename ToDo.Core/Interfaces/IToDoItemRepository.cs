using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToDo.Core.DomainModels;

namespace ToDo.Core.Interfaces
{
    public interface IToDoItemRepository
    {
        void AddToDoItem(ToDoItem toDoShare);
        void CompleteToDoItem(Guid itemId);
        Task<IEnumerable<ToDoItem>> GetListsAsync(Guid listId);
        Task<ToDoItem> GeItemByIdAsync(Guid id);
    }
}
