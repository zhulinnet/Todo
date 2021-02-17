using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo.Infrastructure.Identity
{
    /// <summary>
    /// 自定义 Claim 属性类型
    /// </summary>
    public partial class CustomClaimTypes
    {
        /// <summary>
        /// 用户 ID
        /// </summary>
        public const string UserId = "userID";

        /// <summary>
        /// 用户名
        /// </summary>
        public const string Account = "account";
    }
}
