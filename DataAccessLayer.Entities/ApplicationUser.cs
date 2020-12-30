using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Entities
{
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Имя
        /// </summary>
        [MaxLength(256)]
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [MaxLength(256)]
        [Required]
        public string LastName { get; set; }
    }
}
