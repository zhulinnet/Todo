using System;
using System.Collections.Generic;
using System.Text;
using ToDo.Core.Interfaces;

namespace ToDo.Core.DomainModels
{
    public class SysUser : IEntity
    {
        public Guid Id { get; set; }
        public string Account { get; set; }
        public string Name { get; set; }
        public string  Email { get; set; }
        public string  PassWord { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
