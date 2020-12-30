using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.Entities
{
    /// <summary>
    /// Тег
    /// </summary>
    public class Tag
    {
        public int Id { get; set; }
        
        [MaxLength(32)]
        public string TagText { get; set; }

        public virtual ICollection<MyEventTag> MyEvents { get; set; }
    }
}