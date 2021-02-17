using System;
using System.Collections.Generic;
using System.Text;
using ToDo.Core.Interfaces;

namespace ToDo.Core.DomainModels
{
    public class ToDoShare : IEntity
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public Guid ListId { get; set; }
        public ToDoList List { get; set; }
        public DateTime? ShareTime { get; set; }
    }
}
