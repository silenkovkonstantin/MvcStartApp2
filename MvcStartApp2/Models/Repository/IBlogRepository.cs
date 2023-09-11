using MvcStartApp2.Models.Db;

namespace MvcStartApp2.Models.Repository
{
    public interface IBlogRepository
    {
        Task AddUser(User user);
        Task<User[]> GetUsers();
    }
}
