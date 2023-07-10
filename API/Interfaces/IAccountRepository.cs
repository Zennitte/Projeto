using API.Domains;

namespace API.Interfaces
{
    public interface IAccountRepository
    {
        void Create(Account account);
        void Update(string id, Account account);
        void Delete(string id);
        Account GetById(string id);
    }
}
