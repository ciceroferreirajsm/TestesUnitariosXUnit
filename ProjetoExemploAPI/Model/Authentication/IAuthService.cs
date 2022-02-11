using System.IdentityModel.Tokens.Jwt;

namespace ProjetoExemploAPI.Model.Authentication
{
    public interface IAuthService
    {
        public JwtSecurityToken BuildToken(Login usuario);
    }
}
