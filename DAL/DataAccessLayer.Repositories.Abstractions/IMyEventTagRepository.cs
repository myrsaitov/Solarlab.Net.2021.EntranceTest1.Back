using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstractions
{
    public interface IMyEventTagRepository
    {
        Task<ICollection<MyEventTag>> GetAll();
        Task<List<MyEventTag>> GetById(int tagId);
        Task Add(MyEventTag adverttag);
        Task Update(MyEventTag adverttag);
        Task Delete(int adverttagId);
        Task<int> GetTagsCountById(int tagId);
        Task<List<MyEventTag>> GetAdvById(int? tagId);
    }
}
