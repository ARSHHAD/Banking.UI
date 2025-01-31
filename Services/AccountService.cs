using Banking.Domain.Models;
using Banking.Domain.Models.ViewModels;

namespace Banking.UI.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;
        private readonly IAuthService _authService;

        public AccountService(HttpClient httpClient, IAuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
        }

        public async Task<ResponseViewModel<Account>> GenerateAccount(Account account)
        {
            var response = new ResponseViewModel<Account>();
            try
            {
                await _authService.AddAuthHeader(_httpClient);

                var responseData = await _httpClient.PostAsJsonAsync("/api/account/create", account);

                if (responseData.IsSuccessStatusCode)
                {
                    response = await responseData.Content.ReadFromJsonAsync<ResponseViewModel<Account>>();
                }
            }
            catch (Exception ex)
            {
                response.Message = "An error occured";
            }

            return response;
        }
    }
}
