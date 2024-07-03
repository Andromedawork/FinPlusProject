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
    }
}
