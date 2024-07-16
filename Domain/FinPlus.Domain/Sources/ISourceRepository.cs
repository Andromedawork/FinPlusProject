namespace FinPlus.Domain.Sources
{
    public interface ISourceRepository
    {
        public Task<List<Source>> GetAllSources();

        public Task<Source> GetSourceById(string id);

        public Task AddSource(Source source);

        public Task<bool> UpdateSource(Source source);

        public Task<List<Source>> GetAllSourceByPartName(string partName);

        public Task DeleteSource(string id);
    }
}
