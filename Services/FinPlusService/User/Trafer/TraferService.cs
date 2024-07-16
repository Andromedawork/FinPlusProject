namespace FinPlusService
{
    using FinPlus.Domain.Users.Trafer;

    public class TraferService : ITraferService
    {
        private readonly ITrafferRepository _trafferRepository;

        public TraferService(ITrafferRepository trafferRepository)
        {
            _trafferRepository = trafferRepository;
        }

        public async Task<List<Traffer>> GetAllTraffers()
        {
            return await _trafferRepository.GetAllTrafers();
        }

        public async Task<Traffer> GetTrafferById(string id)
        {
            return await _trafferRepository.GetTrafferById(id);
        }

        public async Task AddTraffer(Traffer trafer)
        {
            trafer.Password = BCrypt.Net.BCrypt.HashPassword(trafer.Password);
            await _trafferRepository.AddTraffer(trafer);
        }

        public async Task<bool> UpdateTraffer(Traffer traffer)
        {
            return await _trafferRepository.UpdateTraffer(traffer);
        }

        public async Task<decimal> GetTrafferRevenue(string id)
        {
            decimal revenue = 0;

            var traffer = await _trafferRepository.GetTrafferById(id);

            return revenue;
        }

        public async Task<decimal> GetTrafferProfitInMonth(string id, int month)
        {
            decimal profit = 0;

            return profit;
        }

        public async Task<List<Traffer>> GetTrafferByPartName(string partName)
        {
            return await _trafferRepository.GetAllTraffersByPartName(partName);
        }

        public async Task DeleteTrafer(string id)
        {
            await _trafferRepository.DeleteTraffer(id);
        }
    }
}