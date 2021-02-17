using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToDo.Core.DomainModels;

namespace ToDo.Core.Interfaces
{
    public interface ISysUserRepository
    {
        void AddUser(SysUser sysUser);
        Task<SysUser> GetUserByIdAsync(Guid id);
        Task<SysUser> GetUserByEmailAync(string email);
        Task<SysUser> GetUserByAccountAync(string account);
        Task<SysUser> UserLoginAync(string account,string password);
    }
}
