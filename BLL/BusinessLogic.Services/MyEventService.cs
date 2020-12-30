using BusinessLogic.Services.Abstractions;
using BusinessLogic.Services.Contracts;
using BusinessLogic.Services.Contracts.Models;
using BusinessLogic.Services.Validators;
using DataAccess.Repositories.Abstractions;
using DataAccess.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using BusinessLogic.Contracts.CustomExceptions;




namespace BusinessLogic.Services
{
    /// <summary>
    /// Реализация сервиса работы с объявлениями
    /// </summary>
    public class MyEventService : IMyEventService
    {
        private readonly IMapper _mapper;
        private readonly IMyEventRepository _myeventRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IMyEventTagRepository _adverttagRepository;

        

        public MyEventService(
            IMapper mapper,
            IMyEventRepository myeventRepository,
            ICategoryRepository categoryRepository,
            ITagRepository tagRepository,
            IMyEventTagRepository adverttagRepository)
        {
            _mapper = mapper;
            _myeventRepository = myeventRepository;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
            _adverttagRepository = adverttagRepository;
        }

        /// <inheritdoc />
        public async Task<OperationResult<ICollection<MyEventDto>>> GetPaged(int? categoryId, int page, int pageSize)
        {
            ICollection<MyEvent> entities;
            if (categoryId.HasValue)
            {
                var categories = await _categoryRepository.GetAllChildIds(categoryId.Value);
                categories.Add(categoryId.Value);
                entities = await _myeventRepository.GetPaged(categories.ToArray(), page, pageSize);
            }
            else
            {
                entities = await _myeventRepository.GetPaged(page, pageSize);
            }
            return OperationResult<ICollection<MyEventDto>>.Ok(_mapper.Map<ICollection<MyEventDto>>(entities));
        }

        /// <inheritdoc />
        public async Task<OperationResult<MyEventDto>> GetById(int id)
        {
            MyEventDto entityDto;

            try
            {

                MyEvent entity = await _myeventRepository.GetById(id);

                if (entity == null)
                {
                    throw new EntityNotFoundException(id, "Объявление");
                }

                entityDto = _mapper.Map<MyEventDto>(entity);

                var entityMyEventTag = await _adverttagRepository.GetById(id);

                int TagIndex = 0;


                Tag _tagentity;
                TagDto _tagdtoentity;

                entityDto.Tags = new List<TagDto>();

                foreach (var advtag in entityMyEventTag)
                {
                    TagIndex = advtag.TagId;
                    _tagentity = await _tagRepository.GetById(TagIndex);
                    _tagdtoentity = _mapper.Map<TagDto>(_tagentity);
                    entityDto.Tags.Add(_tagdtoentity);
                }
            }
            catch (Exception e)
            {
                return OperationResult<MyEventDto>.Failed(new[] {e.Message});
            }

            return OperationResult<MyEventDto>.Ok(entityDto);
        }

  

        public async Task<OperationResult<bool>>Create(MyEventDto myeventDto)
        {
            try
            {
                if (myeventDto == null)
                {
                    throw new ArgumentNullException(nameof(myeventDto));
                }
                MyEvent entity = _mapper.Map<MyEvent>(myeventDto);
                int dvertisement_dbId = await _myeventRepository.Add(entity);

                // Если из UI пришли tag, иначе ничего не делаем
                if(myeventDto.Tags.Count()  > 0)
                {
                    var _adverttag = _mapper.Map<MyEventTag>(myeventDto.Tags.Last());
                    _adverttag.MyEventId = dvertisement_dbId;

                    //int tag_dbId;
                    foreach (var tagDto in myeventDto.Tags)
                    {
                        _adverttag.TagId = await _tagRepository.Add(_mapper.Map<Tag>(tagDto));
                        await _adverttagRepository.Add(_adverttag);
                    }
                }
            }
            catch (Exception e)
            {
                return OperationResult<bool>.Failed(new[] { e.Message });
            }
            return OperationResult<bool>.Ok(true);
        }














