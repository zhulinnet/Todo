using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToDo.Core.DomainModels;
using ToDo.Core.Interfaces;
using ToDo.Infrastructure.Resources;
using ToDo.Infrastructure.Extensions;
using ToDo.Infrastructure.Jwt;

namespace ToDo.Api.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtServcie;
        private readonly ISysUserRepository _userRepository;
        private readonly IMapper _mapper;
        public AccountController(IUnitOfWork unitOfWork,
            ISysUserRepository userRepository,
            IMapper mapper,
            IJwtService jwtService)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtServcie = jwtService;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="passWord">密码</param>
        /// <returns></returns>
        [HttpPost]
        [Route("/api/[controller]/login")]
        public async Task<IActionResult> Login([FromBody]UserLoginResource userLogin)
        {
            if (string.IsNullOrEmpty(userLogin.Account) || string.IsNullOrEmpty(userLogin.PassWord))
            {
                return NotFound();
            }
            var user = await _userRepository.UserLoginAync(userLogin.Account, userLogin.PassWord.ToMd5Caps16 ());
            if (user == null)
            {
                return NotFound("用户名或密码错误");
            }
            var userResult = _mapper.Map<UserResource>(user);
            userResult.Token= _jwtServcie.CreateToken(user.Id.ToString(), user.Account);
            return Ok(userResult);
        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserAddResource user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return new UnprocessableEntityObjectResult(ModelState);
            }
            if(await _userRepository.GetUserByAccountAync(user.Account)!=null)
            {
                return BadRequest("用户已存在");
            }
            if (await _userRepository.GetUserByEmailAync(user.Email) != null)
            {
                return BadRequest("邮箱已存在");
            }
            var userModel = _mapper.Map<SysUser>(user);
            userModel.Id = Guid.NewGuid();
            userModel.CreateTime = DateTime.Now;
            userModel.PassWord = user.PassWord.ToMd5Caps16();
            _userRepository.AddUser(userModel);
            if (!await _unitOfWork.SaveAsync())
            {
                throw new Exception("Error occurred when adding");
            }
            return Ok(user);
        }
    }
}
