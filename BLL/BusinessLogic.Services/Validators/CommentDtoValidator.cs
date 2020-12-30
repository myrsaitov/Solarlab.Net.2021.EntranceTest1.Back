using BusinessLogic.Services.Contracts.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Services.Validators
{    
    /// <summary>
     /// Валидатор CommentDto
     /// </summary>
    public class CommentDtoValidator : AbstractValidator<CommentDto>
    {
        public CommentDtoValidator()
        {
            RuleFor(x => x.Body).NotEmpty().WithMessage("CommentDto поле Body не заполнено");
        }
    }
}
