using API.Domains;
using API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountsController : ControllerBase
	{
		private readonly IAccountRepository _accountRepository;
        public AccountsController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

		[HttpGet("{id}")]
		public IActionResult Get(string id) {
			Account account = _accountRepository.GetById(id);

			try
			{
				if (account == null)
				{
					return BadRequest(new
					{
						Message = "Nenhum usuário encontrado"
					});
				}

				return Ok(account);
			}
			catch (Exception)
			{

				throw;
			}
		}

		[HttpGet]
		public IActionResult GetAll() {
			List<Account> accounts = _accountRepository.GetAll();

			try
			{
                if (accounts == null)
                {
					return BadRequest(new
					{
						Message = "Nenhum usuário encontrado"
					});
				}

				return Ok(accounts);
            }
			catch (Exception)
			{

				throw;
			}
		}
    }
}
