namespace FinPlus.Infrastructure
{
    using FinPlus.Domain.Users.Drop;
    using FinPlus.Domain.Users.Trafer;
    using FinPlus.Infrastructure.Models;
    using Microsoft.Extensions.Options;
    using MongoDB.Bson;
    using MongoDB.Driver;

    public class TrafferRepository : ITrafferRepository
    {
        private readonly IMongoCollection<Traffer> _trafferCollection;

        public TrafferRepository(IOptions<MongoDBSettingsModel> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DataBaseName);
            _trafferCollection = database.GetCollection<Traffer>(mongoDBSettings.Value.TraffersCollection);
        }

        public async Task<List<Traffer>> GetAllTrafers()
        {
            return await _trafferCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task AddTraffer(Traffer traffer)
        {
            await _trafferCollection.InsertOneAsync(traffer);
            return;
        }

        public async Task<Traffer> GetTrafferById(string id)
        {
            return await _trafferCollection.Find(t => t.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateTraffer(Traffer traffer)
        {
            var filter = Builders<Traffer>.Filter.Eq(t => t.Id, traffer.Id);
            var update = Builders<Traffer>.Update
                .Set(t => t.Name, traffer.Name)
                .Set(t => t.Level, traffer.Level)
                .Set(t => t.Login, traffer.Login)
                .Set(t => t.Password, traffer.Password)
                .Set(t => t.ReferalId, traffer.ReferalId)
                .Set(t => t.Bet, traffer.Bet)
                .Set(t => t.OrganisationId, traffer.OrganisationId);

            var updateResult = await _trafferCollection.UpdateOneAsync(filter, update);

            return updateResult.ModifiedCount > 1;
        }

        public async Task<List<Traffer>> GetAllTraffersByPartName(string partName)
        {
            var filter = Builders<Traffer>.Filter.Or(
            Builders<Traffer>.Filter.Regex("Name.Surname", new BsonRegularExpression(partName, "i")),
            Builders<Traffer>.Filter.Regex("Name.Name", new BsonRegularExpression(partName, "i")),
            Builders<Traffer>.Filter.Regex("Name.Patronymic", new BsonRegularExpression(partName, "i")));

            var results = await _trafferCollection.Find(filter).ToListAsync();
            return results;
        }

        public async Task<Traffer> GetTrafferByLogin(string login)
        {
            return await _trafferCollection.Find(t => t.Login == login).FirstOrDefaultAsync();
        }
    }
}