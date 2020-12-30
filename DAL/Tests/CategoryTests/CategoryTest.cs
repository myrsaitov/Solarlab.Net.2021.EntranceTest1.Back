using AutoMapper;
using BusinessLogic.Services.Abstractions;
using BusinessLogic.Services.Mapping;
using DataAccess.Entities;
using DataAccess.Repositories.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CategoryTests
{
    public partial class CategoryTest
    {
        private readonly IMapper _mapper;
        private Mock<ICategoryRepository> categoryRepositoryMock = new Mock<ICategoryRepository>();
        private ICategoryService _categoryService;

        public CategoryTest()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceMappings());
            });
            _mapper = mockMapper.CreateMapper();

            Category category = new Category()
            {
                Id = 1,
                Name = "АвтоМото",
            };

            //Настройка moq - объекта
            categoryRepositoryMock.Setup(_ => _.GetById(It.IsAny<int>())).ReturnsAsync(category);
            categoryRepositoryMock.Setup(_ => _.Delete(It.IsAny<int>()));
            _categoryService = new BusinessLogic.Services.CategoryService(
                _mapper,
                categoryRepositoryMock.Object);
        }




    }
}