using System.IdentityModel.Tokens.Jwt;
using API.Domains;
using API.Interfaces;
using API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class TransactionsController : ControllerBase
	{
		private readonly ITransactionRepository _transactionRepository;
        public TransactionsController(ITransactionRepository transactionRepository)
        {
			_transactionRepository = transactionRepository;
        }

		/// <summary>
		/// Busca todas as transações do usuário
		/// </summary>
		/// <returns>Retorna uma lista de transações</returns>
        [HttpGet("All")]
		public IActionResult GetAll() {
			string id = HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value;

			List<Transaction> transactions = _transactionRepository.GetAll(id);

			try
			{
                if (transactions == null)
                {
					return BadRequest(new
					{
						Message = "Nenhum usuário encontrado"
					});
				}

				return Ok(transactions);
            }
			catch (Exception)
			{

				throw;
			}
		}

		/// <summary>
		/// Busca todas as transações de cashin do usuário
		/// </summary>
		/// <returns>Retorna uma lista de transações</returns>
		[HttpGet("CashIn")]
		public IActionResult GetCashIn() {
			string id = HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value;

			List<Transaction> transactions = _transactionRepository.GetCashIn(id);

			try
			{
				if (transactions == null)
				{
					return BadRequest(new
					{
						Message = "Nenhum usuário encontrado"
					});
				}

				return Ok(transactions);
			}
			catch (Exception)
			{

				throw;
			}
		}

		/// <summary>
		/// Busca todas as transações de cashout do usuário
		/// </summary>
		/// <returns>Retorna uma lista de transações</returns>
		[HttpGet("CashOut")]
		public IActionResult GetCashOut() {
			string id = HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value;

			List<Transaction> transactions = _transactionRepository.GetCashOut(id);

			try
			{
				if (transactions == null)
				{
					return BadRequest(new
					{
						Message = "Nenhum usuário encontrado"
					});
				}

				return Ok(transactions);
			}
			catch (Exception)
			{

				throw;
			}
		}

		/// <summary>
		/// Cria uma transação
		/// </summary>
		/// <param name="transaction"></param>
		/// <returns>Retorna uma transação</returns>
		[HttpPost]
		public IActionResult Create(TransactionsCreateViewModel transaction) {
			try
			{
				Transaction transaction1 = _transactionRepository.Create(transaction);
				return Ok(transaction1);
			}
			catch (Exception)
			{
				return BadRequest(new { 
					Message = "Erro ao criar transação, conta provavelmente inexistente"
				});
			}
		}
	}
}
