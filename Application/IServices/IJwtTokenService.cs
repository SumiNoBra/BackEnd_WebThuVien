using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IJwtTokenService
    {
        public string Generate(int userId, string username, string role);
        public string GenerateToken(ClaimsPrincipal principal);
    }
}
