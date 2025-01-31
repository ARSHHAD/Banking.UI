using Banking.Domain.Models;
using Banking.Domain.Models.ViewModels;

namespace Banking.UI.Services
{
    public interface IUserService
    {
        Task<ResponseViewModel<bool>> RegisterUser(RegisterModel registerModel);
        Task<ResponseViewModel<User>> GetUserInfo();
    }
}