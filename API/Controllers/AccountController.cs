using Microsoft.AspNetCore.Mvc;
using OnlinePaymentSystem.Business.Services;
using OnlinePaymentSystem.API.DTOs;

namespace OnlinePaymentSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    { 
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
       
        [HttpGet("account-info")]
        public IActionResult GetAccountInfo()
        {
            try
            {
                var accountInfo = _userService.GetAccountInfo();
                return Ok(accountInfo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add-money")]
        public IActionResult AddMoney([FromBody] AddMoneyRequest request)
        {   
            if (string.IsNullOrEmpty(request.Description))
            {
                return BadRequest("Description is required.");
            }
            try
            {
                _userService.AddMoney(request);
                return Ok("Money added successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
