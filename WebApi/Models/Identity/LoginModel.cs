using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Identity
{
    /// <summary>
    /// Модель входа пользователя
    /// </summary>
    public class IdentityDataInitializer
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