        /// <inheritdoc />
        public async Task<OperationResult<bool>> Update(MyEventDto myeventDto)
        {
            try
            {
                var advert = await _myeventRepository.GetById(myeventDto.Id);
                _mapper.Map<MyEventDto, MyEvent>(myeventDto, advert);

                //MyEvent entity = _mapper.Map<MyEvent>(myeventDto);


                // int dvertisement_dbId = await _myeventRepository.Add(entity);
                int dvertisement_dbId = myeventDto.Id;


                // Удаляем старые связи
                await _adverttagRepository.Delete(dvertisement_dbId);


                // Если из UI пришли tag, иначе ничего не делаем
                if (myeventDto.Tags.Count() > 0)
                {
                    var _adverttag = _mapper.Map<MyEventTag>(myeventDto.Tags.Last());
                    _adverttag.MyEventId = dvertisement_dbId;

                    //int tag_dbId;
                    foreach (var tagDto in myeventDto.Tags)
                    {
                        _adverttag.TagId = await _tagRepository.Add(_mapper.Map<Tag>(tagDto));
                        await _adverttagRepository.Add(_adverttag);
                    }
                }


                await _myeventRepository.Update(advert);
                //await _myeventRepository.Update(entity);

            }
            catch (Exception e)
            {
                return OperationResult<bool>.Failed(new[] { e.Message });
            }
            return OperationResult<bool>.Ok(true);
        }

        /// <inheritdoc />
        public async Task<OperationResult<bool>> Delete(int id)
        {
            try
            {
                // Удаляем старые связи
                await _adverttagRepository.Delete(id);

                await _myeventRepository.Delete(id);
            }
            catch (Exception e)
            {
                return OperationResult<bool>.Failed(new[] { e.Message });
            }
            return OperationResult<bool>.Ok(true);
        }

        /// <inheritdoc />
        public async Task<OperationResult<bool>> AddComment(int id, CommentDto commentDto)
        {
            CommentDtoValidator commentDtoValidator = new CommentDtoValidator();
            ValidationResult result = await commentDtoValidator.ValidateAsync(commentDto);
            if (!result.IsValid)
            {
                return OperationResult<bool>.Failed(result.Errors.Select(x => x.ErrorMessage).ToArray());
            }
            else
            {
                Comment comment = _mapper.Map<Comment>(commentDto);
                comment.CommentDate = DateTime.UtcNow;

                var advert = await _myeventRepository.GetById(id);
                advert.Comments.Add(comment);

                await _myeventRepository.Update(advert);
                return OperationResult<bool>.Ok(true);
            }
        }

        public async Task<OperationResult<MyEventDto>> GetAllTags()
        {
            MyEventDto entityDto;

            try
            {

                MyEvent entity = new MyEvent();

                entityDto = _mapper.Map<MyEventDto>(entity);



               // Tag _tagentity;
                TagDto _tagdtoentity;
                int TagCount;

                ICollection<Tag> TagsEnt;
                TagsEnt = await _tagRepository.GetAll();
                entityDto.Tags = new List<TagDto>();
                foreach (var alltag in TagsEnt)
                {
                    TagCount = await _adverttagRepository.GetTagsCountById(alltag.Id); ;
                    alltag.TagText += "("+ TagCount + ")";
                    _tagdtoentity = _mapper.Map<TagDto>(alltag);
                    entityDto.Tags.Add(_tagdtoentity);
                }
            }
            catch (Exception e)
            {
                return OperationResult<MyEventDto>.Failed(new[] { e.Message });
            }

            return OperationResult<MyEventDto>.Ok(entityDto);
        }


        public async Task<OperationResult<ICollection<MyEventDto>>> GetTagPaged(int? TagId, int page, int pageSize)
        {
            ICollection<MyEventDto> entities;

            entities = new List<MyEventDto>();

            if (TagId.HasValue)
            {
                var entityMyEventTag = await _adverttagRepository.GetAdvById(TagId); ;
               

                MyEvent entity = new MyEvent();
                MyEventDto entityDto;

               // List<MyEventDto> TagMyEventCol;

               // TagMyEventCol = new List<MyEventDto>();

                foreach (var advtag in entityMyEventTag)
                {
                    entity = await _myeventRepository.GetById(advtag.MyEventId);
                    entityDto = _mapper.Map<MyEventDto>(entity);
                    entities.Add(entityDto);
                }

            }
            else
            {
                //entities = await _myeventRepository.GetPaged(page, pageSize);
            }
            return OperationResult<ICollection<MyEventDto>>.Ok(_mapper.Map<ICollection<MyEventDto>>(entities));
        }






    }
}




 