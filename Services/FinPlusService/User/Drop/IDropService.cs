namespace FinPlusService
{
    using FinPlus.Domain.Users.Drop;

    public interface IDropService
    {
        public Task<List<Drop>> GetAllDrops();

        public Task AddDrop(Drop drop);

        public Task<List<Drop>> GetAllDropsByPartName(string partName);

        public Task<string> GetPartner(string id);

        public Task<string> GetSubPartner(string id);

        public Task<string> GetSource(string id);

        public Task DeleteDrop(string id);
    }
}