using BusinessLogic.Services.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using BusinessLogic.Services.Contracts.Models;
using WebApi.Models.Categories;
using Microsoft.AspNetCore.Authorization;
using WebApi.Models;

namespace WebApi.Controllers
{
    //Все методы будут доступны только администратору
    //[Authorize(Roles = "Administrator")] 

    [Authorize(Roles = "User")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(
            ICategoryService categoryService,
            IMapper mapper) : base(mapper)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Получить категорию
        /// </summary>
        /// <param name="id">идентификатор</param>
        /// <returns>IActionResult</returns>
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([Range(1, Int32.MaxValue)]int id)
        {
            return ProcessOperationResult(await _categoryService.GetById(id));
        }

        /// <summary>
        /// Создать категорию
        /// </summary>
        /// <param name="model">модель</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        public async Task<IActionResult> Create([Required] CategoryCreateModel model)
        {
            return ProcessOperationResult(await _categoryService.Create(Mapper.Map<CategoryCreateDto>(model)));
        }

        /// <summary>
        /// Удалить категорию 
        /// </summary>
        /// <param name="id">идентификатор</param>
        /// <returns>IActionResult</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([Range(1, Int32.MaxValue)]int id)
        {
            return ProcessOperationResult(await _categoryService.Delete(id));
        }

        /// <summary>
        /// Обновить категорию
        /// </summary>
        /// <param name="model">модель</param>
        /// <returns>IActionResult</returns>
        [HttpPut]
        public async Task<IActionResult> Update([Required] CategoryUpdateModel model)
        {
            return ProcessOperationResult(await _categoryService.Update(Mapper.Map<CategoryUpdateDto>(model)));
        }

        /// <summary>
        /// Получить постраничный список
        /// </summary>
        /// <param name="page">номер страницы</param>
        /// <param name="pageSize">объем страницы</param>
        /// <returns>IActionResult</returns>
        [AllowAnonymous]
        [HttpGet("list")]
        public async Task<IActionResult> GatPaged(
            [FromQuery, Required, Range(1, Int32.MaxValue)]int page,
            [FromQuery, Required, Range(1, Int32.MaxValue)]int pageSize)
        {
            return ProcessOperationResult(await _categoryService.GetPaged(page, pageSize));
        }
    }
}