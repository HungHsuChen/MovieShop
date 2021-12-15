using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MovieShopMVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        // Filters

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Purchases()
        {

            // go to User Service and call User Respository and get the Movies purchased by user who loged in
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            // pass above user id to User Service
            var purchased = await _userService.GetUserPurchasedMovies(userId);
            return View(purchased);
        }

        [HttpGet]
        public async Task<IActionResult> Favorites()
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var favorited = await _userService.GetUserFavoritedMovies(userId);
            return View(favorited);
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var user = await _userService.GetUserDetails(userId);
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            return View();
        }
    }
}
