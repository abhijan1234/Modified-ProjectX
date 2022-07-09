using System;
using System.ComponentModel.DataAnnotations;
using ProjectXBff.Resolver.ConstantResolver;

namespace ProjectXBff.Resolver.CommonResolver.Models
{ 
    public class CreateUserRequest
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public DateTime Created_at { get; set; }

        [Required]
        public desig Designation { get; set; }
    }

    public class CreateUserResponse
    {
        public bool IsSuccess { get; set; }

        public Guid Id { get; set; }
    }
}
