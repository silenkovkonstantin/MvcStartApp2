using Microsoft.EntityFrameworkCore;
using MvcStartApp2.Models.Db;

namespace MvcStartApp2
{
    public class BlogRepository : IBlogRepository
    {
        // Ссылка на контекст
        private readonly BlogContext _context;

        public BlogRepository(BlogContext context)
        {
            _context = context;
        }

        public async Task AddUser(User user)
        {
            user.JoinDate = DateTime.Now;
            user.Id = Guid.NewGuid();

            // Добавление пользователя
            var entry = _context.Entry(user);
            if (entry.State == EntityState.Detached)
                await _context.Users.AddAsync(user);

            // Сохранение изенений
            await _context.SaveChangesAsync();
        }

        public async Task<User[]> GetUsers()
        {
            // Получаем всех активных пользователеей
            return await _context.Users.ToArrayAsync();
        }
    }
}
