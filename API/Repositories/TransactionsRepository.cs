using API.Contexts;
using API.Domains;
using API.Interfaces;

namespace API.Repositories
{
    public class TransactionsRepository : ITransactionRepository
    {
		private readonly ProjectContext ctx;
        public TransactionsRepository(ProjectContext appContext)
        {
            ctx = appContext;
        }
        public void Create(Transaction transaction)
        {
			transaction.Id = Guid.NewGuid().ToString("N");

			Account debitedAccount = ctx.Accounts.Where(a => a.Id == transaction.DebbitedAccountNavigation.Id).Select(a => new Account() { 
                Balance = a.Balance,
                Id = a.Id,
                UserId = a.UserId,
            }).First();

			Account creditedAccount = ctx.Accounts.Where(a => a.Id == transaction.CreditedAccountNavigation.Id).Select(a => new Account()
			{
				Balance = a.Balance,
				Id = a.Id,
				UserId = a.UserId,
			}).First();

            try
            {
                if (transaction.Amount > debitedAccount.Balance)
                {
                    throw new Exception();
                }

                debitedAccount.Balance = debitedAccount.Balance - transaction.Amount;
                creditedAccount.Balance = creditedAccount.Balance + transaction.Amount;

                ctx.Accounts.Update(debitedAccount);
                ctx.Accounts.Update(creditedAccount);

                ctx.Transactions.Add(transaction);
                ctx.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Transaction> GetAll()
        {
            return ctx.Transactions.ToList();
        }

        public List<Transaction> GetCashIn(string id)
        {
            return ctx.Transactions.Where(t => t.CreditedAccountNavigation.Id == id).ToList();
        }

        public List<Transaction> GetCashOut(string id)
        {
			return ctx.Transactions.Where(t => t.DebbitedAccountNavigation.Id == id).ToList();
		}
    }
}
