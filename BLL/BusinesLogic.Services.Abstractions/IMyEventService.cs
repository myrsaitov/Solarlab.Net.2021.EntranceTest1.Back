using BusinessLogic.Services.Contracts;
using BusinessLogic.Services.Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Abstractions
{

    /// <summary>
    /// Интерфейс сервиса работы с объявлениями
    /// </summary>
    public interface IMyEventService
    {
        /// <summary>
        /// Получение списка всех объявлений
        /// </summary>
        /// <returns>OperationResult</returns>
        Task<OperationResult<ICollection<MyEventDto>>> GetPaged(int? categoryId, int page, int pageSize);

        /// <summary>
        /// Получение объявления по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор существующего объявления</param>
        /// <returns>OperationResult</returns>
        Task<OperationResult<MyEventDto>> GetById(int id);

        Task<OperationResult<MyEventDto>> GetAllTags();

        /// <summary>
        /// Создание объявления
        /// </summary>
        /// <param name="myeventDto">Объявление. Транспортный объект.</param>
        /// <returns>OperationResult</returns>
        Task<OperationResult<bool>> Create(MyEventDto myeventDto);

        /// <summary>
        /// Изменение объявления
        /// </summary>
        /// <param name="myeventDto">Объявление. Транспортный объект.</param>
        /// <returns>OperationResult</returns>
        Task<OperationResult<bool>> Update(MyEventDto myeventDto);

        /// <summary>
        /// Удаление объявления
        /// </summary>
        /// <param name="id">Идентификатор существующего объявления</param>
        /// <returns>OperationResult</returns>
        Task<OperationResult<bool>> Delete(int id);

        /// <summary>
        /// Добавить комментарий к объявлению
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commentDto"></param>
        /// <returns>OperationResult</returns>
        Task<OperationResult<bool>> AddComment(int id, CommentDto commentDto);


        Task<OperationResult<ICollection<MyEventDto>>> GetTagPaged(int? TagId, int page, int pageSize);
    }
}
