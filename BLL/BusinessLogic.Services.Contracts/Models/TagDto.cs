using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Services.Contracts.Models
{
    /// <summary>
    /// Тег. Транспортный объект.
    /// </summary>
    public class TagDto
    {
        public int Id { get; set; }
        public string TagText { get; set; }
    }
}
