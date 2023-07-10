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
        public void Create(Account account)
        {
            ctx.Accounts.Add(account);

            ctx.SaveChanges();
        }

        public void Delete(string id)
        {
            ctx.Remove(GetById(id));

            ctx.SaveChanges();
        }

        public Account GetById(string id)
        {
			return ctx.Accounts.First(a => a.Id == id);
		}

        public void Update(string id, Account account)
        {
            Account account1 = GetById(id);

            account1 = account;

            ctx.Accounts.Update(account1);

            ctx.SaveChanges();
        }
    }
}
