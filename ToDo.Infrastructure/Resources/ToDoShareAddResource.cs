using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ToDo.Infrastructure.Resources
{
    public class ToDoShareAddResource
    {
        [Required]
        public string Account { get; set; }
        public Guid ListId { get; set; }
    }
}
