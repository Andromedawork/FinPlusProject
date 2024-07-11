namespace FinPlus.Domain.Users.Admin
{
    public interface IAdminRepository
    {
        public Task<List<Admin>> GetAllAdmins();

        public Task AddAdmin(Admin admin);

        public Task<Admin> GetAdminByLogin(string login);
    }
}
