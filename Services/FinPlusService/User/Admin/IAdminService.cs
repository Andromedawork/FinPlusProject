namespace FinPlusService
{
    using FinPlus.Domain.Users.Admin;

    public interface IAdminService
    {
        public Task AddAdmin(Admin admin);

        public Task<List<Admin>> GetAllAdmins();
    }
}