namespace FinPlus.Infrastructure
{
    using FinPlus.Domain.Organisations;
    using FinPlus.Domain.Users.Admin;
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

        public async Task<Organisation> GetOrganisationById(string id)
        {
            return await _organisationsCollection.Find(o => o.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateOrganisation(Organisation organisation)
        {
            var filter = Builders<Organisation>.Filter.Eq(o => o.Id, organisation.Id);
            var update = Builders<Organisation>.Update
            .Set(o => o.Name, organisation.Name)
            .Set(o => o.MainAdminId, organisation.MainAdminId);

            var updateResult = await _organisationsCollection.UpdateOneAsync(filter, update);

            return updateResult.ModifiedCount > 1;
        }

        public async Task<Organisation> GetOrganisationByName(string name)
        {
            return await _organisationsCollection.Find(o => o.Name == name).FirstOrDefaultAsync();
        }

        public async Task DeleteOrganisation(string id)
        {
            await _organisationsCollection.DeleteOneAsync(id);
        }
    }
}
