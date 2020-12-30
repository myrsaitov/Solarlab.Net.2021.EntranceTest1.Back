using BusinessLogic.Services.Abstractions;
using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    /// <summary>
    /// Реализация сервиса работы с пользователями
    /// </summary>
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        /// <inheritdoc />
        public async Task<ApplicationUser> GetUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        /// <inheritdoc />
        public async Task<bool> CheckPasswordSignIn(ApplicationUser user, string password)
        {
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: false);
            return result.Succeeded;
        }

        /// <inheritdoc />
        public Task SignOut()
        {
            return _signInManager.SignOutAsync();
        }

        /// <inheritdoc />
        public async Task<IdentityResult> Create(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded) return result;

            await _userManager.AddToRoleAsync(user, "User");
            await _signInManager.SignInAsync(user, isPersistent: false);

            return result;
        }

        /// <inheritdoc />
        public async Task<List<Claim>> GetValidClaims(ApplicationUser user)
        {
            var options = new IdentityOptions();
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(options.ClaimsIdentity.UserIdClaimType, user.Id),
                new Claim(options.ClaimsIdentity.UserNameClaimType, user.UserName)
            };
            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);
            claims.AddRange(userClaims);
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
                var role = await _roleManager.FindByNameAsync(userRole);
                if (role == null) continue;

                var roleClaims = await _roleManager.GetClaimsAsync(role);
                claims.AddRange(roleClaims);
            }
            return claims;
        }
    }
}