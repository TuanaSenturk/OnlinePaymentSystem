using OnlinePaymentSystem.API.Controllers;


namespace OnlinePaymentSystem.API.DTOs
{
    public class AddMoneyRequest
    {
        public decimal Amount { get; set; }
        public string Description { get; set; } = null!;
    }
}
