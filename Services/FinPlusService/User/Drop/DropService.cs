namespace FinPlusService
{
    using FinPlus.Domain.Users.Drop;

    public class DropService : IDropService
    {
        private readonly IDropRepository _dropRepository;

        public DropService(IDropRepository dropRepository)
        {
            _dropRepository = dropRepository;
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
    }
}
