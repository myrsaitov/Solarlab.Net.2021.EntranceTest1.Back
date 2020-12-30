using AutoMapper;
using BusinessLogic.Services.Contracts.Models;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Services.Mapping
{
    /// <summary>
    /// Профиль автомаппера.
    /// </summary>
    public class ServiceMappings : Profile
    {
        public ServiceMappings()
        {
            CreateMap<Tag, TagDto>();

            CreateMap<TagDto, Tag>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.MyEvents, opt => opt.Ignore());
                

            CreateMap<TagDto,MyEventTag>()
                //.ForMember(d => d.TagId, opt => opt.MapFrom(m => m.Id))
                //.ForPath(d => d.Tag.TagText, opt => opt.MapFrom(m => m.TagText))
                .ForMember(d => d.TagId, opt => opt.Ignore())
                .ForMember(d => d.MyEventId, opt => opt.Ignore())
                .ForMember(d => d.Tag, opt => opt.Ignore())
                .ForMember(d => d.MyEvent, opt => opt.Ignore());

            //.ForPath нужно для вложенностей d.Tag.TagText
            //https://stackoverflow.com/questions/46034426/how-to-map-nested-child-object-properties-in-automapper





            CreateMap<MyEventTag, TagDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(m => m.TagId))
                .ForMember(d => d.TagText, opt => opt.MapFrom(m => m.Tag.TagText));
            CreateMap<Category, CategoryDto>();
            CreateMap<Comment, CommentDto>();
            CreateMap<MyEvent, MyEventDto>()
                .ForMember(d => d.Tags, opt => opt.Ignore());
                //.ForMember(d => d.CategoryId, opt => opt.MapFrom(s => s.Category != null ? s.Category.Id : (int?)null));
            CreateMap<CommentDto, Comment>()
                .ForMember(d => d.CommentDate, opt => opt.Ignore());
            CreateMap<MyEventDto, MyEvent>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.MyEventTags, opt => opt.Ignore())
                .ForMember(d => d.Comments, opt => opt.Ignore())
                .ForMember(d => d.Category, opt => opt.Ignore());
            CreateMap<CategoryDto, Category>();
            CreateMap<CategoryCreateDto, Category>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.Childs, opt => opt.Ignore())
                .ForMember(d => d.ParentCategory, opt => opt.Ignore());
            CreateMap<CategoryUpdateDto, Category>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.Childs, opt => opt.Ignore())
                .ForMember(d => d.ParentCategory, opt => opt.Ignore());




        }
    }
}
