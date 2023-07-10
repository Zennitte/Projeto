using API.Domains;
using API.Interfaces;
using API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TransactionsController : ControllerBase
	{
		private readonly ITransactionRepository _transactionRepository;
        public TransactionsController(ITransactionRepository transactionRepository)
        {
			_transactionRepository = transactionRepository;
        }

        [HttpGet]
		public IActionResult GetAll() {
			List<Transaction> transactions = _transactionRepository.GetAll();

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

		[HttpPost]
		public IActionResult Create(TransactionsCreateViewModel transaction) { 
			Transaction transaction1 = _transactionRepository.Create(transaction);

			return Ok(transaction1);
		}
	}
}
