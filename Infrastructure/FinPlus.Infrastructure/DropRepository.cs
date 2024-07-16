namespace FinPlus.Infrastructure
{
    using Amazon.Runtime.SharedInterfaces;
    using FinPlus.Domain.Offers;
    using FinPlus.Domain.Users;
    using FinPlus.Domain.Users.Drop;
    using FinPlus.Infrastructure.Models;
    using Microsoft.Extensions.Options;
    using MongoDB.Bson;
    using MongoDB.Driver;

    public class DropRepository : IDropRepository
    {
        private readonly IMongoCollection<Drop> _dropsCollection;

        public DropRepository(IOptions<MongoDBSettingsModel> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DataBaseName);
            _dropsCollection = database.GetCollection<Drop>(mongoDBSettings.Value.DropsCollection);
        }

        public async Task<List<Drop>> GetAllDrops()
        {
            return await _dropsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task AddDrop(Drop drop)
        {
            await _dropsCollection.InsertOneAsync(new Drop
            {
                Name = drop.Name,
                ReferalId = drop.ReferalId,
                Pass = drop.Pass,
                Offers = drop.Offers,
                MoneyStatus = drop.MoneyStatus,
                DropCategory = drop.DropCategory,
                Point = drop.Point,
                RevenuePotential = drop.RevenuePotential,
                ProfitPotencial = drop.ProfitPotencial,
                TrafferProfitPotencial = drop.TrafferProfitPotencial,
                Payment = drop.Payment,
                MobileNumber = drop.MobileNumber,
                CardNumber = drop.CardNumber,
                PersonalReferalId = drop.PersonalReferalId,
                Steps = drop.Steps,
                DateOfBirth = drop.DateOfBirth,
                OrganisationId = drop.OrganisationId,
                PaymentTransactionNumber = drop.PaymentTransactionNumber,
                DateEnd = drop.DateEnd,
                Comments = drop.Comments,
            });
            return;
        }

        public async Task<Drop> GetDropById(string id)
        {
            return await _dropsCollection.Find(d => d.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateDropData(string id, FIO name, string referalId, DropPassport pass, string mobileNumber, string cardNumber, string personalReferalId, DateTime dateOfBirth)
        {
            var filter = Builders<Drop>.Filter.Eq(d => d.Id, id);
            var update = Builders<Drop>.Update
                .Set(d => d.Name, name)
                .Set(d => d.ReferalId, referalId)
                .Set(d => d.Pass, pass)
                .Set(d => d.MobileNumber, mobileNumber)
                .Set(d => d.CardNumber, cardNumber)
                .Set(d => d.PersonalReferalId, personalReferalId)
                .Set(d => d.DateOfBirth, dateOfBirth);

            var updateResult = await _dropsCollection.UpdateOneAsync(filter, update);

            return updateResult.ModifiedCount > 0;
        }

        public async Task<bool> UpdateDropStep(string id, Dictionary<DateTime, DropStep> step, List<Offer> offers, Dictionary<DropStep, string> comments)
        {
            var filter = Builders<Drop>.Filter.Eq(d => d.Id, id);
            var update = Builders<Drop>.Update
                .Set(d => d.Steps, step)
                .Set(d => d.Comments, comments)
                .Set(d => d.Offers, offers);

            var updateResult = await _dropsCollection.UpdateOneAsync(filter, update);

            return updateResult.ModifiedCount > 0;
        }

        public async Task<bool> UpdateDropFinance(string id, bool moneyStatus, DropCategory category, int point, decimal revenuePotential, decimal profitPotential, decimal payment)
        {
            var filter = Builders<Drop>.Filter.Eq(d => d.Id, id);
            var update = Builders<Drop>.Update
                .Set(d => d.MoneyStatus, moneyStatus)
                .Set(d => d.DropCategory, category)
                .Set(d => d.Point, point)
                .Set(d => d.RevenuePotential, revenuePotential)
                .Set(d => d.ProfitPotencial, profitPotential)
                .Set(d => d.Payment, payment);

            var updateResult = await _dropsCollection.UpdateOneAsync(filter, update);

            return updateResult.ModifiedCount > 0;
        }

        public async Task<List<Drop>> GetAllDropsByPartName(string partName)
        {
            if (!string.IsNullOrEmpty(partName))
            {
                var filter = Builders<Drop>.Filter.Or(
                Builders<Drop>.Filter.Regex("Name.Surname", new BsonRegularExpression(partName, "i")),
                Builders<Drop>.Filter.Regex("Name.Name", new BsonRegularExpression(partName, "i")),
                Builders<Drop>.Filter.Regex("Name.Patronymic", new BsonRegularExpression(partName, "i")));
                var results = await _dropsCollection.Find(filter).ToListAsync();
                return results;
            }

            return await GetAllDrops();
        }
    }
}