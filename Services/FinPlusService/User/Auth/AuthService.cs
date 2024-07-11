namespace FinPlusService.User.Auth
{
    using FinPlus.Domain.Users.Admin;

    public class AuthService : IAuthService
    {
        private readonly IAdminRepository _adminRepository;

        public AuthService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<bool> Authentication(string login, string password)
        {
            var admin = await _adminRepository.GetAdminByLogin(login);

            if (admin == null)
            {
                return false;
            }

            return BCrypt.Net.BCrypt.Verify(password, admin.Password);
        }
    }
}
