using API.Contexts;
using API.Domains;
using API.Interfaces;
using API.ViewModels;

namespace API.Repositories
{
    public class TransactionsRepository : ITransactionRepository
    {
		private readonly ProjectContext ctx;
        public TransactionsRepository(ProjectContext appContext)
        {
            ctx = appContext;
        }
        public Transaction Create(TransactionsCreateViewModel transaction)
        {
			Account debitedAccount = ctx.Accounts.Where(a => a.User.Username == transaction.DebitedUser).Select(a => new Account() { 
                Balance = a.Balance,
                Id = a.Id,
                UserId = a.UserId,
            }).First();

			Account creditedAccount = ctx.Accounts.Where(a => a.User.Username == transaction.CreditedUser).Select(a => new Account()
			{
				Balance = a.Balance,
				Id = a.Id,
				UserId = a.UserId,
			}).First();

            try
            {
                if (debitedAccount == null || creditedAccount == null)
                {
                    throw new Exception();
                }

                if (transaction.Amount > debitedAccount.Balance)
                {
                    throw new Exception();
                }

				Transaction transaction1 = new Transaction();
				transaction1.Id = Guid.NewGuid().ToString("N");
				transaction1.Amount = transaction.Amount;
				transaction1.CreatedAt = DateTime.Now;
                transaction1.CreditedAccount = creditedAccount.Id;
                transaction1.DebbitedAccount = debitedAccount.Id;

				debitedAccount.Balance = debitedAccount.Balance - transaction.Amount;
                creditedAccount.Balance = creditedAccount.Balance + transaction.Amount;

                ctx.Accounts.Update(debitedAccount);
                ctx.Accounts.Update(creditedAccount);

                ctx.Transactions.Add(transaction1);
                ctx.SaveChanges();

                return transaction1;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Transaction> GetAll(string id)
        {
            return ctx.Transactions.Where(t => t.DebbitedAccountNavigation.UserId == id || t.CreditedAccountNavigation.UserId == id).Select(t => new Transaction { 
                Amount = t.Amount,
                CreatedAt = t.CreatedAt,
                DebbitedAccountNavigation = new Account() { 
                    Balance = t.DebbitedAccountNavigation.Balance,
                    User = new User() { 
                        Username = t.DebbitedAccountNavigation.User.Username
					}
                },
                CreditedAccountNavigation = new Account() { 
                    Balance = t.CreditedAccountNavigation.Balance,
                    User = new User() { 
                        Username = t.CreditedAccountNavigation.User.Username
                    }
                }
            }).ToList();
        }

        public List<Transaction> GetCashIn(string id)
        {
            return ctx.Transactions.Where(t => t.CreditedAccountNavigation.UserId == id).Select(t => new Transaction
            {
                Amount = t.Amount,
                CreatedAt = t.CreatedAt,
                CreditedAccountNavigation = new Account()
                {
                    Balance = t.CreditedAccountNavigation.Balance,
                    User = new User()
                    {
                        Username = t.CreditedAccountNavigation.User.Username
                    }
                },
				DebbitedAccountNavigation = new Account()
				{
					Balance = t.DebbitedAccountNavigation.Balance,
					User = new User()
					{
						Username = t.DebbitedAccountNavigation.User.Username
					}
				}
			}).ToList();
        }

        public List<Transaction> GetCashOut(string id)
        {
			return ctx.Transactions.Where(t => t.DebbitedAccountNavigation.UserId == id).Select(t => new Transaction
			{
				Amount = t.Amount,
				CreatedAt = t.CreatedAt,
				DebbitedAccountNavigation = new Account()
				{
					Balance = t.DebbitedAccountNavigation.Balance,
					User = new User()
					{
						Username = t.DebbitedAccountNavigation.User.Username
					}
				},
				CreditedAccountNavigation = new Account()
				{
					Balance = t.CreditedAccountNavigation.Balance,
					User = new User()
					{
						Username = t.CreditedAccountNavigation.User.Username
					}
				}
			}).ToList();
		}
    }
}
