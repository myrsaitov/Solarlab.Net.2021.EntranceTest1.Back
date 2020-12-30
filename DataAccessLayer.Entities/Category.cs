using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.Entities
{
    /// <summary>
    /// Категория
    /// </summary>
    public class Category
    {
        /// <summary>
        /// id категории
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название категории
        /// </summary>
        
        public string Name { get; set; }

        /// <summary>
        /// ключ для связи с родительской категорией
        /// </summary>
        public int? ParentCategoryId { get; set; }

        /// <summary>
        /// Родительская категория
        /// </summary>
        public virtual Category ParentCategory { get; set; }

        /// <summary>
        /// Подкатегории родительской категории
        /// </summary>
        public virtual ICollection<Category> Childs { get; set; }
    }
}
