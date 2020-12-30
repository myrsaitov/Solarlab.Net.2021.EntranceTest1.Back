using BusinessLogic.Services.Abstractions;
using DataAccess.Entities;
using WebApi.Models.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace WebApi.Controllers
{
    /// <summary>
    /// Контролер логина и регистрации пользователя
    /// </summary>
    [Authorize(Roles = "User")]
    [Route("api/v1/[controller]")]
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AccountController(
                IMapper mapper,
                IUserService userService,
                IConfiguration configuration) : base(mapper)
        {
            _userService = userService;
            _configuration = configuration;
        }

        /// <summary>
        /// Вход пользователя в систему, получение JWT-токена
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] IdentityDataInitializer model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userService.GetUserByEmail(model.Email);
            if (user == null) return BadRequest();

            var result = await _userService.CheckPasswordSignIn(user, model.Password);

            if (result)
            {
                return await GetToken(user);
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return BadRequest(model);

        }

        /// <summary>
        /// Выход пользователя из системы
        /// </summary>
        /// <returns></returns>
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _userService.SignOut();
            return Ok("Logged out");
        }

        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = Mapper.Map<ApplicationUser>(model);
            var result = await _userService.Create(user, model.Password);
            if (result.Succeeded)
                return Ok(user);

            AddErrors(result);
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Тестовый метод для получения данных авторизованного пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet("secure-data")]
        public IActionResult GetSecuredData()
        {
            // Вызывает ошибку
            //return Ok("Secured data " + User.FindFirst(JwtRegisteredClaimNames.Sub).Value);
            return Ok("Secured data " + User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        /// <summary>
        /// Получить имя авторизированного пользователя 
        /// </summary>
        /// <returns></returns>
        [HttpGet("username")]
        public IActionResult GetUserName()
        {
            //return Ok(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return Ok(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private async Task<IActionResult> GetToken(ApplicationUser user)
        {
            var claims = await _userService.GetValidClaims(user);

            var token = new JwtSecurityToken
            (
                issuer: _configuration["Token:Issuer"],
                audience: _configuration["Token:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(60),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"])), SecurityAlgorithms.HmacSha256)
            );

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));

        }

    }
}
