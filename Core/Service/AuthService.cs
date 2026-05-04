using Core.Input;
using Core.Repository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Core.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;

        public AuthService(
            IUsuarioRepository usuarioRepository,
            string key,
            string issuer,
            string audience)
        {
            _usuarioRepository = usuarioRepository;
            _key = key;
            _issuer = issuer;
            _audience = audience;
        }

        public string Login(LoginInput input)
        {
            var usuario = _usuarioRepository.ObterPorEmail(input.Email);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(input.Senha, usuario.Senha))
                throw new UnauthorizedAccessException("E-mail ou senha inválidos.");

            return GerarToken(usuario.Id, usuario.Email, usuario.Perfil.ToString());
        }

        private string GerarToken(int id, string email, string perfil)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, perfil),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}