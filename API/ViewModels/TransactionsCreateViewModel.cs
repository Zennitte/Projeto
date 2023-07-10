using System.ComponentModel.DataAnnotations;

namespace API.ViewModels
{
	public class TransactionsCreateViewModel
	{
		[Required]
		public string DebitedUser { get; set; } = null!;
		[Required]
		public string CreditedUser { get; set; } = null!;
		public decimal Amount { get; set; }
	}
}
