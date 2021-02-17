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
    public class ToDoListRepository : IToDoListRepository
    {
        private readonly ToDoContext _context;

        public ToDoListRepository(ToDoContext context)
        {
            _context = context;
        }
        public void AddToDoList(ToDoList toDoList)
        {
            _context.Lists.Add(toDoList);
        }

        public async Task<ToDoList> GetListById(Guid Id)
        {
            return await _context.Lists.SingleOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<IEnumerable<ToDoList>> GetListsAsync(Guid userId)
        {
            return await _context.Lists.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<bool> ToDoListExistAsync(Guid listid)
        {
            return await _context.Lists.AnyAsync(x => x.Id == listid);
        }
    }
}
