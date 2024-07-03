namespace FinPlus.Infrastructure
{
    using FinPlus.Domain.CalendarOfDrops;
    using FinPlus.Infrastructure.Models;
    using Microsoft.Extensions.Options;
    using MongoDB.Bson;
    using MongoDB.Driver;

    public class CalendarRepository : ICalendarRepository
    {
        private readonly IMongoCollection<Records> _recordsCollection;

        public CalendarRepository(IOptions<MongoDBSettingsModel> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DataBaseName);
            _recordsCollection = database.GetCollection<Records>(mongoDBSettings.Value.RecordsCollection);
        }

        public async Task<List<Records>> GetAllRecords()
        {
            return await _recordsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task AddRecords(Records records)
        {
            await _recordsCollection.InsertOneAsync(new Records
            {
                Day = records.Day,
                DropRecords = records.DropRecords,
                CreatedAt = records.CreatedAt,
                OrganisationId = records.OrganisationId,
            });
            return;
        }
    }
}
