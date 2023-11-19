using WukkamanCleaningAgencyApi.Models;

namespace WukkamanCleaningAgencyApi.Services
{
    public interface IAuthService
    {
        Task<string?> GenerateToken(User user);
        Task<bool> Login(User user);
        Task<bool> RegisterUser(User user);
        Task<bool> AssignRoles(string userName, IEnumerable<string> roles);
    }
}