using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ToDo.Core.DomainModels;

namespace ToDo.Infrastructure
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
        public DbSet<SysUser> Users { get; set; }
        public DbSet<ToDoList> Lists { get; set; }
        public DbSet<ToDoShare> Shares { get; set; }
        public DbSet<ToDoItem> Items { get; set; }
    }
}
