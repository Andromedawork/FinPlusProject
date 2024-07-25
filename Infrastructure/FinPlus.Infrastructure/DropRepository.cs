namespace FinPlus.Infrastructure
{
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
            await _dropsCollection.InsertOneAsync(drop);
            return;
        }

        public async Task<Drop> GetDropById(string id)
        {
            return await _dropsCollection.Find(d => d.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateDropData(string id, FIO name, string referalId, DropPassport pass, string mobileNumber, string cardNumber, string personalReferalId, DateTime dateOfBirth, string telegram)
        {
            var filter = Builders<Drop>.Filter.Eq(d => d.Id, id);
            var update = Builders<Drop>.Update
                .Set(d => d.Name, name)
                .Set(d => d.ReferalId, referalId)
                .Set(d => d.Pass, pass)
                .Set(d => d.MobileNumber, mobileNumber)
                .Set(d => d.CardNumber, cardNumber)
                .Set(d => d.PersonalReferalId, personalReferalId)
                .Set(d => d.Telegram, telegram)
                .Set(d => d.DateOfBirth, dateOfBirth);

            var updateResult = await _dropsCollection.UpdateOneAsync(filter, update);

            return updateResult.ModifiedCount > 0;
        }

        public async Task<bool> UpdateDropStep(string id, Dictionary<string, DropStep> step, Dictionary<string, List<Offer>> offers, string comment)
        {
            var filter = Builders<Drop>.Filter.Eq(d => d.Id, id);
            var currentDrop = await _dropsCollection.Find(filter).FirstOrDefaultAsync();

            if (currentDrop == null)
            {
                return false;
            }

            var updatedSteps = currentDrop.Steps;

            foreach (var kvp in step)
            {
                updatedSteps[kvp.Key] = kvp.Value;
            }

            var updatedOffers = currentDrop.Offers;

            foreach (var kvp in offers)
            {
                updatedOffers[kvp.Key] = kvp.Value;
            }

            var updates = new List<UpdateDefinition<Drop>>();

            var stepsUpdate = Builders<Drop>.Update.Set(d => d.Steps, updatedSteps);
            updates.Add(stepsUpdate);

            var offersUpdate = Builders<Drop>.Update.Set(d => d.Offers, updatedOffers);
            updates.Add(offersUpdate);

            var commentsUpdate = Builders<Drop>.Update.Push(d => d.Comments, comment);
            updates.Add(commentsUpdate);

            var combinedUpdate = Builders<Drop>.Update.Combine(updates);
            var updateResult = await _dropsCollection.UpdateOneAsync(filter, combinedUpdate);

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

        public async Task DeleteDrop(string id)
        {
            await _dropsCollection.DeleteOneAsync(id);
        }
    }
}