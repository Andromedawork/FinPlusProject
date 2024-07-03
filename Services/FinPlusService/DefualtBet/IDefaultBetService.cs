namespace FinPlusService.DefualtBet
{
    public interface IDefaultBetService
    {
        public Task<Dictionary<int, double>> GetDefaultBetByOrganisationId(string organisationId);
    }
}
