using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using ProjetoExemploAPI.Model.Authentication;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjetoExemploAPI.Services
{
    public class AuthService : IAuthService
    {
        #region Propriedades

        private AuthToken _token;
        private IConfiguration _config;
        public readonly ILogger _logger;

        #endregion Propriedades

        #region Construtores

        public AuthService(IConfiguration config, ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        #endregion Construtores

        #region Métodos

        public JwtSecurityToken BuildToken(Login usuario)
        {
            try
            {
                string secret = _config["JwtToken:SecretKey"];
                byte[] secretByte = Encoding.UTF8.GetBytes(secret);
                SymmetricSecurityKey key = new SymmetricSecurityKey(secretByte);
                SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                List<Claim> clains = new List<Claim> { new Claim(ClaimTypes.Name, usuario.Username) };
                JwtSecurityToken token = new JwtSecurityToken("https:grupotechnos.com.br", "https:grupotechnos.com.br", clains, expires: DateTime.Now.AddMinutes(30), signingCredentials: creds);

                return token;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AuthController - BuildToken");

                throw ex;
            }
        }
        #endregion Métodos
    }
}
