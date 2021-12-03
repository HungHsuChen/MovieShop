﻿using ApplicationCore.ServiceInterfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Models;
using System.Diagnostics;

namespace MovieShopMVC.Controllers
{
    public class HomeController : Controller
    {
        //private MovieService _movieService;
        //public HomeController()
        //{
        //    _movieService = new MovieService(new MovieRepository());
        //}

        private readonly IMovieService _movieService;

        public HomeController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // Call Model Service to get list of movie cards to show in the index view
            // 3 ways to pass the data/models from Controller Action methods to Views
                // 1. Pass the Models in the View Method
                // 2. ViewBag
                // 3. ViewData
            var movieCards = _movieService.GetTopMovies();
            return View(movieCards);
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            var movieDetail = _movieService.GetMovieDetail();
            return View(movieDetail);
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