using AutoMapper;
using BusinessLogic.Services.Abstractions;
using BusinessLogic.Services.Contracts;
using BusinessLogic.Services.Contracts.Models;
using DataAccess.Entities;
using DataAccess.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    /// <summary>
    /// Реализация сервиса работы с категориями
    /// </summary>
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        //private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(
            IMapper mapper,
            //IUnitOfWork unitOfWork,
            ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            //_unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }

        /// <inheritdoc />
        public async Task<OperationResult<ICollection<CategoryDto>>> GetPaged(int page, int pageSize)
        {
            var entities = await _categoryRepository.GetPaged(page, pageSize);

            return OperationResult<ICollection<CategoryDto>>.Ok(_mapper.Map<ICollection<CategoryDto>>(entities));
        }

        /// <inheritdoc />
        public async Task<OperationResult<CategoryDto>> GetById(int id)
        {
            var entity = await _categoryRepository.GetById(id);
            return OperationResult<CategoryDto>.Ok(_mapper.Map<CategoryDto>(entity));
        }

        /// <inheritdoc />
        public async Task<OperationResult<bool>> Create(CategoryCreateDto categoryDto)
        {
            try
            {
                Category parentCategory = null;
                if (categoryDto.ParentCategoryId.HasValue)
                {
                    parentCategory = await _categoryRepository.GetById(categoryDto.ParentCategoryId.Value);
                    if (parentCategory == null)
                    {
                        return OperationResult<bool>.Failed(new[] { $"Родительская категория с идентификатором {categoryDto.ParentCategoryId.Value} не существует" });
                    }
                }
                Category entity = _mapper.Map<Category>(categoryDto);
                entity.ParentCategory = parentCategory;
                await _categoryRepository.Add(entity);
                //await _unitOfWork.SaveChanges();
            }
            catch (Exception e)
            {
                return OperationResult<bool>.Failed(new[] { e.Message });
            }
            return OperationResult<bool>.Ok(true);
        }

        /// <inheritdoc />
        public async Task<OperationResult<bool>> Update(CategoryUpdateDto categoryDto)
        {
            try
            {
                var category = await _categoryRepository.GetById(categoryDto.Id);
                if (category == null)
                {
                    return OperationResult<bool>.Failed(new[] { $"Категория с идентификатором {categoryDto.Id} не существует" });
                }
                Category parentCategory = null;
                if (categoryDto.ParentCategoryId.HasValue)
                {
                    parentCategory = await _categoryRepository.GetById(categoryDto.ParentCategoryId.Value);
                    if (parentCategory == null)
                    {
                        return OperationResult<bool>.Failed(new[] { $"Родительская категория с идентификатором {categoryDto.ParentCategoryId.Value} не существует" });
                    }
                }
                _mapper.Map(categoryDto, category);
                category.ParentCategory = parentCategory;
                await _categoryRepository.Update(category);
                //await _unitOfWork.SaveChanges();
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
                await _categoryRepository.Delete(id);
                //await _unitOfWork.SaveChanges();
            }
            catch (Exception e)
            {
                return OperationResult<bool>.Failed(new[] { e.Message });
            }
            return OperationResult<bool>.Ok(true);
        }
    }
}
