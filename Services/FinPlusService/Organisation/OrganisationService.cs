namespace FinPlusService.Organisation
{
    using FinPlus.Domain.Organisations;

    public class OrganisationService : IOrganisationService
    {
        private readonly IOrganisationsRepository _organisationsRepository;

        public OrganisationService(IOrganisationsRepository organisationsRepository)
        {
            _organisationsRepository = organisationsRepository;
        }

        public async Task AddOrganisation(Organisation organisation)
        {
            await _organisationsRepository.AddOrganisation(organisation);
        }

        public async Task DeleteOrganisation(string id)
        {
            await _organisationsRepository.DeleteOrganisation(id);
        }

        public async Task<List<Organisation>> GetAllOrganisations()
        {
            return await _organisationsRepository.GetAllOrganisations();
        }

        public async Task<Organisation> GetOrganisationById(string id)
        {
            return await _organisationsRepository.GetOrganisationById(id);
        }

        public async Task<Organisation> GetOrganisationByName(string name)
        {
            return await _organisationsRepository.GetOrganisationByName(name);
        }

        public Task<bool> UpdateOrganisation(Organisation organisation)
        {
            return _organisationsRepository.UpdateOrganisation(organisation);
        }
    }
}
