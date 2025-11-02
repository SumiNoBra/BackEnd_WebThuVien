using Application.IServices;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class JwtTokenService :IJwtTokenService
{
    private IConfiguration _configuration;
    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Generate(int userId, string username,string role)
    {
        var key = Encoding.UTF8.GetBytes(_configuration["SecretKey"]!);

        var claims = new[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
        new Claim(ClaimTypes.Name, username),
        new Claim(ClaimTypes.Role, role),
        };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    public string GenerateToken(ClaimsPrincipal principal)
    {
        // Lấy thông tin từ cấu hình
        var secretKey = _configuration["SecretKey"]
                        ?? throw new InvalidOperationException("SecretKey not found.");
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        // Lấy Issuer và Audience (nếu có)
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];

        // 1. Lấy thông tin cần thiết từ principal (ví dụ: User ID, Email)
        // LƯU Ý: NameClaimType của Google thường là Email hoặc NameIdentifier
        var email = principal.FindFirst(ClaimTypes.Email)?.Value;
        var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(email))
        {
            throw new InvalidOperationException("Required claims (Email) missing from principal.");
        }

        // 2. Định nghĩa Claims cho JWT Token MỚI của bạn
        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId ?? Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email, email),
                // Bạn có thể thêm các Claim tùy chỉnh khác ở đây (ví dụ: Role từ DB của bạn)
                // new Claim(ClaimTypes.Role, "User")
            };

        // 3. Tạo Security Token Descriptor
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(2), // Token hết hạn sau 2 giờ
            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature),
            Issuer = issuer,
            Audience = audience
        };

        // 4. Phát hành Token
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
