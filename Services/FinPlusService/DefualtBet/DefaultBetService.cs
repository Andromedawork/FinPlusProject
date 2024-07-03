namespace FinPlusService.DefualtBet
{
    using FinPlus.Domain.Organisations;

    public class DefaultBetService : IDefaultBetService
    {
        private readonly IDefaultBetRepository _defaultBetRepository;

        public DefaultBetService(IDefaultBetRepository defaultBetRepository)
        {
            _defaultBetRepository = defaultBetRepository;
        }

        public async Task<Dictionary<int, double>> GetDefaultBetByOrganisationId(string organisationId)
        {
            return await _defaultBetRepository.GetDefualtBetByOrganisationId(organisationId);
        }
    }
}
