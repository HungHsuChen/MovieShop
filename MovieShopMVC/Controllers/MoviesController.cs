using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class MoviesController : Controller
    {
        public IActionResult Details(int id)
        {
            // Call the MovieService with Id to get the movie detail info
            return View();
        }
    }
}
