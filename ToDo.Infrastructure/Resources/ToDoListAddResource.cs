using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ToDo.Infrastructure.Resources
{
    public class ToDoListAddResource
    {
        [Required,MaxLength(50)]
        public string Title { get; set; }
    }
}
