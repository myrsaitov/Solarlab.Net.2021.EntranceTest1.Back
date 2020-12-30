using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.Comments
{
    public class AddCommentModel
    {
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
