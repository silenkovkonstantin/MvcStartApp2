namespace RequestLibrary
{
    public interface IRequestRepository
    {
        public Task AddRequest(string urlInfo);

        Task<Request[]> GetRequests();

    }
}
