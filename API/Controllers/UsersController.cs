using API.Domains;
using API.Interfaces;
using API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
				return Ok(user);
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
