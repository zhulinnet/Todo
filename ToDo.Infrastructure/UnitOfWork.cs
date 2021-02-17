using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToDo.Core.Interfaces;

namespace ToDo.Infrastructure
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly ToDoContext _context;

        public UnitOfWork(ToDoContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
