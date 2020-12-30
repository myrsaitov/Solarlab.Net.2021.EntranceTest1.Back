using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstractions
{
    /// <summary>
    /// Интерфейс репозитория объявлений
    /// </summary>
    public interface IMyEventRepository
    {
        /// <summary>
        /// Получить все объявления
        /// </summary>
        /// <returns></returns>
        Task<ICollection<MyEvent>> GetAll();

        /// <summary>
        /// Получить объявления постранично
        /// </summary>
        /// <param name="page">Номер страницы</param>
        /// <param name="pageSize">Количество записей на странице</param>
        /// <returns></returns>
        Task<ICollection<MyEvent>> GetPaged(int page, int pageSize);

        /// <summary>
        /// Получить объявления попадающие в категории постранично
        /// </summary>
        /// <param name="categoriesId">Набор идентификаторов категорий</param>
        /// <param name="page">Номер страницы</param>
        /// <param name="pageSize">Количество записей на странице</param>
        /// <returns></returns>
        Task<ICollection<MyEvent>> GetPaged(int[] categoriesId, int page, int pageSize);

        /// <summary>
        /// Получить объявление по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<MyEvent> GetById(int id);

        /// <summary>
        /// Добавить объявление
        /// </summary>
        /// <param name="myevent">Сущность для добавления</param>
        Task<int> Add(MyEvent myevent);

        /// <summary>
        /// Обновить объявление
        /// </summary>
        /// <param name="myevent">Сущность для обновления</param>
        Task Update(MyEvent myevent);

        /// <summary>
        /// Удалить объявление
        /// </summary>
        /// <param name="id">Идентификатор сущности для удаления</param>
        Task Delete(int id);


    }
}