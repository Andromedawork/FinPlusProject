namespace FinPlus.Domain.Organisations
{
    public interface IOrganisationsRepository
    {
        public Task<List<Organisation>> GetAllOrganisations();

        public Task AddOrganisation(Organisation organisation);
    }
}
