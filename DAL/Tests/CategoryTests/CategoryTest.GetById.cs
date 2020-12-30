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
        public void GetById_Should_Return_Error_If__Argument_Is_Zero()
        {
            //Arrange
            

            //Act
            var result = _categoryService.GetById (0);

            //Assert
            Assert.False (result.Result.Success);
        }

    }
}
