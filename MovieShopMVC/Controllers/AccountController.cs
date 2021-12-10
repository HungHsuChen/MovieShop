using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // acount/register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterRequestModel userRegisterRequestModel)
        {
            // we need to send the data to service, which is gonna convert in to user entity and send it to user repository
            // save the data in database and return to login page
            var user = await _accountService.RegisterUser(userRegisterRequestModel);
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginRequestModel loginRequestModel)
        {
            var user = _accountService.ValidateUser(loginRequestModel);
            if (user == null)
            {
                // send message to the view saying "please enter correct email/password"
            }

            // create a cookie => have information Claims (MovieShopAuthCookie
            // Claims will have (FirstName, LstName, TimeZone, etc)
            // usually encrypt the data stored in cookies
            // Cookie Based Authentication
            // Cookie will have expiration time
            // Cookie => store in Browser 

            // redirect to home page
            // 
            return View();
        }
    }
}
