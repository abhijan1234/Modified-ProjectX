using System;
using System.Threading.Tasks;
using ProjectXBff.Resolver.CommonResolver.Models;
using ProjectXBff.Resolver.CommonResolver.Models.Users;

namespace ProjectXBff.Resolver.ServiceResolver
{
    public interface IUserServiceResolver
    {
        public Task<CreateUserResponse> CreateUser(CreateUserRequest request);

        public Task<EnterUserResponse> EnterUser(EnterUserRequest request);

    }
}
