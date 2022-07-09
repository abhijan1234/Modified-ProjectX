using System;
using System.Threading.Tasks;
using ProjectXBff.Resolver.CommonResolver.Models;
using ProjectXBff.Resolver.CommonResolver.Models.Users;

namespace ProjectXBff.Resolver.DBResolver
{
    public interface IUserDBResolver
    {
        public Task<CreateUserResponse> CreateUser(CreateUserRequest request);

        public Task<EnterUserResponse> EnterUser(EnterUserRequest request);

    }
}
