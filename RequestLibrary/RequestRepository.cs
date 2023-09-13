using Microsoft.EntityFrameworkCore;

namespace RequestLibrary
{
    public class RequestRepository : IRequestRepository
    {
        private readonly RequestContext _context;
        public RequestRepository(RequestContext context)
        {
            _context = context;
        }

        public async Task<Request[]> GetRequests()
        {
            return await _context.Requests.ToArrayAsync();
        }

        public async Task AddRequest(string urlInfo)
        {
            Request request = new();
            request.Url = urlInfo;

            // Добавление пользователя
            var entry = _context.Entry(request);
            if (entry.State == EntityState.Detached)
                await _context.Requests.AddAsync(request);

            // Сохранение изенений
            await _context.SaveChangesAsync();
        }
    }
}
