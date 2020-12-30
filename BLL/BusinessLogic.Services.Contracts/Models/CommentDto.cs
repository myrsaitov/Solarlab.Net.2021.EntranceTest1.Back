using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Services.Contracts.Models
{
    public class CommentDto
    {
        /// <summary>
        /// Идентификатор комментария
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Текст комментария
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Дата и время комментария (UTC)
        /// </summary>
        public DateTime CommentDate { get; set; }
    }
}
