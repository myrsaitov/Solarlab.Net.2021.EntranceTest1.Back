using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.MyEvents
{
    public class GetPagedMyEventModel
    {
        public int? CategoryId { get; set; }

        [Range(1, Int32.MaxValue)]
        public int Page { get; set; }

        [Range(1, Int32.MaxValue)]
        public int PageSize { get; set; }
    }
}