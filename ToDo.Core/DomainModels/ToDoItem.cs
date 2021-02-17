using System;
using System.Collections.Generic;
using System.Text;
using ToDo.Core.Interfaces;

namespace ToDo.Core.DomainModels
{
    public class ToDoItem : IEntity
    {
        public Guid Id { get; set; }

        public Guid ListId { get; set; }
        public ToDoList List { get; set; }

        public bool IsDone { get; set; }

        public string Title { get; set; }

        public DateTime? CreateTime { get; set; }
        public DateTime? CompleteTime { get; set; }
    }
}
