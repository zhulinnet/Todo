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
    public class SysUserRepository : ISysUserRepository
    {
        private readonly ToDoContext _context;

        public SysUserRepository(ToDoContext context)
        {
            _context = context;
        }
        public void AddUser(SysUser sysUser)
        {
            _context.Users.Add(sysUser);
        }

        public async Task<SysUser> GetUserByAccountAync(string account)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Account == account);
        }

        public async Task<SysUser> GetUserByEmailAync(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Email == email);
        }

        public async Task<SysUser> GetUserByIdAsync(Guid id)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<SysUser> UserLoginAync(string account, string password)
        {
            return await _context.Users.SingleOrDefaultAsync(x => (x.Account == account&&x.PassWord== password) || (x.Email == account && x.PassWord == password));
        }
    }
}
