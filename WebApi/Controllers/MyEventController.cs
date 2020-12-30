using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Services.Abstractions;
using BusinessLogic.Services.Contracts;
using BusinessLogic.Services.Contracts.Models;
using DataAccess.Entities;
using DataAccess.Repositories.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Models.MyEvents;
using System.ComponentModel.DataAnnotations;
using WebApi.Models.Comments;

namespace WebApi.Controllers
{
    /// <summary>
    /// Контроллер объявлений
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MyEventController : BaseController
    {
        readonly IMyEventService _myeventService;
        readonly IMapper _mapper;

        public MyEventController(IMyEventService myeventService, IMapper mapper) : base(mapper)
        {
            _myeventService = myeventService;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить объявление
        /// </summary>
        /// <param name="id">идентификатор</param>
        /// <returns>IActionResult</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([Range(1, Int32.MaxValue)]int id)
        {
            return await ProcessOperationResult(async () => await _myeventService.GetById(id));
        }

        /// <summary>
        /// Создать объявление
        /// </summary>
        /// <param name="model">модель</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        public async Task<IActionResult> Create([Required] MyEventCreateModel model)
        {
            return ProcessOperationResult(await _myeventService.Create(Mapper.Map<MyEventDto>(model)));
        }

        /// <summary>
        /// Удалить объявление 
        /// </summary>
        /// <param name="id">идентификатор</param>
        /// <returns>IActionResult</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([Range(1, Int32.MaxValue)]int id)
        {
            return ProcessOperationResult(await _myeventService.Delete(id));
        }

        /// <summary>
        /// Обновить объявление
        /// </summary>
        /// <param name="model">модель</param>
        /// <returns>IActionResult</returns>
        [HttpPut]
        public async Task<IActionResult> Update([Required] MyEventUpdateModel model)
        {
            return ProcessOperationResult(await _myeventService.Update(Mapper.Map<MyEventDto>(model)));
        }

        /// <summary>
        /// Получить постраничный список
        /// </summary>
        /// <param name="page">номер страницы</param>
        /// <param name="pageSize">объем страницы</param>
        /// <param name="categoryId">идентификатор страницы</param>
        /// <returns>IActionResult</returns>
        [HttpGet("list")]
        public async Task<IActionResult> GatPaged(
            [FromQuery, Required, Range(1, Int32.MaxValue)]int page,
            [FromQuery, Required, Range(1, Int32.MaxValue)]int pageSize,
            [FromQuery, Range(1, Int32.MaxValue)]int? categoryId)
        {
            return ProcessOperationResult(await _myeventService.GetPaged(categoryId, page, pageSize));
        }

        /// <summary>
        /// Добавать комментарий
        /// </summary>
        /// <param name="myeventId">идентификатор объявления</param>
        /// <param name="model">модель комментария</param>
        /// <returns>IActionResult</returns>
        [HttpPost("{myeventId}/comments")]
        public async Task<IActionResult> AddComment(
            int myeventId,
            [Required] AddCommentModel model)
        {
            return ProcessOperationResult(await _myeventService.AddComment(myeventId, Mapper.Map<CommentDto>(model)));
        }

        /// <summary>
        /// Получить полный список тагов всех объявлений
        /// </summary>
        [HttpGet("GetAllTags")]
        public async Task<IActionResult> GetAllTags()
        {
            return await ProcessOperationResult(async () => await _myeventService.GetAllTags());
        }

        /// <summary>
        /// Получить постраничный список по таг
        /// </summary>
        [HttpGet("taglist")]
        public async Task<IActionResult> GetTagPaged(
            [FromQuery, Required, Range(1, Int32.MaxValue)]int page,
            [FromQuery, Required, Range(1, Int32.MaxValue)]int pageSize,
            [FromQuery, Range(1, Int32.MaxValue)]int? TagId)
        {
            return ProcessOperationResult(await _myeventService.GetTagPaged(TagId, page, pageSize));
        }
    }
}