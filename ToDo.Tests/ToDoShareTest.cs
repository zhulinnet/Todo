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
    public class ToDoShareTest
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IToDoListRepository _toDoListRepository;
        private readonly IToDoShareRepository _toDoShareRepository;
        public ToDoShareTest()
        {
            var options = new DbContextOptionsBuilder<ToDoContext>()
      .UseInMemoryDatabase(Guid.NewGuid().ToString())
      .Options;
            var dbContext = new ToDoContext(options);
            _toDoListRepository = new ToDoListRepository(dbContext);
            _toDoShareRepository = new ToDoShareRepository(dbContext);
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

            var shareModel = new ToDoShare();
            shareModel.Id = Guid.NewGuid();
            shareModel.ShareTime = DateTime.Now;
            shareModel.ListId = id1;
            shareModel.UserId = Guid.NewGuid();
            _toDoShareRepository.AddToDoShare(shareModel);
            var result = await _unitOfWork.SaveAsync();
            Assert.True(result);
        }
        [Fact]
        public async Task GetLists()
        {
            await _unitOfWork.SaveAsync();
            var listModel = new ToDoList();
            var id1 = Guid.NewGuid();
            listModel.Id = id1;
            listModel.CreateTime = DateTime.Now;
            listModel.Title = "测试一下";
            listModel.UserId = Guid.NewGuid();
            _toDoListRepository.AddToDoList(listModel);

            var shareUser = Guid.NewGuid();
            var shareId1 = Guid.NewGuid();
            var shareModel = new ToDoShare();
            shareModel.Id = shareId1;
            shareModel.ShareTime = DateTime.Now;
            shareModel.ListId = id1;
            shareModel.UserId = shareUser;
            _toDoShareRepository.AddToDoShare(shareModel);

            var shareId2 = Guid.NewGuid();
            shareModel = new ToDoShare();
            shareModel.Id = shareId2;
            shareModel.ShareTime = DateTime.Now;
            shareModel.ListId = id1;
            shareModel.UserId = Guid.NewGuid();
            _toDoShareRepository.AddToDoShare(shareModel);

            await _unitOfWork.SaveAsync();

            var result = await _toDoShareRepository.GetListsAsync(shareUser);
            Assert.Contains(result, t => t.Id == shareId1);
            Assert.DoesNotContain(result, t => t.Id == shareId2);

        }
    }
}
