namespace PCTradeClient.Models
{
    public class LoginRequest {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }

    public class LoginResponse {
        public required string JwtToken { get; set; }
    }
}
