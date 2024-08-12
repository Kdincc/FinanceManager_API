using FluentValidation;
using Task11.Application.Properties;

namespace Task11.Application.IncomeTypes.Commands.Delete
{
    public sealed class DeleteIncomeTypeCommandValidator : AbstractValidator<DeleteIncomeTypeCommand>
    {
        public DeleteIncomeTypeCommandValidator()
        {
            RuleFor(i => i.Id)
                .NotEmpty()
                .Must(x => Guid.TryParse(x, out _))
                .WithMessage(ValidationErrorMessages.IncorrectIdFormatError);
        }
    }
}
