using System;
using System.Threading.Tasks;
using ProjectXBff.Resolver.CommonResolver.Models;
using ProjectXBff.Resolver.CommonResolver.Models.Users;
using ProjectXBff.Resolver.DBResolver;

namespace ProjectXBff.Resolver.ServiceResolver
{
    public class UserServiceResolver : IUserServiceResolver
    {
        public readonly IUserDBResolver _userDBResolver;
        public UserServiceResolver(IUserDBResolver userDBResolver)
        {
            _userDBResolver = userDBResolver;
        }

        public async Task<CreateUserResponse> CreateUser(CreateUserRequest request)
        {
            return await _userDBResolver.CreateUser(request);
        }

        public async Task<EnterUserResponse> EnterUser(EnterUserRequest request)
        {
            return await _userDBResolver.EnterUser(request);
        }
    }
}
