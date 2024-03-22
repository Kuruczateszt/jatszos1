using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wshop3.Dto;

namespace wshop3.Service.IAuth
{
    public interface IAuthService
    {
        Task<string> Register(RegisterRequestDto registerRequestDto);
        Task<bool> AssignRole(string email, string roleName);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
    }
}