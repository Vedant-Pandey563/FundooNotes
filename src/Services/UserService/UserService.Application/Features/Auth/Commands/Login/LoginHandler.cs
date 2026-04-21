using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using UserService.Application.DTOs;
using UserService.Application.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace UserService.Application.Features.Auth.Commands.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, AuthResponseDto?>
    {
        private readonly IUserRepository _repo;
        private readonly IConfiguration _config;

        public LoginHandler(IUserRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config; // read JWT settings from appsettings
        }

        public async Task<AuthResponseDto?> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _repo.GetByEmailAsync(request.Dto.Email);
            if (user == null) return null;

            bool isValid = BCrypt.Net.BCrypt.Verify(
                request.Dto.Password,
                user.PasswordHash
            );

            if (!isValid) return null;

            // read JWT settings
            var secret = _config["JwtSettings:Secret"];
            var issuer = _config["JwtSettings:Issuer"];     // FIX: define issuer
            var audience = _config["JwtSettings:Audience"]; // FIX: define audience

            if (string.IsNullOrWhiteSpace(secret))
                throw new InvalidOperationException("JwtSettings:Secret is missing.");

            var key = Encoding.UTF8.GetBytes(secret);

            var tokenHandler = new JwtSecurityTokenHandler();

            // FIX: only one tokenDescriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("userId", user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email)
                }),

                Expires = DateTime.UtcNow.AddHours(2),

                // must match Program.cs validation
                Issuer = issuer,
                Audience = audience,

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthResponseDto(
                tokenHandler.WriteToken(token),
                user.Email
            );
        }
    }
}