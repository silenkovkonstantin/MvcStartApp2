using MvcStartApp2.Models.Db;

namespace MvcStartApp2.Models.Repository
{
    public interface IRequestRepository
    {
        public Task AddRequest(string urlInfo);

        Task<Request[]> GetRequests();

    }
}
