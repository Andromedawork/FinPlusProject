namespace FinPlus.Infrastructure
{
    using FinPlus.Domain.Organisations;
    using FinPlus.Domain.Users.Trafer;
    using FinPlus.Infrastructure.Models;
    using Microsoft.Extensions.Options;
    using MongoDB.Bson;
    using MongoDB.Driver;

    public class DefaultBetRepository : IDefaultBetRepository
    {
        private readonly IMongoCollection<DefaultBet> _defaultBetCollection;

        public DefaultBetRepository(IOptions<MongoDBSettingsModel> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DataBaseName);
            _defaultBetCollection = database.GetCollection<DefaultBet>(mongoDBSettings.Value.DefaultBetsCollection);
        }

        public async Task<List<DefaultBet>> GetAllBets()
        {
            return await _defaultBetCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task AddDefaultBet(DefaultBet defaultBet)
        {
            await _defaultBetCollection.InsertOneAsync(new DefaultBet
            {
                Bet = defaultBet.Bet,
                OrganisationId = defaultBet.OrganisationId,
            });
            return;
        }

        public async Task<Dictionary<int, double>> GetDefualtBetByOrganisationId(string organisationId)
        {
            // var defaultBet = await _defaultBetCollection.Find(b => b.OrganisationId == organisationId).FirstOrDefaultAsync();
            Dictionary<int, double> defaultBet = new Dictionary<int, double>();

            return defaultBet;
        }
    }
}
