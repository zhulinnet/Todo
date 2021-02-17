using Microsoft.EntityFrameworkCore;
using System;
using ToDo.Core.DomainModels;
using ToDo.Core.Interfaces;
using ToDo.Infrastructure;
using ToDo.Infrastructure.Repositories;
using Xunit;
using ToDo.Infrastructure.Extensions;
using System.Threading.Tasks;

namespace ToDo.Tests
{
    public class AccountTest
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISysUserRepository _userRepository;
        public AccountTest()
        {
            var options = new DbContextOptionsBuilder<ToDoContext>()
      .UseInMemoryDatabase(Guid.NewGuid().ToString())
      .Options;
            var dbContext = new ToDoContext(options);
            _userRepository = new SysUserRepository(dbContext);
            _unitOfWork = new UnitOfWork(dbContext);
        }
        [Fact]
        public async Task Add()
        {
            var userModel = new SysUser();
            userModel.Id = Guid.NewGuid();
            userModel.CreateTime = DateTime.Now;
            userModel.Account = "lisi";
            userModel.Email = "lisi@qq.com";
            userModel.PassWord = "123456".ToMd5Caps16();
            _userRepository.AddUser(userModel);
            var result = await _unitOfWork.SaveAsync();
            Assert.True(result);
        }
        [Fact]
        public async Task GetUserById()
        {
            var userModel = new SysUser();
            userModel.Id = Guid.NewGuid();
            userModel.CreateTime = DateTime.Now;
            userModel.Account = "zhangsan";
            userModel.Email = "zhangsan@qq.com";
            userModel.PassWord = "123456".ToMd5Caps16();
            _userRepository.AddUser(userModel);
            await _unitOfWork.SaveAsync();
            var resultTrue = await _userRepository.GetUserByIdAsync(userModel.Id);
            Assert.True(resultTrue != null);
            var resultFalse= await _userRepository.GetUserByIdAsync(Guid.NewGuid());
            Assert.False(resultFalse != null);
        }
        [Fact]
        public async Task GetUserByEmail()
        {
            var userModel = new SysUser();
            userModel.Id = Guid.NewGuid();
            userModel.CreateTime = DateTime.Now;
            userModel.Account = "aruan";
            userModel.Email = "aruan@qq.com";
            userModel.PassWord = "123456".ToMd5Caps16();
            _userRepository.AddUser(userModel);
            await _unitOfWork.SaveAsync();
            var resultTrue = await _userRepository.GetUserByEmailAync("aruan@qq.com");
            Assert.True(resultTrue != null);
            var resultFalse = await _userRepository.GetUserByEmailAync("zhangsan@qq.com");
            Assert.False(resultFalse != null);
        }
        [Fact]
        public async Task GetUserByAccount()
        {
            var userModel = new SysUser();
            userModel.Id = Guid.NewGuid();
            userModel.CreateTime = DateTime.Now;
            userModel.Account = "aruan";
            userModel.Email = "aruan@qq.com";
            userModel.PassWord = "123456".ToMd5Caps16();
            _userRepository.AddUser(userModel);
            await _unitOfWork.SaveAsync();
            var resultTrue = await _userRepository.GetUserByAccountAync("aruan");
            Assert.True(resultTrue != null);
            var resultFalse = await _userRepository.GetUserByAccountAync("zhangsan");
            Assert.False(resultFalse != null);
        }
        [Fact]
        public async Task UserLogin()
        {
            var userModel = new SysUser();
            userModel.Id = Guid.NewGuid();
            userModel.CreateTime = DateTime.Now;
            userModel.Account = "aruan";
            userModel.Email = "aruan@qq.com";
            userModel.PassWord = "123456".ToMd5Caps16();
            _userRepository.AddUser(userModel);
            await _unitOfWork.SaveAsync();
            var resultTrue = await _userRepository.UserLoginAync("aruan", "123456".ToMd5Caps16());
            Assert.True(resultTrue != null);
            var resultFalse = await _userRepository.UserLoginAync("zhangsan", "123456".ToMd5Caps16());
            Assert.False(resultFalse != null);
        }
    }
}
