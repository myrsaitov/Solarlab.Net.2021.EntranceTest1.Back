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
        public void Update_Should_Succsessfully_Update_Category()
        {
            //Arrange
            BusinessLogic.Services.Contracts.Models.CategoryUpdateDto emptyNameCategory = new BusinessLogic.Services.Contracts.Models.CategoryUpdateDto();

            //Act
            var result = _categoryService.Update (emptyNameCategory);

            //Assert
            Assert.True (result.Result.Success);
        }

        [Fact]
        public void Update_Should_Return_Error_If_Repository_Throws_Exception()
        {
            //Arrange
            BusinessLogic.Services.Contracts.Models.CategoryUpdateDto emptyNameCategory = new BusinessLogic.Services.Contracts.Models.CategoryUpdateDto();
            categoryRepositoryMock.Setup(_ => _.Update(It.IsAny<Category>())).Throws<Exception>();

            //Act
            var result = _categoryService.Update(emptyNameCategory);

            //Assert
            Assert.False(result.Result.Success);
        }

        [Fact]
        public void Update_Should_Return_Error_If_Argument_Is_Null()
        {
            //Arrange
            BusinessLogic.Services.Contracts.Models.CategoryUpdateDto emptyNameCategory = null;

            //Act
            var result = _categoryService.Update(emptyNameCategory);

            //Assert
            Assert.False(result.Result.Success );
        }

    }
}
