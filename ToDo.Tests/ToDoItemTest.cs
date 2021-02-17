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
    public class ToDoItemTest
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IToDoListRepository _toDoListRepository;
        private readonly IToDoItemRepository _toDoItemRepository;
        public ToDoItemTest()
        {
            var options = new DbContextOptionsBuilder<ToDoContext>()
      .UseInMemoryDatabase(Guid.NewGuid().ToString())
      .Options;
            var dbContext = new ToDoContext(options);
            _toDoListRepository = new ToDoListRepository(dbContext);
            _toDoItemRepository = new ToDoItemRepository(dbContext);
            _unitOfWork = new UnitOfWork(dbContext);
        }
        [Fact]
        public async Task Add()
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

            var itemModel = new ToDoItem();
            itemModel.Id = Guid.NewGuid();
            itemModel.CreateTime = DateTime.Now;
            itemModel.ListId = id1;
            itemModel.Title = "项目1";
            itemModel.IsDone = false;
            _toDoItemRepository.AddToDoItem(itemModel);
            var result = await _unitOfWork.SaveAsync();
            Assert.True(result);
        }
        [Fact]
        public async Task Complete()
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

            var itemModel = new ToDoItem();
            itemModel.Id = Guid.NewGuid();
            itemModel.CreateTime = DateTime.Now;
            itemModel.ListId = id1;
            itemModel.Title = "项目1";
            itemModel.IsDone = false;
            _toDoItemRepository.AddToDoItem(itemModel);
            await _unitOfWork.SaveAsync();

            _toDoItemRepository.CompleteToDoItem(itemModel.Id);
            await _unitOfWork.SaveAsync();

            itemModel = await _toDoItemRepository.GeItemByIdAsync(itemModel.Id);
            Assert.True(itemModel.IsDone);
        }
        [Fact]
        public async Task GeItemById()
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

            var itemModel = new ToDoItem();
            itemModel.Id = Guid.NewGuid();
            itemModel.CreateTime = DateTime.Now;
            itemModel.ListId = id1;
            itemModel.Title = "项目1";
            itemModel.IsDone = false;
            _toDoItemRepository.AddToDoItem(itemModel);
            await _unitOfWork.SaveAsync();

            itemModel = await _toDoItemRepository.GeItemByIdAsync(itemModel.Id);
            Assert.NotNull(itemModel);
            itemModel = await _toDoItemRepository.GeItemByIdAsync(Guid.NewGuid());
            Assert.Null(itemModel);
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

            var itemModel = new ToDoItem();
            itemModel.Id = Guid.NewGuid();
            itemModel.CreateTime = DateTime.Now;
            itemModel.ListId = id1;
            itemModel.Title = "项目1";
            itemModel.IsDone = false;
            _toDoItemRepository.AddToDoItem(itemModel);
            await _unitOfWork.SaveAsync();

            var result = await _toDoItemRepository.GetListsAsync(id1);
            Assert.Contains(result, t => t.Id == itemModel.Id);
            Assert.DoesNotContain(result, t => t.Id == Guid.NewGuid());
        }
    }
}
