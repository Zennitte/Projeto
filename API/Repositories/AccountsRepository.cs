using API.Contexts;
using API.Domains;
using API.Interfaces;

namespace API.Repositories
{
    public class AccountsRepository : IAccountRepository
    {
		private readonly ProjectContext ctx;
        public AccountsRepository(ProjectContext appContext)
        {
			ctx = appContext;
		}

		public List<Account> GetAll()
		{
			return ctx.Accounts.Select(a => new Account()
			{
				Balance = a.Balance,
				Id = a.Id,
				UserId = a.UserId,
				User = new User() {
					Username = a.User.Username
				}
			}).ToList();
		}

		public Account GetById(string id)
        {
			return ctx.Accounts.Where(a => a.Id == id).Select(a => new Account()
			{
				Balance = a.Balance,
				Id = a.Id,
				UserId = a.UserId,
				User = new User()
				{
					Username = a.User.Username
				}
			}).First();
		}
    }
}
