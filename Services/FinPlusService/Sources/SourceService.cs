namespace FinPlusService.Sources
{
    using FinPlus.Domain.Sources;
    using FinPlus.Domain.Users.Drop;

    public class SourceService : ISourceService
    {
        private readonly ISourceRepository _sourcesRepository;

        public SourceService(ISourceRepository sourcesRepository)
        {
            _sourcesRepository = sourcesRepository;
        }

        public async Task AddSource(Source source)
        {
            await _sourcesRepository.AddSource(source);
        }

        public async Task DeleteSource(string id)
        {
            await _sourcesRepository.DeleteSource(id);
        }

        public async Task<List<Drop>> GetAllMyDrops(string id)
        {
            List<Drop> result = new List<Drop>();

            return result;
        }

        public async Task<List<Source>> GetAllSourceByPartName(string partName)
        {
            return await _sourcesRepository.GetAllSourceByPartName(partName);
        }

        public async Task<List<Source>> GetAllSources()
        {
            return await _sourcesRepository.GetAllSources();
        }

        public async Task<Source> GetSourceById(string id)
        {
            return await _sourcesRepository.GetSourceById(id);
        }

        public async Task<decimal> GetSoureProfitInMonth(string id, DateTime startDate, DateTime endDate)
        {
            decimal profit = 0;

            return profit;
        }

        public async Task<decimal> GetSoureRevenue(string id)
        {
            decimal revenue = 0;

            return revenue;
        }

        public async Task<bool> UpdateSource(Source source)
        {
            return await _sourcesRepository.UpdateSource(source);
        }
    }
}
