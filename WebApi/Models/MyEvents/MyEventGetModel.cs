using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.Tags;

namespace WebApi.Models
{
    public class MyEventGetModel
    {
        /// <summary>
        /// Заголовок
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Тело объявления
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Пользователь, создавший объявление
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// Идентификатор удаления
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Идентификатор категории
        /// </summary>
        [Required]
        [Range(1, Int32.MaxValue)]
        public int CategoryId { get; set; }

        /// <summary>
        /// Теги
        /// </summary>
        public ICollection<TagModel> Tags { get; set; }



    }
}
