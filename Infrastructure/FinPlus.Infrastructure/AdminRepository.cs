namespace FinPlus.Infrastructure
{
    using FinPlus.Domain.Users.Admin;
    using FinPlus.Infrastructure.Models;
    using Microsoft.Extensions.Options;
    using MongoDB.Bson;
    using MongoDB.Driver;

    public class AdminRepository : IAdminRepository
    {
        private readonly IMongoCollection<Admin> _adminCollection;

        public AdminRepository(IOptions<MongoDBSettingsModel> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DataBaseName);
            _adminCollection = database.GetCollection<Admin>(mongoDBSettings.Value.AdminsCollection);
        }

        public async Task<List<Admin>> GetAllAdmins()
        {
            return await _adminCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task AddAdmin(Admin admin)
        {
            await _adminCollection.InsertOneAsync(new Admin
            {
                Role = admin.Role,
                Login = admin.Login,
                Password = admin.Password,
                Name = admin.Name,
                MobileNumber = admin.MobileNumber,
                OrganisationId = admin.OrganisationId,
            });
            return;
        }
    }
}
