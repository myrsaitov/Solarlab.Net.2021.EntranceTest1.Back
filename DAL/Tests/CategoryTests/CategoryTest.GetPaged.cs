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
            [Fact]
        public void GetPaged_Should_Return_Error_If_Page_Is_Zero()
        {
            //Arrange
            int page = 0;
            int pageSize = 8;

            //Act
            var result = _categoryService.GetPaged(page, pageSize) ;

            //Assert
            Assert.False (result.Result.Success);
        }

        //[Fact]
        //public void Create_Should_Return_Error_If_Repository_Throws_Exception()
        //{
        //    //Arrange
        //    BusinessLogic.Services.Contracts.Models.CategoryDto emptyNameCategory = new BusinessLogic.Services.Contracts.Models.CategoryDto();
        //    _CategoryRepository.Setup(_ => _.Add(It.IsAny<Category>())).Throws<Exception>();

        //    //Act
        //    var result = _categoryService.Create(emptyNameCategory);

        //    //Assert
        //    Assert.False(result.Result.Success);
        //}

        //[Fact]
        //public void Create_Should_Return_Error_If_Argument_Is_Null()
        //{
        //    //Arrange
        //    BusinessLogic.Services.Contracts.Models.CategoryDto emptyNameCategory = null;

        //    //Act
        //    var result = _categoryService.Create(emptyNameCategory);

        //    //Assert
        //    Assert.False(result.Result.Success );
        //}

    }
}
