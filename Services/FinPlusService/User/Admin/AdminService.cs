namespace FinPlusService
{
    using FinPlus.Domain.Users.Admin;

    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task AddAdmin(Admin admin)
        {
            admin.Password = BCrypt.Net.BCrypt.HashPassword(admin.Password);
            await _adminRepository.AddAdmin(admin);
        }

        public async Task<List<Admin>> GetAllAdmins()
        {
            return await _adminRepository.GetAllAdmins();
        }

        public async Task<Admin> GetAdminById(string id)
        {
            return await _adminRepository.GetAdminById(id);
        }

        public async Task<bool> UpdateAdmin(Admin admin)
        {
            return await _adminRepository.UpdateAdmin(admin);
        }

        public async Task<List<Admin>> GetAllAdminsByPartName(string partName)
        {
            return await _adminRepository.GetAllAdminsByPartName(partName);
        }
    }
}