namespace FinPlusService
{
    using FinPlus.Domain.Users.Drop;

    public interface IDropService
    {
        public Task<List<Drop>> GetAllDrops();

        public Task AddDrop(Drop drop);

        public Task<List<Drop>> GetAllDropsByPartName(string partName);

        public Task DeleteDrop(string id);
    }
}