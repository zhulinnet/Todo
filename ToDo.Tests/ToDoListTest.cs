using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToDo.Core.DomainModels;
using ToDo.Core.Interfaces;
using ToDo.Infrastructure;
using ToDo.Infrastructure.Repositories;
using Xunit;

namespace ToDo.Tests
{
    public class ToDoListTest
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IToDoListRepository _toDoListRepository;
        public ToDoListTest()
        {
            var options = new DbContextOptionsBuilder<ToDoContext>()
      .UseInMemoryDatabase(Guid.NewGuid().ToString())
      .Options;
            var dbContext = new ToDoContext(options);
            _toDoListRepository = new ToDoListRepository(dbContext);
            _unitOfWork = new UnitOfWork(dbContext);
        }
        [Fact]
        public async Task Add()
        {
            var listModel = new ToDoList();
            listModel.Id = Guid.NewGuid();
            listModel.CreateTime = DateTime.Now;
            listModel.Title = "测试一下";
            listModel.UserId = Guid.NewGuid();
            _toDoListRepository.AddToDoList(listModel);
            var result = await _unitOfWork.SaveAsync();
            Assert.True(result);
        }
        [Fact]
        public async Task GetLists()
        {
            var userID = Guid.NewGuid();
            await _unitOfWork.SaveAsync();
            var listModel = new ToDoList();
            var id1 = Guid.NewGuid();
            listModel.Id = id1;
            listModel.CreateTime = DateTime.Now;
            listModel.Title = "测试一下";
            listModel.UserId = userID;
            _toDoListRepository.AddToDoList(listModel);
            await _unitOfWork.SaveAsync();

            listModel= new ToDoList();
            var id2 = Guid.NewGuid();
            listModel.Id = id2;
            listModel.CreateTime = DateTime.Now;
            listModel.Title = "测试一下2";
            listModel.UserId = userID;
            _toDoListRepository.AddToDoList(listModel);
            await _unitOfWork.SaveAsync();

            listModel = new ToDoList();
            var id3 = Guid.NewGuid();
            listModel.Id = id3;
            listModel.CreateTime = DateTime.Now;
            listModel.Title = "测试一下3";
            listModel.UserId = Guid.NewGuid();
            _toDoListRepository.AddToDoList(listModel);
            await _unitOfWork.SaveAsync();
            var result = await _toDoListRepository.GetListsAsync(userID);
            Assert.Contains(result, t => t.Id== id1);
            Assert.Contains(result, t => t.Id == id2);
            Assert.DoesNotContain(result, t => t.Id ==Guid.NewGuid());
            Assert.DoesNotContain(result, t => t.Id == id3);
        }

    }
}
