using RequestLibrary.Models.Db;

namespace RequestLibrary.Models.Repository
{
    public interface IBlogRepository
    {
        Task AddUser(User user);
        Task<User[]> GetUsers();
    }
}
