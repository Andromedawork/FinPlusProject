namespace FinPlus.Infrastructure
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FinPlus.Domain.Sources;
    using FinPlus.Infrastructure.Models;
    using Microsoft.Extensions.Options;
    using MongoDB.Bson;
    using MongoDB.Driver;

    public class SourceRepository : ISourceRepository
    {
        private readonly IMongoCollection<Source> _sourceCollection;

        public SourceRepository(IOptions<MongoDBSettingsModel> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DataBaseName);
            _sourceCollection = database.GetCollection<Source>(mongoDBSettings.Value.SourceCollection);
        }

        public async Task AddSource(Source source)
        {
            await _sourceCollection.InsertOneAsync(source);
            return;
        }

        public async Task DeleteSource(string id)
        {
            await _sourceCollection.DeleteOneAsync(id);
        }

        public async Task<List<Source>> GetAllSourceByPartName(string partName)
        {
            if (!string.IsNullOrEmpty(partName))
            {
                var filter = Builders<Source>.Filter.Or(
                Builders<Source>.Filter.Regex("Name.Surname", new BsonRegularExpression(partName, "i")),
                Builders<Source>.Filter.Regex("Name.Name", new BsonRegularExpression(partName, "i")),
                Builders<Source>.Filter.Regex("Name.Patronymic", new BsonRegularExpression(partName, "i")));

                var results = await _sourceCollection.Find(filter).ToListAsync();
                return results;
            }

            return await GetAllSources();
        }

        public async Task<List<Source>> GetAllSources()
        {
            return await _sourceCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Source> GetSourceById(string id)
        {
            return await _sourceCollection.Find(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateSource(Source source)
        {
            var filter = Builders<Source>.Filter.Eq(s => s.Id, source.Id);
            var update = Builders<Source>.Update
                .Set(s => s.Name, source.Name)
                .Set(s => s.ReferalId, source.ReferalId);

            var updateResult = await _sourceCollection.UpdateOneAsync(filter, update);

            return updateResult.ModifiedCount > 1;
        }
    }
}
