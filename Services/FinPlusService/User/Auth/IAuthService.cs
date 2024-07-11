namespace FinPlusService.User.Auth
{
    public interface IAuthService
    {
        public Task<string> Authentication(string login, string password);
    }
}
