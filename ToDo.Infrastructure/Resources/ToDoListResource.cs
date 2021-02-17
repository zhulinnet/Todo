using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo.Infrastructure.Resources
{
    public class ToDoListResource
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public string Title { get; set; }
        public DateTime? Date { get; set; }
    }
}
