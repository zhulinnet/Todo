using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo.Infrastructure.Identity
{
    /// <summary>
    /// 当前操作人
    /// </summary>
    public interface IOperatorUser
    {
        /// <summary>
        /// 当前操作人唯一标识
        /// </summary>
        string Id { get; }

        /// <summary>
        /// 用户名/账号
        /// </summary>
        string Account { get; }
    }
}
