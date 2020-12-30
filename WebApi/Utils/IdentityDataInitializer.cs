using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;

namespace WebApi.Utils
{
    /// <summary>
    /// Инициализация ролей и пользователей в системе
    /// </summary>
    public static class IdentityDataInitializer
    {
        private const string RoleAdministrator = "Administrator";
        private const string RoleUser = "User";

        public static void SeedData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            /// Проверяет, существует ли роль юзера и администратор, если не существует, то он их создает
            SeedRoles(roleManager);
            /// Ищет админ, если не находит, то создает его. То же самое с юзером
            SeedUsers(userManager);
            /// Usermanager - уровень бизнес-логики, поэтому в дале пароли храним так
        }

        private static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByNameAsync("admin").Result == null)
            {
                var user = new ApplicationUser { UserName = "admin@test.ru", Email = "admin@test.ru", FirstName = "Ivan", LastName = "Ivanov" };
                var result = userManager.CreateAsync(user, "P@ssw0rd1!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, RoleAdministrator).Wait();
                }
            }

            if (userManager.FindByNameAsync("user2").Result == null)
            {
                var user = new ApplicationUser { UserName = "user@test.ru", Email = "user@test.ru", FirstName = "Piter", LastName = "Petrov" };
                var result = userManager.CreateAsync(user, "P@ssw0rd1!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, RoleUser).Wait();
                }
            }
        }

        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync(RoleUser).Result)
            {
                var role = new IdentityRole { Name = RoleUser };
                var roleResult = roleManager.CreateAsync(role).Result;
            }


            if (!roleManager.RoleExistsAsync(RoleAdministrator).Result)
            {
                var role = new IdentityRole { Name = RoleAdministrator };
                var roleResult = roleManager.CreateAsync(role).Result;
            }
        }
    }
}
