using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoExemploAPI.Model.Authentication;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace ProjetoExemploAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        public readonly IAuthService _authService;

        public AuthenticationController(IAuthService authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        [Authorize(Policy = "Cicero")]
        [AllowAnonymous]
        [HttpPost]
        [Route("GerarToken")]
        public IActionResult GerarToken(Login login)
        {
            try
            {
                JwtSecurityToken builedToken = _authService.BuildToken(login);
                string token = new JwtSecurityTokenHandler().WriteToken(builedToken);

                return Ok(new { token, builedToken.Issuer, Expires = builedToken.ValidTo });
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
