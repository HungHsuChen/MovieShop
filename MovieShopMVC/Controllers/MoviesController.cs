using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MovieShopMVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public async Task<IActionResult> Details(int id)
        {
            // Call the MovieService with Id to get the movie detail info
            var movieDetail = await _movieService.GetMovieDetail(id);
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            movieDetail.PurchaseStat = await _movieService.PurchaseStat(movieDetail.Id, userId);
            return View(movieDetail);
        }
    }
}
