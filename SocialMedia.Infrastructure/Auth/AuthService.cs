using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SocialMedia.Infrastructure.Auth
{
	public class AuthService : IAuthService
	{
		private readonly IConfiguration _configuration;

		public AuthService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public async Task<string> ComputeHashAsync(string password)
		{
			using (var hash = SHA256.Create())
			{
				var bytes = hash.ComputeHash(Encoding.UTF8.GetBytes(password));
				var builder = new StringBuilder();

				foreach (var b in bytes)
				{
					builder.Append(b.ToString("x2"));
				}

				return await Task.FromResult(builder.ToString());
			}
		}

		public async Task<string> GenerateTokenAsync(string email, string role, Guid accountId)
		{
			var issuer = _configuration["Jwt:Issuer"];
			var audience = _configuration["Jwt:Audience"];

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
			var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var claims = new[]
			{
				new Claim("userName", email),
				new Claim(ClaimTypes.Role, role),
				new Claim("accountId", accountId.ToString())
			};

			var token = new JwtSecurityToken(
				issuer: issuer,
				audience: audience,
				claims: claims,
				expires: DateTime.Now.AddMinutes(120),
				signingCredentials: credentials);

			var handler = new JwtSecurityTokenHandler();
			return await Task.FromResult(handler.WriteToken(token));
		}
	}
}
