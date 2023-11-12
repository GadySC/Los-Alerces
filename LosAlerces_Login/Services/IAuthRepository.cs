using LosAlerces_Login.Models;
using Microsoft.AspNetCore.Identity;

namespace LosAlerces_Login.Services
{
    public interface IAuthRepository
    {
        Task<IdentityResult> RegisterUserAsync(RegisterModel model);
        Task<string> LoginUserAsync(LoginModel model);
    }
}
