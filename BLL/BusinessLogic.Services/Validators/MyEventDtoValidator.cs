using BusinessLogic.Services.Contracts.Models;
using FluentValidation;


namespace BusinessLogic.Services.Validators
{
    /// <summary>
    /// Валидатор MyEventDto
    /// </summary>
    public class MyEventDtoValidator : AbstractValidator<MyEventDto>
    {
        public MyEventDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("MyEventDto Title is required");
        }
    }
}
