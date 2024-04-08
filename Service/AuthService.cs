using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using wshop3.Datab;
using wshop3.Dto;
using wshop3.Model;
using wshop3.Service.IAuth;

namespace wshop3.Service
{
    public class AuthService
    {
        public class AuthService1 : IAuthService
        {
            private readonly IdentityContext dbcontext;
            private readonly UserManager<IdentityFelhasznalo> userManager;
            private readonly RoleManager<IdentityRole> roleManager;

            private readonly IJwtTokenGenerator jwtTokenGenerator;

            public AuthService1(IdentityContext appDbContext, UserManager<IdentityFelhasznalo> userManager, RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator)
            {
                this.dbcontext = appDbContext;
                this.userManager = userManager;
                this.roleManager = roleManager;
                this.jwtTokenGenerator = jwtTokenGenerator;
            }

            public async Task<bool> AssignRole(string email, string roleName)
            {
                var user = dbcontext.identityFelhasznalok.FirstOrDefault(user => user.Email != null && user.Email.ToLower() == email.ToLower());

                if (user != null)
                {
                    if (!roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                    {
                        //Itt készülnek a Role-ok
                        roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                    }

                    await userManager.AddToRoleAsync(user, roleName);

                    return true;
                }

                return false;

            }

            public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
            {
                var user = await dbcontext.identityFelhasznalok.
                    FirstOrDefaultAsync(user => user.UserName != null && user.UserName.ToLower() == loginRequestDto.UserName.ToLower());

                bool isValid = await userManager.CheckPasswordAsync(user ?? new IdentityFelhasznalo(), loginRequestDto.Password);

                if (user == null || isValid == false)
                {
                    return new LoginResponseDto() { Token = "" };
                }

                var roles = await userManager.GetRolesAsync(user);
                var token = jwtTokenGenerator.GenerateToken(user, roles);

                LoginResponseDto loginResponseDto = new()
                {
                    Token = token
                };

                return loginResponseDto;


            }

            public async Task<string> Register(RegisterRequestDto registerRequestDto)
            {
                IdentityFelhasznalo user = new()
                {
                    UserName = registerRequestDto.UserName,
                    NormalizedUserName = registerRequestDto.UserName.ToUpper(),
                    Email = registerRequestDto.Email,
                    TeljesNev = registerRequestDto.TeljesNev,
                    Iranyitoszam = registerRequestDto.Iranyitoszam,
                    Varos = registerRequestDto.Varos,
                    Utca = registerRequestDto.Utca,
                    Hazszam = registerRequestDto.Hazszam
                };

                try
                {
                    var result = await userManager.CreateAsync(user, registerRequestDto.Password);

                    if (result.Succeeded)
                    {
                        var userToReturn = dbcontext.identityFelhasznalok.First(user => user.UserName == registerRequestDto.UserName);

                        RegisterResponseDto registerResponseDto = new()
                        {
                            UserName = userToReturn.UserName ?? "",
                            Email = userToReturn.Email ?? "",
                            TeljesNev = userToReturn.TeljesNev,
                            Iranyitoszam = userToReturn.Iranyitoszam,
                            Varos = userToReturn.Varos,
                            Utca = userToReturn.Utca,
                            Hazszam = userToReturn.Hazszam
                        };

                        return "";
                    }
                    else
                    {
                        return result.Errors.FirstOrDefault()?.Description ?? "";
                    }


                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}