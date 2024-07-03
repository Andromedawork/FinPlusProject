namespace FinPlus.Domain.Organisations
{
    using FinPlus.Domain.Users.Trafer;

    public interface IDefaultBetRepository
    {
        public Task<List<DefaultBet>> GetAllBets();

        public Task AddDefaultBet(DefaultBet defaultBet);

        public Task<Dictionary<int, double>> GetDefualtBetByOrganisationId(string organisationId);
    }
}
