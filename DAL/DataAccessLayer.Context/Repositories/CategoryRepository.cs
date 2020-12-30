using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        #region Private fields

        private readonly Context _dbContext;

        #endregion

        #region Ctor

        public CategoryRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        #region ICategoryRepository implementation

        /// <summary>
        /// Получить все категории
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<Category>> GetAll()
        {
            return await _dbContext.Set<Category>().AsNoTracking().ToArrayAsync();
        }

        /// <summary>
        /// Получить категорию по идентификатору
        /// </summary>
        /// <param name="categoryId">Идентификатор сущности</param>
        /// <returns></returns>
        public async Task<Category> GetById(int categoryId)
        {
            return await _dbContext.Set<Category>().FindAsync(categoryId);
        }

        /// <summary>
        /// Полчить идентификаторы всех потомков категории
        /// </summary>
        /// <param name="categoryId">Идентификатор родительской сущности</param>
        /// <returns></returns>
        public async Task<ICollection<int>> GetAllChildIds(int categoryId)
        {
            async Task<List<int>> GetCategoriesId(int categoryId)
            {
                var result = new List<int>();

                var childCategories = await _dbContext.Categories
                    .AsNoTracking()
                    .Where(x => x.ParentCategory.Id == categoryId)
                    .ToListAsync();

                foreach (var category in childCategories)
                {
                    result.Add(category.Id);
                    result.AddRange(await GetCategoriesId(category.Id));
                }

                return result;
            }

            return await GetCategoriesId(categoryId);
        }

        /// <summary>
        /// Добавить новую категорию
        /// </summary>
        /// <param name="category">Сущность для добавления</param>
        /// <returns></returns>
        public async Task Add(Category category)
        {
            await _dbContext.Categories.AddAsync(category);
        }

        /// <summary>
        /// Обновить категорию
        /// </summary>
        /// <param name="category">Сущность для обновления</param>
        /// <returns></returns>

        public Task Update(Category category)
        {
            _dbContext.Categories.Update(category);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Удалить категорию
        /// </summary>
        /// <param name="categoryId">Идентификатор категории для удаления</param>
        /// <returns></returns>
        public async Task Delete(int categoryId)
        {
            var category = await _dbContext.Categories.FindAsync(categoryId);
            if (category != null)
            {
                _dbContext.Categories.Remove(category);
            }
        }

        /// <summary>
        /// Получить категории постранично
        /// </summary>
        /// <param name="page">Номер страницы</param>
        /// <param name="pageSize">Количество записей на странице</param>
        /// <returns></returns>
        public async Task<ICollection<Category>> GetPaged(int page, int pageSize)
        {
            var skip = (page - 1) * pageSize;
            return await _dbContext.Categories
                .AsNoTracking()
                .Skip(skip)
                .Take(pageSize)
                .ToArrayAsync();
        }
        #endregion
    }
}
