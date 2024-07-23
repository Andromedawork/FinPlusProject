namespace FinPlus.Domain.Users.Admin
{
    public interface IAdminRepository
    {
        public Task<List<Admin>> GetAllAdmins();

        public Task AddAdmin(Admin admin);

        public Task<Admin> GetAdminByLogin(string login);

        public Task<Admin> GetAdminById(string id);

        public Task<Admin> GetAdminByOrganisationId(string id);

        public Task<List<Admin>> GetAllAdminsByPartName(string partName);

        public Task<bool> UpdateAdmin(Admin admin);

        public Task DeleteAdmin(string id);
    }
}
