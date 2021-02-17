using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ToDo.Infrastructure.Resources
{
    public class UserAddResource
    {
        [Required]
        public string Account { get; set; }
        [Required, MaxLength(20)]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required, MaxLength(18)]
        public string PassWord { get; set; }
    }
}
