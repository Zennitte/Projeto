using API.Domains;
using API.ViewModels;

namespace API.Interfaces
{
    public interface ITransactionRepository
    {
        Transaction Create(TransactionsCreateViewModel transaction);
        List<Transaction> GetAll();
        List<Transaction> GetCashIn(string id);
        List<Transaction> GetCashOut(string id);
    }
}
