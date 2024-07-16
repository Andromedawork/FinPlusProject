namespace FinPlus.Infrastructure
{
    using FinPlus.Domain.Users.Admin;
    using FinPlus.Domain.Users.Trafer;
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

        public async Task<Admin> GetAdminByLogin(string login)
        {
            return await _adminCollection.Find(a => a.Login == login).FirstOrDefaultAsync();
        }

        public async Task<Admin> GetAdminById(string id)
        {
            return await _adminCollection.Find(a => a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAdmin(Admin admin)
        {
            var filter = Builders<Admin>.Filter.Eq(a => a.Id, admin.Id);
            var update = Builders<Admin>.Update
            .Set(a => a.Name, admin.Name)
            .Set(a => a.Role, admin.Role)
            .Set(a => a.MobileNumber, admin.MobileNumber)
            .Set(a => a.Login, admin.Login)
            .Set(a => a.OrganisationId, admin.OrganisationId);

            var updateResult = await _adminCollection.UpdateOneAsync(filter, update);

            return updateResult.ModifiedCount > 1;
        }

        public async Task<List<Admin>> GetAllAdminsByPartName(string partName)
        {
            if (!string.IsNullOrEmpty(partName))
            {
                var filter = Builders<Admin>.Filter.Or(
                Builders<Admin>.Filter.Regex("Name.Surname", new BsonRegularExpression(partName, "i")),
                Builders<Admin>.Filter.Regex("Name.Name", new BsonRegularExpression(partName, "i")),
                Builders<Admin>.Filter.Regex("Name.Patronymic", new BsonRegularExpression(partName, "i")));

                var results = await _adminCollection.Find(filter).ToListAsync();
                return results;
            }

            return await GetAllAdmins();
        }

        public async Task DeleteAdmin(string id)
        {
            await _adminCollection.DeleteOneAsync(id);
        }
    }
}
