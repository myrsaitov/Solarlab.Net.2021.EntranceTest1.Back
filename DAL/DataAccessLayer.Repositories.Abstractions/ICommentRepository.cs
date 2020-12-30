using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstractions
{
    public interface ICommentRepository
    {


        /// <summary>
        /// Получить категорию по идентификатору
        /// </summary>
        /// <param name="commentId">Идентификатор сущности</param>
        /// <returns></returns>
        Task<Comment> GetById(int commentId);



        /// <summary>
        /// Добавить 
        /// </summary>
        /// <param name="comments">Сущность для добавления</param>
        /// <returns></returns>
        Task Add(Comment comments);

        /// <summary>
        /// Обновить 
        /// </summary>
        /// <param name="comments">Сущность для обновления</param>
        /// <returns></returns>
        Task Update(Comment comments);

        /// <summary>
        /// Удалить 
        /// </summary>
        /// <param name="commentsId">Идентификатор для удаления</param>
        /// <returns></returns>
        Task Delete(int commentsId);


    }
}