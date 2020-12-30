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
        public  async void Delete_Successfully_Deletes()
        {
            //Arrange
            var id = 1;
           
            //Act
            var result =await _categoryService.Delete(id);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
        }

    }
}
