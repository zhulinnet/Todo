
using System;
using System.Collections.Generic;
using System.Text;
using ToDo.Core.Interfaces;

namespace ToDo.Core.DomainModels
{
    public class ToDoList : IEntity
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public string Title { get; set; }
        public DateTime? CreateTime { get; set; }

        public ICollection<ToDoItem> Items { get; set; }
    }
}
