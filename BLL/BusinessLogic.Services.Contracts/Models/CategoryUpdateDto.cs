using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Services.Contracts.Models
{
    /// <summary>
    /// ДТО обновления категории
    /// </summary>
    public class CategoryUpdateDto : CategoryCreateDto
    {
        public int Id { get; set; }
    }
}
