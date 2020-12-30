using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogic.Services.Abstractions
{
    /// <summary>
    /// Интерфейс сервиса работы с пользователями
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Получение пользователя по его e-mail
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<ApplicationUser> GetUserByEmail(string email);

        /// <summary>
        /// Проверка пароля пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<bool> CheckPasswordSignIn(ApplicationUser user, string password);

        /// <summary>
        /// Выход пользователя из системы
        /// </summary>
        /// <returns></returns>
        Task SignOut();

        /// <summary>
        /// Создание нового пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<IdentityResult> Create(ApplicationUser user, string password);

        /// <summary>
        /// Формирование клаймсов пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<List<Claim>> GetValidClaims(ApplicationUser user);
    }
}