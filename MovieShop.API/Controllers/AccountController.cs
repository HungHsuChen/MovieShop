using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AccountController(IAccountService accountService, IUserService userService, IConfiguration configuration
            )
        {
            _accountService = accountService;
            _userService = userService;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetUserDetails(id);

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetUser()
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var user = await _userService.GetUserDetails(userId);

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequestModel model)
        {
            var user = await _accountService.RegisterUser(model);

            if (user == null)
            {
                return Unauthorized("Registration failed");
            }
            return Ok(user);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel model)
        {
            var user = await _accountService.ValidateUser(model);

            if (user == null)
            {
                return Unauthorized("wrong email / password");
            }
            // JWT Authentication
            return Ok(new {token = GenerateJWT(user)}); 
        }

        private string GenerateJWT(UserLoginResponseModel user)
        {
            // create the token

            var claims = new List<Claim> {
            new Claim(ClaimTypes.Name, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
            };

            //add the requiered claims to identity object
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            // get the secret key for signing the tokens
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["secretKey"]));

            // specify the algorithm to sign the token
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var expires = DateTime.UtcNow.AddHours(_configuration.GetValue<int>("ExpirationHours"));

            // creating the token (using system.identitymodel.token.jwt)
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Expires = expires,
                SigningCredentials = credentials,
                Issuer = _configuration["Issuer"],
                Audience = _configuration["Audience"]
            };
            var encodedJwt = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(encodedJwt);

        }

    }
}
