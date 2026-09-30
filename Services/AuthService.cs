using Api.Common.Dtos.Users;
using Api.Common.Interfaces;
using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Services
{
    public class AuthService(ApplicationDbContext context, IConfiguration configuration) : IAuthService
    {
        public async Task<string?> LoginAsync(SignInUserDto request)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null) return null;

            if (new PasswordHasher<User>().VerifyHashedPassword(new User(), user.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
            {
                return null;
            }

            var token = await createToken(user);

            return token;
        }

        public async Task<UserDto?> RegisterAsync(SignUpUserDto request)
        {
            var user = await context.Users.AnyAsync(u => u.Email == request.Email);

            if (user) return null;

            var memberRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == "member");

            var newUser = new User
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = new PasswordHasher<User>().HashPassword(new User(), request.Password),
                IsActive = true,
                RoleId = memberRole!.Id
            };

            context.Users.Add(newUser);
            await context.SaveChangesAsync();

            return new UserDto
            {
                Name = newUser.Name,
                Email = newUser.Email,
                IsActive = newUser.IsActive,
                Role = (await context.Roles.FindAsync(newUser.RoleId))?.Name ?? string.Empty
            };
        }

        private async Task<string> createToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, (await context.Roles.FindAsync(user.RoleId))?.Name ?? string.Empty)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("AppSettings:Issuer"),
                audience: configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
