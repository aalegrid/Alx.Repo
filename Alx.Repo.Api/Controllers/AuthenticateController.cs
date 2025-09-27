using Alx.Repo.Application.Auth;
using Alx.Repo.Application.Auth.Model;
using Alx.Repo.Application.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Alx.Repo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticateController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUser model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                    new Claim(ClaimTypes.Email, user.Email!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                return Ok(new
                {
                    access_token = new JwtSecurityTokenHandler().WriteToken(token),
                    //expires_in = DateTime.UtcNow.AddMinutes(120), // Set 2 hour validation
                    expires_in = token.ValidTo // Use default - 3 hour validation
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUser model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Email);
            if (userExists != null)
            {
                var errorDict = new Dictionary<string, string[]>
                {
                    { "UserExists", new[] { "User already exists." } }
                };
                return StatusCode(StatusCodes.Status400BadRequest, new ErrorResponse { Message = "User creation failed. Please check user details and try again.", Errors = errorDict });
            }
                

            ApplicationUser user = new()
            {
                FirstName = model.FirstName,
                LastName = model.LastName, 
                AppDomain = model.AppDomain,
                Email = model.Email,
                UserName = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),

            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                // Convert IEnumerable<IdentityError> to Dictionary<string, string[]>
                var errorDict = result.Errors
                    .GroupBy(e => e.Code)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.Description).ToArray()
                    );

                return StatusCode(StatusCodes.Status400BadRequest, new ErrorResponse { Message = "User creation failed. Please check user details and try again.", Errors = errorDict });
            }

            return Ok(new { Message = "User created successfully." });

        }


        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var jwtKey = _configuration.GetSection("JWT:Secret").Value ?? throw new InvalidOperationException("Config[JWT:Secret] not found.");
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
