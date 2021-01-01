using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.MyEvents
{
    public class MyEventUpdateModel : MyEventModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Range(1, Int32.MaxValue)]
        public int Id { get; set; }
    }
}