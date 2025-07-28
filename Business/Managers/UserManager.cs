using OnlinePaymentSystem.Models;
using OnlinePaymentSystem.Business.Services;
using OnlinePaymentSystem.Business.Managers;
using Microsoft.AspNetCore.Identity.Data;
using OnlinePaymentSystem.API.Controllers;
using OnlinePaymentSystem.API.DTOs;

namespace OnlinePaymentSystem.Business.Managers
{
    public class UserManager : IUserService
    {
        private readonly OPSDbContext _context;
        public UserManager(OPSDbContext context)
        {
            _context = context;
        }

        public bool emailExists(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }
        private string generateUniqeIBAN()
        {
            string iban;
            var random = new Random();
            do
            {
                iban = "TR" + random.Next(100000000, 999999999).ToString() + random.Next(100000000, 999999999).ToString();
            } while (_context.Accounts.Any(a => a.Iban == iban));
            return iban;
        }

        public void createUser(string name, string surname, string email, string password)
        {
            if(emailExists(email))
            {
                throw new Exception("Email already exists.");
            }
            var user = new User
            {
                Name = name,
                Surname = surname,
                Email = email,
                Password = password
            };
            _context.Users.Add(user);
            _context.SaveChanges();

            var account = new Account
            {
                Id = user.Id,
                Iban = generateUniqeIBAN(),
                Balance = 0
            };

            _context.Accounts.Add(account);
            _context.SaveChanges();
        }
        public bool loginUser(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user == null)
            {
                return false;
            }

            Session.SessionData.currentUserId = user.Id;
           
            return true;
        }

        public AccountInfoResponse GetAccountInfo()
        {
            if (Session.SessionData.currentUserId == null)
            {
                throw new Exception("User not logged in.");
            }
            int id = Session.SessionData.currentUserId;
            var user = _context.Users.Find(id);
            if (user == null)
            {
                throw new Exception("User not found.");
            }
            var account = _context.Accounts.FirstOrDefault(a => a.Id == id);
            if (account == null)
            {
                throw new Exception("Account not found.");
            }
            return new AccountInfoResponse
            {
                
                Name = user.Name,
                Surname = user.Surname,
                IBAN = account.Iban,
                Balance = account.Balance ?? 0
            };
        }

        public void AddMoney(AddMoneyRequest request)
        {
            int userId = Session.SessionData.currentUserId;
            var user = _context.Users.Find(userId);
            if (user == null)
                throw new Exception("User not found.");
            var account = _context.Accounts.FirstOrDefault(a => a.Id == userId);
            if (account == null)
            {
                throw new Exception("Account not found.");
            }

            if (request.Amount <= 0)
            {
                throw new Exception("Amount must be greater than zero.");
            }

            account.Balance += request.Amount;

            _context.Transactions.Add(new Transaction
            {
                SenderAccountId = userId,
                ReceiverAccountId = userId,
                Amount = request.Amount,
                Date = DateTime.Now,
                Description = "Money added"
            });
            _context.SaveChanges();
        }


    }
}
