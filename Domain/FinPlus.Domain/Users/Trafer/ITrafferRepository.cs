namespace FinPlus.Domain.Users.Trafer
{
    public interface ITrafferRepository
    {
        public Task<List<Traffer>> GetAllTrafers();

        public Task AddTraffer(Traffer traffer);

        public Task<Traffer> GetTrafferById(string id);

        public Task<bool> UpdateTraffer(Traffer traffer);

        public Task<List<Traffer>> GetAllTraffersByPartName(string partName);
    }
}
