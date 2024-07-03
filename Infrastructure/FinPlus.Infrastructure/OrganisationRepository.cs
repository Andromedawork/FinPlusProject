namespace FinPlus.Infrastructure
{
    using FinPlus.Domain.Organisations;
    using FinPlus.Infrastructure.Models;
    using Microsoft.Extensions.Options;
    using MongoDB.Bson;
    using MongoDB.Driver;

    public class OrganisationRepository : IOrganisationsRepository
    {
        private readonly IMongoCollection<Organisation> _organisationsCollection;

        public OrganisationRepository(IOptions<MongoDBSettingsModel> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DataBaseName);
            _organisationsCollection = database.GetCollection<Organisation>(mongoDBSettings.Value.OrganisationsCollection);
        }

        public async Task<List<Organisation>> GetAllOrganisations()
        {
            return await _organisationsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task AddOrganisation(Organisation organisation)
        {
            await _organisationsCollection.InsertOneAsync(new Organisation
            {
                MainAdminId = organisation.MainAdminId,
                Name = organisation.Name,
            });
            return;
        }
    }
}
