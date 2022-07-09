using System;
using System.ComponentModel.DataAnnotations;
using ProjectXBff.Resolver.ConstantResolver;

namespace ProjectXBff.Resolver.CommonResolver.Models.Users
{
    public class EnterUserRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class EnterUserResponse
    {
        public bool DoesExist { get; set; }

        public Guid Id { get; set; }

        public desig Designation { get; set; }
    }
}
