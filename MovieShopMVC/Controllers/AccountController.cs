using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        public async Task<IActionResult> Login(LoginRequestModel loginRequestModel)
        {
            var user = await _accountService.ValidateUser(loginRequestModel);
            if (user == null)
            {
                // send message to the view saying "please enter correct email/password"
                return RedirectToAction("Login");
            }

            // create a cookie => have information Claims (MovieShopAuthCookie
            // Claims will have (FirstName, LstName, TimeZone, etc)
            // usually encrypt the data stored in cookies
            // Cookie Based Authentication
            // Cookie will have expiration time
            // Cookie => store in Browser

            // creatre claims that we are going to store in the cookie

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.DateOfBirth, user.DateOfBirth.GetValueOrDefault().ToString()),
                new Claim("Language", "English")
            };

            // Identity object that is going to store the claim
            var claimsIdentity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            // create the cookie
            // ASP.NET(both core and old asp.net) we have one very important class called HttpContext
            // HttpContext captures everything about http request
            // what kind of http method GET/POST/PUT, URL, FORM, Cookies, Headers
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                new ClaimsPrincipal(claimsIdentity));

            // redirect to home page
            return LocalRedirect("~/");
        }

        public async Task<IActionResult> Logout()
        {
            // invalidate cookie
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
