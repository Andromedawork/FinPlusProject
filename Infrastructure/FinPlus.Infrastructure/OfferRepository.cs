namespace FinPlus.Infrastructure
{
    using FinPlus.Domain.Offers;
    using FinPlus.Infrastructure.Models;
    using Microsoft.Extensions.Options;
    using MongoDB.Bson;
    using MongoDB.Driver;

    public class OfferRepository : IOfferRepository
    {
        private readonly IMongoCollection<Offer> _offersCollection;

        public OfferRepository(IOptions<MongoDBSettingsModel> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DataBaseName);
            _offersCollection = database.GetCollection<Offer>(mongoDBSettings.Value.OffersCollection);
        }

        public async Task<List<Offer>> GetAllOffers()
        {
            return await _offersCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task AddOffer(Offer offer)
        {
            await _offersCollection.InsertOneAsync(new Offer
            {
                Type = offer.Type,
                Name = offer.Name,
                Profit = offer.Profit,
                TargetAction = offer.TargetAction,
                OrganisationId = offer.OrganisationId,
            });
            return;
        }

        public async Task DeleteOffer(string id)
        {
            await _offersCollection.DeleteOneAsync(id);
        }
    }
}
