using API.Domains;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

		/// <summary>
		/// Busca uma conta por id
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Retorna uma conta</returns>
		[Authorize]
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

		/// <summary>
		/// Busca todas as contas da aplicação
		/// </summary>
		/// <returns>Retorna uma lista de contas</returns>
		[Authorize]
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
