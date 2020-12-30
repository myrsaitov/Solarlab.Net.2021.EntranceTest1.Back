using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic.Services.Contracts;
using BusinessLogic.Services.Contracts.Models;


namespace BusinessLogic.Services.Abstractions
{
    public interface ICategoryService
    {
        /// <summary>
        /// Получение списка всех категорий
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<OperationResult<ICollection<CategoryDto>>> GetPaged(int page, int pageSize);

        /// <summary>
        /// Получение категории по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<OperationResult<CategoryDto>> GetById(int id);

        /// <summary>
        /// Создание категории
        /// </summary>
        /// <param name="categoryDto"></param>
        /// <returns></returns>
        Task<OperationResult<bool>> Create(CategoryCreateDto categoryDto);

        /// <summary>
        /// Удаление категории
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<OperationResult<bool>> Delete(int id);

        /// <summary>
        /// Изменение категории
        /// </summary>
        /// <param name="categoryDto"></param>
        /// <returns></returns>
        Task<OperationResult<bool>> Update(CategoryUpdateDto categoryDto);


    }
}
