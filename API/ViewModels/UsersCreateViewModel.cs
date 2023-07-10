using System.ComponentModel.DataAnnotations;

namespace API.ViewModels
{
	public class UsersCreateViewModel
	{
		[Required]
		public string Username { get; set; } = null!;
		[Required]
		public string Password { get; set; } = null!;
	}
}
