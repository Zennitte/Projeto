using API.Contexts;
using API.Domains;
using API.Interfaces;
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
        public void Create(User user)
        {
            user.Password = BCryptNet.HashPassword(user.Password);

            ctx.Users.Add(user);

            ctx.SaveChanges();
        }

        public void Delete(string id)
        {
            ctx.Users.Remove(GetById(id));

            ctx.SaveChanges();
        }

        public List<User> GetAll()
        {
            return ctx.Users.ToList();
        }

        public User GetById(string id)
        {
            return ctx.Users.First(u => u.Id == id);
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
