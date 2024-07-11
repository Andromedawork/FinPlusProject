namespace FinPlusService.User.Auth
{
    using FinPlus.Domain.Users.Admin;
    using FinPlus.Domain.Users.Trafer;

    public class AuthService : IAuthService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly ITrafferRepository _trafferRepository;

        public AuthService(IAdminRepository adminRepository, ITrafferRepository trafferRepository)
        {
            _adminRepository = adminRepository;
            _trafferRepository = trafferRepository;
        }

        public async Task<string> Authentication(string login, string password)
        {
            var admin = await _adminRepository.GetAdminByLogin(login);

            if (admin == null)
            {
                var traffer = await _trafferRepository.GetTrafferByLogin(login);

                if (traffer == null)
                {
                    return null;
                }

                if (BCrypt.Net.BCrypt.Verify(password, traffer.Password))
                {
                    return "Traffer";
                }

                return null;
            }

            if (BCrypt.Net.BCrypt.Verify(password, admin.Password))
            {
                return admin.Role.ToString();
            }

            return null;
        }
    }
}
