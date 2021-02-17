using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.Core.DomainModels;
using ToDo.Infrastructure.Resources;

namespace ToDo.Api.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SysUser, UserAddResource>();
            CreateMap<UserAddResource, SysUser>();
            CreateMap<SysUser, UserResource>();
            CreateMap<UserResource, SysUser>();
            CreateMap<ToDoList, ToDoListAddResource>();
            CreateMap<ToDoListAddResource, ToDoList>();
            CreateMap<ToDoShare, ToDoShareAddResource>();
            CreateMap<ToDoShareAddResource, ToDoShare>();
            CreateMap<ToDoItem, ToDoItemAddResource>();
            CreateMap<ToDoItemAddResource, ToDoItem>();
        }
    }
}
