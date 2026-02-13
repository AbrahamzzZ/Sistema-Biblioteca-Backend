using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using bibliosys.be.application.Interfaces.Service;
using bibliosys.be.application.Models;
using bibliosys.be.domain.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace bibliosys.be.application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IBibliotecarioRepository _bibliotecarioRepository;
        private readonly JwtSettings _jwtSettings;

        public AuthService(
            IBibliotecarioRepository bibliotecarioRepository,
            IOptions<JwtSettings> jwtOptions)
        {
            _bibliotecarioRepository = bibliotecarioRepository;
            _jwtSettings = jwtOptions.Value;
        }

        public async Task<string?> LoginAsync(string email, string password)
        {
            var bibliotecario = await _bibliotecarioRepository.GetByEmailAsync(email);
            if (bibliotecario == null || !bibliotecario.Estado)
            {
                return null;
            }

            // Comparación directa para mantenerlo simple (no usar en producción).
            if (bibliotecario.Password != password)
            {
                return null;
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, bibliotecario.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, bibliotecario.Email),
                new Claim("nombre", bibliotecario.Nombre)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(_jwtSettings.ExpirationHours),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

