using API.Domains;

namespace API.Interfaces
{
    public interface ITransactionRepository
    {
        void Create(Transaction transaction);
        List<Transaction> GetAll();
        List<Transaction> GetCashIn(string id);
        List<Transaction> GetCashOut(string id);
    }
}
