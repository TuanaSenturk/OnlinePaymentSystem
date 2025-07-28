using OnlinePaymentSystem.API.Controllers;

namespace OnlinePaymentSystem.API.DTOs
{
    public class LoginRequest
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

    }
}
