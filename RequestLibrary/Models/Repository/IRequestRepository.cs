using RequestLibrary.Models.Db;

namespace RequestLibrary.Models.Repository
{
    public interface IRequestRepository
    {
        public Task AddRequest(string urlInfo);

        Task<Request[]> GetRequests();

    }
}
