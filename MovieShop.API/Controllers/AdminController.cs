using ApplicationCore.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AdminService _adminService;

        public AdminController(AdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost]
        [Route("movie")]
        public async Task<IActionResult> CreateMovie([FromBody] MovieCreateRequestModel model)
        {
            var movie = await _adminService.CreateMovie(model);

            if(movie == null) return Unauthorized("Failed");

            return Ok(movie);
        }

        [HttpPut]
        [Route("movie")]
        public async Task<IActionResult> UpdateMovie([FromBody] MovieCreateRequestModel model)
        {
            var movie = await _adminService.UpdateMovie(model);

            if (movie == null) return Unauthorized("Failed");

            return Ok(movie);

        }

        [HttpGet]
        [Route("purchases")]
        public async Task<IActionResult> GetAllPurchases()
        {
            var purchases = await _adminService.GetAllPurchases();

            if(!purchases.Any()) return NotFound();

            return Ok(purchases);
        }
    }
}
