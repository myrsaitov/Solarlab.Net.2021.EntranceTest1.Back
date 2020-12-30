using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Categories
{
    public class BaseCategoryComposeModel
    {
        [Range(1, Int32.MaxValue)]
        public int? ParentCategoryId { get; set; }

        public string Name { get; set; }
    }
}