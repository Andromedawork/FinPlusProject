namespace FinPlusService.User.Auth
{
    public interface IAuthService
    {
        public Task<bool> Authentication(string login, string password);
    }
}
