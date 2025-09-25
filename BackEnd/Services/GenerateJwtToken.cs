using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class GenerateJwtToken
{
    private IConfiguration _configuration;
    public GenerateJwtToken(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Generate(string userId, string username,string role)
    {
        var key = Encoding.UTF8.GetBytes(_configuration["SecretKey"]!);

        var claims = new[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId),
        new Claim(ClaimTypes.Name, username),
        new Claim(ClaimTypes.Role, role),
        };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(10),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
