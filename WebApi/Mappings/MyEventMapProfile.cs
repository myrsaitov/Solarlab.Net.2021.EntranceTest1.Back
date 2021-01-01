using AutoMapper;
using BusinessLogic.Services.Contracts.Models;
using WebApi.Models.MyEvents;
using WebApi.Models.Tags;
using WebApi.Models.Comments;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Mappings
{
    public class MyEventMapProfile : Profile
    {
        public MyEventMapProfile()
        {
            CreateMap<MyEventCreateModel, MyEventDto>()
                .ForMember(s => s.Id, map => map.Ignore())
                .ForMember(s => s.Comments, map => map.Ignore())
                .ForMember(s => s.Category, map => map.Ignore())
                .ForMember(s => s.MyDateTime, map => map.Ignore());
            // .ForMember(s => s.CategoryId, map => map.MapFrom(m => m.CategoryId));
            CreateMap<MyEventUpdateModel, MyEventDto>()
                .ForMember(s => s.Comments, map => map.Ignore())
                .ForMember(s => s.Category, map => map.Ignore())
                .ForMember(s => s.CategoryId, map => map.MapFrom(m => m.CategoryId))
                .ForMember(s => s.MyDateTime, map => map.Ignore());
        }
    }
}
