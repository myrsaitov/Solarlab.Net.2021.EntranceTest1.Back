using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Categories
{
    public class CategoryUpdateModel : BaseCategoryComposeModel
    {
        [Required]
        [Range(1, Int32.MaxValue)]
        public int Id { get; set; }
    }
}