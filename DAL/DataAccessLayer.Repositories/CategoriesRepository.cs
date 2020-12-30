using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class CategoriesRepository : ICategoryRepository
    {
        private readonly Context.Context _context;
        DbSet<Category> _dbSet;

        public CategoriesRepository(Context.Context context)
        {
            _context = context;
            _dbSet = context.Set<Category>();
        }

        public void Create(Category item)
        {
            _dbSet.Add(item);
            _context.SaveChanges();
        }

        /// <summary>
        /// Получить категорию по id
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public async Task<Category> GetById(int categoryId)
        {
            return await _context.Set<Category>()
                .Include(c=>c.Childs).SingleOrDefaultAsync(c=>c.Id == categoryId);
        }

        /// <summary>
        /// Получить все категории
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<Category>> GetAll()
        {
            return await _context.Set<Category>().AsNoTracking().ToArrayAsync();
        }

        /// <summary>
        /// Добавить категорию
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public async Task Add(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(Category item)
        {
            _dbSet.Remove(item);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Обновить категорию
        /// </summary>
        /// <param name="item"></param>
        public async Task Update(Category item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Получить категории постранично
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<ICollection<Category>> GetPaged(int page, int pageSize)
        {
            var skip = (page - 1) * pageSize;
            return await _context.Categories
                .Include(c=>c.Childs)
                .AsNoTracking()
                .Skip(skip)
                .Take(pageSize)
                .ToArrayAsync();
        }

        /// <summary>
        /// Удалить категорию
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public async Task Delete(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category != null)
            {
                await Remove(category);

            }
        }

        public async Task<ICollection<int>> GetAllChildIds(int categoryId)
        {
            return await _context.Categories
                 .Where(c => c.ParentCategoryId == categoryId)
                 .Select(c => c.Id).ToListAsync();
        }
    }
}
