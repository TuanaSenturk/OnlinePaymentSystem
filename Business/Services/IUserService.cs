using OnlinePaymentSystem.Business.Managers;
using OnlinePaymentSystem.Business.Services;
using OnlinePaymentSystem.API.DTOs;

namespace OnlinePaymentSystem.Business.Services
{
    public interface IUserService
    {
        bool emailExists(string email);
        void createUser(string name, string surname, string email, string password);
        bool loginUser(string email, string password);
        AccountInfoResponse GetAccountInfo();
        void AddMoney(AddMoneyRequest request);

    }
}
