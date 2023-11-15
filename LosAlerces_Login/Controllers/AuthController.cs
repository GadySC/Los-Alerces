using LosAlerces_Login.Models;
using LosAlerces_Login.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LosAlerces_Login.Controllers
{
    [Route("v1/login")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterModel model)
        {
            var result = await _authRepository.RegisterUserAsync(model);

            if (result.Succeeded)
            {
                return Ok(new { message = "Usuario registrado exitosamente." });
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var token = await _authRepository.LoginUserAsync(model);

            if (!string.IsNullOrEmpty(token))
            {
                return Ok(new { token = token });
            }

            return Unauthorized(new { message = "Credenciales incorrectas." });
        }

        [HttpPost("asignar-rol")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> AssignRole([FromBody] RoleAssignmentModel model)
        {
            var result = await _authRepository.AssignRoleToUserAsync(model.UserEmail, model.RoleName);

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(result.Errors);
        }

    }
}
