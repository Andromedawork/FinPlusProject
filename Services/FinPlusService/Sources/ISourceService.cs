namespace FinPlusService.Sources
{
    using FinPlus.Domain.Sources;
    using FinPlus.Domain.Users.Drop;

    public interface ISourceService
    {
        public Task<List<Source>> GetAllSources();

        public Task<Source> GetSourceById(string id);

        public Task AddSource(Source source);

        public Task<bool> UpdateSource(Source source);

        public Task<List<Source>> GetAllSourceByPartName(string partName);

        public Task<decimal> GetSoureRevenue(string id);

        public Task<decimal> GetSoureProfitInMonth(string id, DateTime startDate, DateTime endDate);

        public Task<List<Drop>> GetAllMyDrops(string id);

        public Task DeleteSource(string id);
    }
}
