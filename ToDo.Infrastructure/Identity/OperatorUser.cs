using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace ToDo.Infrastructure.Identity
{
    /// <summary>
    /// 当前操作人
    /// </summary>
    public class OperatorUser : IOperatorUser
    {
        /// <summary>
        /// HttpContext 上下文
        /// </summary>
        private readonly IHttpContextAccessor _httpContext;

        public OperatorUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor;
        }

        private ClaimsPrincipal ClaimUser
        {
            get
            {
                if (_httpContext == null || _httpContext.HttpContext == null)
                {
                    return null;
                }
                return _httpContext.HttpContext.User;
            }
        }

        /// <summary>
        /// 当前操作人唯一标识
        /// </summary>
        public string Id
        {
            get
            {
                if (ClaimUser == null)
                {
                    return string.Empty;
                }
                var claim = ClaimUser.FindFirst(CustomClaimTypes.UserId);
                return claim == null ? string.Empty : claim.Value;
            }
        }

        /// <summary>
        /// 用户名/账号
        /// </summary>
        public string Account
        {
            get
            {
                if (ClaimUser == null)
                {
                    return string.Empty;
                }
                var claim = ClaimUser.FindFirst(CustomClaimTypes.Account);
                return claim == null ? string.Empty : claim.Value;
            }
        }
    }
}
