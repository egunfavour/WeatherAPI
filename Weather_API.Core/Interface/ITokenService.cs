using Weather_API.Domain.Models;

namespace Weather_API.Core.Interface
{
    public interface ITokenService
    {
        Task<string> GenerateToken(AppUser user);
        string GenerateRefreshToken();
    }
}