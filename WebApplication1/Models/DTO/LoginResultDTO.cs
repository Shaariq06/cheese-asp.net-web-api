namespace WebApplication1.Models.DTO
{
    public class LoginResultDTO
    {
        public bool Success { get; set; }
        public required string Token { get; set; }
        public string? ErrorMessage { get; set; }
    }
}