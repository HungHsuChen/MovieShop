using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public IActionResult Details(int id)
        {
            // Call the MovieService with Id to get the movie detail info
            var movieDetail = _movieService.GetMovieDetail(id);
            return View(movieDetail);
        }
    }
}
