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

namespace WebApi.Mapping
{
    public class TagMapProfile : Profile
    {
        public TagMapProfile()
        {
            CreateMap<TagModel, TagDto>();
        }
    }
}