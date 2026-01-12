using AlbCarRent.Datalayer;
using AlbCarRent.Modules.AuthModule.Application.Interfaces;
using AlbCarRent.Modules.AuthModule.Domain;
using AlbCarRent.Modules.AuthModule.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AlbCarRent.Modules.AuthModule.Application.Services
{
    public class AuthService : IAuthService
    {
        public IAuthRepository _authRepository;
        public IConfiguration _configuration;
        public UserManager<AppUser> _userManager;
        public RoleManager<IdentityRole> _roleManager;

        public AuthService(IAuthRepository authRepository, IConfiguration configuration,UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _authRepository = authRepository;
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public string GenerateToken(string userId, string userEmail, string[] roles)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings["SecretKey"])
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, userId),
        new Claim(ClaimTypes.NameIdentifier, userId),
        new Claim(ClaimTypes.Email, userEmail),
        new Claim(ClaimTypes.Name, userEmail),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            claims.AddRange(
                roles.Select(role => new Claim(ClaimTypes.Role, role))
            );

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    Convert.ToDouble(jwtSettings["ExpiryMinutes"])
                ),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public async Task<LoginResponse> Login(LoginRequest request)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    return new LoginResponse
                    {
                        Success = false,
                        Message = "Invalid email or password."
                    };
                }

                var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);
                if (!passwordValid)
                {
                    return new LoginResponse
                    {
                        Success = false,
                        Message = "Invalid email or password."
                    };
                }

                var roles = await _userManager.GetRolesAsync(user);

                var rolesArray = roles.ToArray();

                var token = GenerateToken(
                    user.UserName!,
                    user.Email!,
                    rolesArray
                     );

                return new LoginResponse
                {
                    Success = true,
                    Message = "Login successful.",
                    Token = token,
                    Id = user.Id,
                    Role = roles.First()
                };

            }
            catch(Exception ex)
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = "Service Error,Please Try Again",
                };
            }
        }

        public async Task<RegisterResponse> Register(RegisterRequest request)
        {
            try
            {
                var userExists = await _userManager.FindByEmailAsync(request.Email);
                if (userExists != null)
                {
                    return new RegisterResponse
                    {
                        Success = false,
                        Message = "User already exists."
                    };
                }

                var newUser = new AppUser
                {
                    FullName = request.FullName,
                    UserName = request.Username,
                    Email = request.Email
                };

                var creationResult = await _userManager.CreateAsync(newUser, request.Password);
                if (!creationResult.Succeeded)
                {
                    return new RegisterResponse
                    {
                        Success = false,
                        Message = string.Join(
                            "; ",
                            creationResult.Errors.Select(e => e.Description)
                        )
                    };
                }

                string role = "";

                if (request.IsBussinessAccount)
                {
                    role = "Bussiness";
                }
                else
                {
                    role = "Client";
                }

                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }

                await _userManager.AddToRoleAsync(newUser, role);

                var roles = await _userManager.GetRolesAsync(newUser);

                var rolesArray = roles.ToArray();

                var token = GenerateToken(
                    newUser.UserName!,
                    newUser.Email!,
                    rolesArray
                );

                return new RegisterResponse
                {
                    Success = true,
                    Message = "User was created successfully.",
                    Token = token,
                    Id = newUser.Id,
                    Role = role
                };


            }
            catch (Exception ex)
            {
                return new RegisterResponse
                {
                    Success = false,
                    Message = "Service Error,Please Try Again",
                };
            }
        }
    }
}
