using API.Domains;

namespace API.Interfaces
{
    public interface IAccountRepository
    {
        Account GetById(string id);
        List<Account> GetAll();
    }
}
