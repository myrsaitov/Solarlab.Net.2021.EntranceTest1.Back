using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Services.Contracts.Models
{
    /// <summary>
    /// Категория. Транспортный объект.
    /// </summary>
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CategoryDto ParentCategory { get; set; }
        public ICollection<CategoryDto> Childs { get; set; }
    }
}
