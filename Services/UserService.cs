using Banking.Domain.Models;
using Banking.Domain.Models.ViewModels;
using Banking.UI.Components.Pages;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace Banking.UI.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly IAuthService _authService;

        public UserService(HttpClient httpClient, IAuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
        }

        public async Task<ResponseViewModel<User>> GetUserInfo()
        {
            var response = new ResponseViewModel<User>();
            try
            {
                await _authService.AddAuthHeader(_httpClient);
                response = await _httpClient.GetFromJsonAsync<ResponseViewModel<User>>("/api/user/userinfo");
            }
            catch (Exception ex)
            {
                response.Message = "An Error Occured";
            }

            return response;
        }

        public async Task<ResponseViewModel<bool>> RegisterUser(RegisterModel registerModel)
        {
            var response = new ResponseViewModel<bool>();

            try
            {
                var responseData = await _httpClient.PostAsJsonAsync("/api/user/register", registerModel);

                response = await responseData.Content.ReadFromJsonAsync<ResponseViewModel<bool>>();
            }
            catch (Exception ex)
            {
                //log
                response.Success = false;
            }

            return response;
        }
    }

}
