using Banking.Domain.Models;
using Banking.Domain.Models.ViewModels;

namespace Banking.UI.Services
{
    public interface IAccountService
    {
        Task<ResponseViewModel<Account>> GenerateAccount(Account account);
    }
}