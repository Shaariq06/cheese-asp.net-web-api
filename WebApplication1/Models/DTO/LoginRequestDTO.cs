using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.DTO
{
    public class LoginRequestDTO
    {
        public required string Username { get; set; }
        [DataType(DataType.Password)]
        public required string Password { get; set; }
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }
    }
}