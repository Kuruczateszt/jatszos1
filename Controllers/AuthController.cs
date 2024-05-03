using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wshop3.Dto;
using wshop3.Service.IAuth;

namespace wshop3.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterRequestDto registerRequestDto)
        {
            var errorMessage = await authService.Register(registerRequestDto);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                return StatusCode(400, errorMessage);
            }
            return StatusCode(201, "Sikeres regisztráció");
        }

        [AllowAnonymous]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        [HttpPost("AssignRole")]
        public async Task<ActionResult> AssignRole([FromBody] RoleDto model)
        {

            var assignRoleSuccesful = await authService.AssignRole(model.Email, model.Role.ToUpper());

            if (!assignRoleSuccesful)
            {
                return BadRequest();
            }


            return StatusCode(200, "Sikeres szerep létrehozás.");
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequestDto model)
        {
            var loginResponse = await authService.Login(model);

            if (loginResponse.Token == null)
            {
                return BadRequest("Nem megfelelő username vagy jelszó!");
            }

            return StatusCode(200, loginResponse);

        }
    }
}