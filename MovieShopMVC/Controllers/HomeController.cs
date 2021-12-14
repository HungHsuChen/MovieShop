using ApplicationCore.ServiceInterfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Models;
using System.Diagnostics;

namespace MovieShopMVC.Controllers
{
    public class HomeController : Controller
    {

        private readonly IMovieService _movieService;
        private readonly IGenreService _genreService;
        public HomeController(IMovieService movieService, IGenreService genreService )
        {
            _movieService = movieService;
            _genreService = genreService;
        }

        // u1, u2, ...., u100 =>
        // Thread Pool => Collection of threads => 100 T1...T100
        // Thread Starvation
        // Thread => worker in a factory => 
        // calling this same method at 10:00AM
        // U101, U102 =>

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Call Model Service to get list of movie cards to show in the index view
            // 3 ways to pass the data/models from Controller Action methods to Views
            // 1. Pass the Models in the View Method
            // 2. ViewBag
            // 3. ViewData

            // This is an I/O bound operation => Database calls, File calls, Http Call
            // May take 10ms, 100ms, 1sec, 10sec
            // T1 is waiting for the I/O bound operation to finish
            var movieCards = await _movieService.GetHighestGrossingMovies();
            
            // CPU bound operation => Resizing an image, reading pixel image, calculating Pi, calculating algorithm, loan interest

            return View(movieCards);
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            //var movieDetail = _movieService.GetMovieDetail();
            //return View(movieDetail);
            return View();
        }

        [HttpGet]
        public IActionResult TopMovies()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}