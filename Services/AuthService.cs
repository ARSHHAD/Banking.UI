using Banking.Domain.Models;
using Banking.Domain.Models.ViewModels;
using Microsoft.JSInterop;
namespace Banking.UI.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;

        public AuthService(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
        }

        public async Task<ResponseViewModel<string>> Login(LoginModel login)
        {
            var response = new ResponseViewModel<string>();

            try
            {
                var responseData = await _httpClient.PostAsJsonAsync("api/auth/login", login);

                response = await responseData.Content.ReadFromJsonAsync<ResponseViewModel<string>>();

                if (response != null || !string.IsNullOrEmpty(response.Data))
                {
                    await SetTokenAsync(response.Data);
                }
            }
            catch (Exception ex)
            {
                response.Message = "An Error Occured";
            }

            return response;
        }

        // Get the stored token from localStorage
        public async Task<string> GetTokenAsync()
        {
            return await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "jwt");
        }

        // Store the JWT token in localStorage
        public async Task SetTokenAsync(string? token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                await _jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "jwt", token);
            }
        }

        // Remove the JWT token from localStorage
        public async Task RemoveTokenAsync()
        {
            await _jsRuntime.InvokeVoidAsync("sessionStorage.removeItem", "jwt");
        }

        // Add Bearer token to the request header
        public async Task AddAuthHeader(HttpClient client)
        {
            var token = await GetTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}