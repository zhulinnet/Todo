﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo.Infrastructure.Resources
{
    public class UserResource
    {
        public Guid Id { get; set; }
        public string Account { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PassWord { get; set; }
        public DateTime? CreateTime { get; set; }
        public string Token { get; set; }
    }
}
