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
 
    public class MyEventRepository : IMyEventRepository
    {
        #region Private fields

        private readonly Context _dbContext;
        DbSet<MyEvent> _dbSet;
        #endregion

        #region Ctor

        public MyEventRepository(Context dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<MyEvent>();
        }


        #endregion

        #region IMyEventRepository implementation
        public void Create(MyEvent item)
        {
            _dbSet.Add(item);
            _dbContext.SaveChanges();
        }




        /// <summary>
        /// Получить все объявления
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<MyEvent>> GetAll()
        {
            return await _dbContext.MyEvents.AsNoTracking().ToArrayAsync();
        }

        /// <summary>
        /// Получить объявления постранично
        /// </summary>
        /// <param name="page">Номер страницы</param>
        /// <param name="pageSize">Количество записей на странице</param>
        /// <returns></returns>
        public async Task<ICollection<MyEvent>> GetPaged(int page, int pageSize)
        {
            var skip = (page - 1) * pageSize;
            return await _dbContext.MyEvents
                .Include(x => x.Comments)
                .Include(x => x.Category)
                //.Include(x => x.Category.ChildCategories)
               // .Include(x => x.Category.ParentCategory)
                .Include(x => x.MyEventTags)
                .AsNoTracking()
                .Skip(skip)
                .Take(pageSize)
                .ToArrayAsync();
        }
        /// <summary>
        /// Получить объявления попадающие в категрии постранично
        /// </summary>
        /// <param name="categoriesId">Набор идентификаторов категорий</param>
        /// <param name="page">Номер страницы</param>
        /// <param name="pageSize">Количество записей на странице</param>
        /// <returns></returns>
        public async Task<ICollection<MyEvent>> GetPaged(int[] categoriesId, int page, int pageSize)
        {
            var skip = (page - 1) * pageSize;
            return await _dbContext.MyEvents
                .AsNoTracking()
                .Where(x => categoriesId.Contains(x.Category.Id))
                //.Where(x => categoriesId.Contains(x.MyDateTime))
                .Skip(skip)
                .Take(pageSize).ToArrayAsync();

        }


        /// <summary>
        /// Получить объявление по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<MyEvent> GetById(int id)
        {
            return await _dbContext.MyEvents.FindAsync(id);
        }

        /// <summary>
        /// Добавить объявление
        /// </summary>
        /// <param name="myevent">Сущность для добавления</param>
        public async Task<int> Add(MyEvent myevent)
        {
            await _dbContext.MyEvents.AddAsync(myevent);
            await _dbContext.SaveChangesAsync();
            return myevent.Id;
        }

        /// <summary>
        /// Обновить объявление
        /// </summary>
        /// <param name="myevent">Сущность для обновления</param>
        /*public Task Update(MyEvent myevent)
        {
            _dbContext.MyEvents.Update(myevent);
            return Task.CompletedTask;
        }*/

        public async Task Update(MyEvent item)
        {
            _dbContext.Entry(item).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }



        /// <summary>
        /// Удалить объявление
        /// </summary>
        /// <param name="id">Идентификатор сущности для удаления</param>
        public async Task Delete(int id)
        {
            var entity = await _dbContext.MyEvents.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _dbContext.MyEvents.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }


        #endregion
    }
}
