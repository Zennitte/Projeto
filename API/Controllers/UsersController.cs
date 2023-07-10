using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using API.Domains;
using API.Interfaces;
using API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

		[Authorize]
		[HttpGet]
        public IActionResult GetAll() { 
            List<User> users = _userRepository.GetAll();

			try
			{
                if (users == null)
                {
					return BadRequest(new
					{
						Message = "Nenhum usuário encontrado"
					});
				}

                return Ok(users);
            }
			catch (Exception)
			{

				throw;
			}
        }

		[Authorize]
		[HttpGet("{id}")]
		public IActionResult Get(string id) {
			User user = _userRepository.GetById(id);

			try
			{
                if (user == null)
                {
					return BadRequest(new
					{
						Message = "Nenhum usuário encontrado"
					});
				}

				return Ok(user);
			}
			catch (Exception)
			{

				throw;
			}
		}

		[HttpPost]
		public IActionResult Create(UsersCreateViewModel user) {
			User user1 = _userRepository.Create(user);

			return Ok(user1);
		}

		[HttpPost("Login")]
		public IActionResult Login(string username, string password) {
			User user = _userRepository.Login(username, password);

            if (user != null)
            {
				var Claims = new[]
				{
					new Claim(JwtRegisteredClaimNames.Jti, user.Id),
				};

				var Key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("projectKey12345678_webApi_securityKey"));

				var Creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

				var token = new JwtSecurityToken(issuer: "projectApi", audience: "projectApi", claims: Claims, expires: DateTime.Now.AddMinutes(30), signingCredentials: Creds);


				return Ok(new
				{
					token = new JwtSecurityTokenHandler().WriteToken(token),
				});
            }
            else
            {
				return BadRequest(new
				{
					Message = "Nenhum usuário encontrado"
				});
			}
        }
    }
}
