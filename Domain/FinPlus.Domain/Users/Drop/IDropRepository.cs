namespace FinPlus.Domain.Users.Drop
{
    using FinPlus.Domain.Offers;

    public interface IDropRepository
    {
        public Task<List<Drop>> GetAllDrops();

        public Task AddDrop(Drop drop);

        public Task<Drop> GetDropById(string id);

        public Task<bool> UpdateDropData(string id, FIO name, string referalId, DropPassport pass, string mobileNumber, string cardNumber, string personalReferalId, DateTime dateOfBirth);

        public Task<bool> UpdateDropStep(string id, Dictionary<DateTime, DropStep> step, List<Offer> offers, Dictionary<DropStep, string> comments);

        public Task<bool> UpdateDropFinance(string id, bool moneyStatus, DropCategory category, int point, decimal revenuePotential, decimal profitPotential, decimal payment);

        public Task<List<Drop>> GetAllDropsByPartName(string partName);
    }
}
