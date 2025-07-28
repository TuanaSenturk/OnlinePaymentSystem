using OnlinePaymentSystem.API.Controllers;


namespace OnlinePaymentSystem.API.DTOs
{
    public class AccountInfoResponse
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string IBAN { get; set; } = null!;
        public decimal Balance { get; set; }
    }
}
