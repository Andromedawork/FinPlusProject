namespace FinPlusService
{
    using FinPlus.Domain.Users.Admin;

    public interface IAdminService
    {
        public Task AddAdmin(Admin admin);

        public Task<List<Admin>> GetAllAdmins();

        public Task<Admin> GetAdminById(string id);

        public Task<bool> UpdateAdmin(Admin admin);

        public Task<List<Admin>> GetAllAdminsByPartName(string partName);
    }
}