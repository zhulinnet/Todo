using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo.Core.Interfaces
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
