using System.Security.Claims;
using Application.DTOs;
namespace Application.IServices
{
    public interface IJwtTokenService
    {
        public string Generate(int userId, string username, string role);
        public string GenerateToken(ClaimsPrincipal principal);
        public string GenerateAccessToken(int userId, string email, string role);
        public RefreshToken GenerateRefreshToken();
    }
}
