using Banking.Domain.Models.ViewModels;

namespace Banking.UI.Services
{
    public interface IAuthService
    {
        Task<ResponseViewModel<string>?> Login(LoginModel login);
        Task<string> GetTokenAsync();
        Task SetTokenAsync(string token);
        Task RemoveTokenAsync();
        Task AddAuthHeader(HttpClient client);
    }
}
