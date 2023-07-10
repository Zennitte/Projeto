using API.Contexts;
using API.Domains;
using API.Interfaces;
using API.ViewModels;
using BCryptNet = BCrypt.Net.BCrypt;

namespace API.Repositories
{
    public class UsersRepository : IUserRepository
    {
        private readonly ProjectContext ctx;

        public UsersRepository(ProjectContext appContext)
        {
            ctx = appContext;
        }
        public User Create(UsersCreateViewModel user)
        {
            User user1 = new();
            user1.Id = Guid.NewGuid().ToString("N");
            user1.Username = user.Username;
            user1.Password = BCryptNet.HashPassword(user.Password);

            ctx.Users.Add(user1);

            Account userAccount = new Account {
                Id = Guid.NewGuid().ToString("N"),
                UserId = user1.Id,
                Balance = new Decimal(200.00)
            };

            ctx.Accounts.Add(userAccount);

            ctx.SaveChanges();

            return GetById(user1.Id);
        }

        public List<User> GetAll()
        {
            return ctx.Users.ToList();
        }

        public User GetById(string id)
        {
            return ctx.Users.Select(u => new User() { 
                Id = u.Id,
                Username = u.Username,
                Password = u.Password
            }).First(u => u.Id == id);
        }

        public User? Login(string username, string password)
        {
            User user = ctx.Users.First(u => u.Username == username);

            if (user != null)
            {
                var compare = BCryptNet.Verify(password, user.Password);

                if (compare)
                {
                    return user;
                }
            }

            return null;
        }
    }
}
