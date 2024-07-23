namespace FinPlusService.Organisation
{
    using FinPlus.Domain.Organisations;

    public interface IOrganisationService
    {
        public Task<List<Organisation>> GetAllOrganisations();

        public Task AddOrganisation(Organisation organisation);

        public Task<Organisation> GetOrganisationById(string id);

        public Task<Organisation> GetOrganisationByName(string name);

        public Task<bool> UpdateOrganisation(Organisation organisation);

        public Task DeleteOrganisation(string id);
    }
}
