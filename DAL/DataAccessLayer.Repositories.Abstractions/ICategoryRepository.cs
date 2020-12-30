using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstractions
{
    /// <summary>
    /// Интерфейс репозитория категорий
    /// </summary>
    public interface ICategoryRepository
    {
        /// <summary>
        /// Получить все категории
        /// </summary>
        /// <returns></returns>
        Task<ICollection<Category>> GetAll();

        /// <summary>
        /// Получить категорию по идентификатору
        /// </summary>
        /// <param name="categoryId">Идентификатор сущности</param>
        /// <returns></returns>
        Task<Category> GetById(int categoryId);

        /// <summary>
        /// Полчить идентификаторы всех потомков категории
        /// </summary>
        /// <param name="categoryId">Идентификатор родительской сущности</param>
        /// <returns></returns>
        Task<ICollection<int>> GetAllChildIds(int categoryId);

        /// <summary>
        /// Добавить категорию
        /// </summary>
        /// <param name="category">Сущность для добавления</param>
        /// <returns></returns>
        Task Add(Category category);

        /// <summary>
        /// Обновить категорию
        /// </summary>
        /// <param name="category">Сущность для обновления</param>
        /// <returns></returns>
        Task Update(Category category);

        /// <summary>
        /// Удалить категорию
        /// </summary>
        /// <param name="categoryId">Идентификатор категории для удаления</param>
        /// <returns></returns>
        Task Delete(int categoryId);

        /// <summary>
        /// Получить категории постранично
        /// </summary>
        /// <param name="page">Номер страницы</param>
        /// <param name="pageSize">Количество записей на странице</param>
        /// <returns></returns>
        Task<ICollection<Category>> GetPaged(int page, int pageSize);
    }
}
