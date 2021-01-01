using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class MyEvent
    {
        public int Id { get; set; }
        
        [MaxLength(256)]
        public string Title { get; set; }

        [MaxLength(2048)]
        public string Body { get; set; }

        public DateTime MyDateTime { get; set; }

        public string MyDateTimeStr { get; set; }

        [MaxLength(32)]
        public string email { get; set; }

        public bool Deleted { get; set; }

        public int? CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<MyEventTag> MyEventTags { get; set; }

    }
}
