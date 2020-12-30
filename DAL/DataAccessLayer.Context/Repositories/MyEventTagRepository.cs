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

    public class MyEventTagRepository : IMyEventTagRepository
    {
        #region Private fields

        private readonly Context _dbContext;
        DbSet<MyEventTag> _dbSet;
        #endregion

        #region Ctor

        public MyEventTagRepository(Context dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<MyEventTag>();
        }


        #endregion

        #region IMyEventTagRepository implementation
        public void Create(MyEventTag item)
        {
            _dbSet.Add(item);
            _dbContext.SaveChanges();
        }




        /// <summary>
        /// Получить все объявления
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<MyEventTag>> GetAll()
        {
            return await _dbContext.MyEventTags.AsNoTracking().ToArrayAsync();
        }


        /// <summary>
        /// Получить объявление по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<MyEventTag>> GetById(int id)
        {
            //// include
            //return _dbContext.MyEventTags
            //    .Include(x => x.Comments)
            //    .Include(x => x.Category)
            //    .SingleOrDefault(x => x.Id == id);
            //without include use only with lazyloading


            return await _dbContext.MyEventTags.Where(x => x.MyEventId == id).ToListAsync();
            
            //return await _dbContext.MyEventTags.FindAsync(id);
        }

        /// <summary>
        /// Добавить объявление
        /// </summary>
        /// <param name="tag">Сущность для добавления</param>
        public async Task Add(MyEventTag tag)
        {
            await _dbContext.MyEventTags.AddAsync(tag);
            await _dbContext.SaveChangesAsync();
        }



        public async Task Update(MyEventTag item)
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
            //var entity = await _dbContext.MyEventTags.FindAsync(id);


            var entityList = await _dbContext.MyEventTags.Where(x => x.MyEventId == id).ToListAsync();




            foreach (var entity in entityList)
            {
                if (entity != null)
                {
                    _dbSet.Remove(entity);
                    _dbContext.MyEventTags.Remove(entity);
                    await _dbContext.SaveChangesAsync();
                }
            }



        }

        public async Task<int> GetTagsCountById(int id)
        {


            List<MyEventTag> TagList = new List<MyEventTag>();

            TagList = await _dbContext.MyEventTags.Where(x => x.TagId == id).ToListAsync();

            return TagList.Count;

            //return await _dbContext.MyEventTags.FindAsync(id);
        }




        public async Task<List<MyEventTag>> GetAdvById(int? id)
        {
            //// include
            //return _dbContext.MyEventTags
            //    .Include(x => x.Comments)
            //    .Include(x => x.Category)
            //    .SingleOrDefault(x => x.Id == id);
            //without include use only with lazyloading


            return await _dbContext.MyEventTags.Where(x => x.TagId == id).ToListAsync();

            //return await _dbContext.MyEventTags.FindAsync(id);
        }

        #endregion
    }
}
