using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectXBff.Resolver.CommonResolver.Models;
using ProjectXBff.Resolver.CommonResolver.Models.Users;
using ProjectXBff.Resolver.ConstantResolver;
using ProjectXBff.Resolver.ServiceResolver;

namespace ProjectX_Bff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserServiceResolver _userServiceResolver;

        public UserController(IUserServiceResolver userServiceResolver)
        {
            _userServiceResolver = userServiceResolver;
        }

        [HttpPost]
        [Route(template: "CreateUser")]
        public async Task<IActionResult> CreateUser(CreateUserRequest request)
        {
            CreateUserResponse response = null;
            try
            {
                response = await _userServiceResolver.CreateUser(request);
            }catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Id = Guid.Empty;
            }
            return Ok(response);
        }

        [HttpGet]
        [Route(template: "EnterUser")]
        public async Task<IActionResult> EnterUser(EnterUserRequest request)
        {
            EnterUserResponse response = null;

            try
            {
                response = await _userServiceResolver.EnterUser(request);
            }catch(Exception ex)
            {
                response.DoesExist = false;
                response.Id = Guid.Empty;
                response.Designation = desig.none;
            }
            return Ok(response);
        }
    }
}