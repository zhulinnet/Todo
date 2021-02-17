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
    public class ToDoShareRepository : IToDoShareRepository
    {
        private readonly ToDoContext _context;

        public ToDoShareRepository(ToDoContext context)
        {
            _context = context;
        }
        public void AddToDoShare(ToDoShare toDoShare)
        {
           _context.Shares.Add(toDoShare);
        }

        public async Task<IEnumerable<ToDoShare>> GetListsAsync(Guid userId)
        {
            return await _context.Shares.Include(x=>x.List).Where(x => x.UserId == userId).ToListAsync();
        }
    }
}
