using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo.Infrastructure.Jwt
{
    public interface IJwtService
    {
        /// <summary>
        /// 创建 Token
        /// </summary>
        /// <param name="userId">用户 Id</param>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        string CreateToken(string userId, string userName);
    }
}
