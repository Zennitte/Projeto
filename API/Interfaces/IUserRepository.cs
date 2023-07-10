using API.Domains;
using API.ViewModels;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        User Create(UsersCreateViewModel user);
        User GetById(string id);
        List<User> GetAll();
        User? Login(string username, string password);
    }
}
