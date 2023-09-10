using MvcStartApp2.Models.Db;

namespace MvcStartApp2
{
    public interface IBlogRepository
    {
        Task AddUser(User user);
        Task<User[]> GetUsers();
    }
}
