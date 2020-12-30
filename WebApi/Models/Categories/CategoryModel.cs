using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Categories
{
    public class CategoryModel
    {
        [Range(1, Int32.MaxValue)]
        public int Id { get; set; }

        public string Name { get; set; }

        public CategoryModel ParentCategory { get; set; }

        public ICollection<CategoryModel> Childs { get; set; }
    }
}