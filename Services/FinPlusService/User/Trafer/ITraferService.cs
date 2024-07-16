namespace FinPlusService
{
    using FinPlus.Domain.Users.Trafer;

    public interface ITraferService
    {
        public Task<List<Traffer>> GetAllTraffers();

        public Task<Traffer> GetTrafferById(string id);

        public Task AddTraffer(Traffer trafer);

        public Task<bool> UpdateTraffer(Traffer trafer);

        public Task<decimal> GetTrafferRevenue(string id);

        public Task<decimal> GetTrafferProfitInMonth(string id, int month);

        public Task<List<Traffer>> GetTrafferByPartName(string partName);

        public Task DeleteTrafer(string id);
    }
}