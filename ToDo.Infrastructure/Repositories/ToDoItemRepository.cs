using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Core.DomainModels;
using ToDo.Core.Interfaces;

namespace ToDo.Infrastructure.Repositories
{
    public class ToDoItemRepository : IToDoItemRepository
    {
        private readonly ToDoContext _context;

        public ToDoItemRepository(ToDoContext context)
        {
            _context = context;
        }
        public void AddToDoItem(ToDoItem toDoShare)
        {
            _context.Items.Add(toDoShare);
        }

        public void CompleteToDoItem(Guid itemId)
        {
            var item = _context.Items.SingleOrDefault(x=>x.Id== itemId);
            item.IsDone = true;
            _context.Items.Update(item);
        }

        public async Task<ToDoItem> GeItemByIdAsync(Guid id)
        {
            return await _context.Items.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<ToDoItem>> GetListsAsync(Guid listId)
        {
            return await _context.Items.Where(x => x.ListId== listId).ToListAsync();
        }
    }
}
