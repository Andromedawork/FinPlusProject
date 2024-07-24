namespace FinPlusService
{
    using FinPlus.Domain.Users.Drop;
    using FinPlusService.Sources;

    public class DropService : IDropService
    {
        private readonly IDropRepository _dropRepository;

        private readonly ITraferService _traferService;

        private readonly ISourceService _sourceService;

        public DropService(IDropRepository dropRepository, ITraferService traferService, ISourceService sourceService)
        {
            _dropRepository = dropRepository;
            _traferService = traferService;
            _sourceService = sourceService;
        }

        public async Task<List<Drop>> GetAllDrops()
        {
            return await _dropRepository.GetAllDrops();
        }

        public async Task AddDrop(Drop drop)
        {
            await _dropRepository.AddDrop(drop);
        }

        public async Task<List<Drop>> GetAllDropsByPartName(string partName)
        {
            return await _dropRepository.GetAllDropsByPartName(partName);
        }

        public async Task DeleteDrop(string id)
        {
            await _dropRepository.DeleteDrop(id);
        }

        public async Task<string> GetPartner(string id)
        {
            var partner = await _traferService.GetTrafferById(id);

            if (partner == null)
            {
                return "-";
            }

            return partner.Name.ToString();
        }

        public async Task<string> GetSubPartner(string id)
        {
            var partner = await _traferService.GetTrafferById(id);

            if (partner == null)
            {
                return "-";
            }

            return partner.Name.ToString();
        }

        public async Task<string> GetSource(string id)
        {
            var source = await _sourceService.GetSourceById(id);

            if (source == null)
            {
                return "-";
            }

            return source.Name;
        }
    }
}
