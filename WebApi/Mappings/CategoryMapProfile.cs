using AutoMapper;
using BusinessLogic.Services.Contracts.Models;
using WebApi.Models;
using WebApi.Models.Categories;

namespace WebApi.Mappings
{
    public class CategoryMapProfile : Profile
    {
        public CategoryMapProfile()
        {
            CreateMap<CategoryModel, CategoryDto>();
            CreateMap<CategoryCreateModel, CategoryCreateDto>();
            CreateMap<CategoryUpdateModel, CategoryUpdateDto>();
        }
    }
}
