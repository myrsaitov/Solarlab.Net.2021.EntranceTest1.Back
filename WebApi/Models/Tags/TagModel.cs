using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.Tags
{
    /// <summary>
    /// Тег. Транспортный объект.
    /// </summary>
    public class TagModel
    {
        public int? Id { get; set; }
        public string TagText { get; set; }
    }
}
