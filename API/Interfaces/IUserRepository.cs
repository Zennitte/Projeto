using API.Domains;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        void Create(User user);
        void Delete(string id);
        User GetById(string id);
        List<User> GetAll();
        User? Login(string username, string password);
    }
}
