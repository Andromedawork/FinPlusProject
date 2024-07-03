namespace FinPlusService
{
    using FinPlus.Domain.Users.Drop;

    public interface IDropService
    {
        public Task<List<Drop>> GetAllDrops();

        public Task AddDrop(Drop drop);
    }
}