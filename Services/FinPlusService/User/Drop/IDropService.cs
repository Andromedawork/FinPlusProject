namespace FinPlusService
{
    using FinPlus.Domain.Offers;
    using FinPlus.Domain.Users;
    using FinPlus.Domain.Users.Drop;

    public interface IDropService
    {
        public Task<List<Drop>> GetAllDrops();

        public Task<Drop> GetDropById(string id);

        public Task AddDrop(Drop drop);

        public Task<List<Drop>> GetAllDropsByPartName(string partName);

        public Task<string> GetPartner(string id);

        public Task<string> GetSubPartner(string id);

        public Task<string> GetSource(string id);

        public Task<bool> UpdateDropData(string id, FIO name, string referalId, DropPassport pass, string mobileNumber, string cardNumber, string personalReferalId, DateTime dateOfBirth, string telegram);

        public Task<bool> UpdateDropStep(string id, int step, List<Offer> offers, string comments);

        public Task DeleteDrop(string id);
    }
}