using FluentValidation;
using Task11.Domain.Common.Сonstants;

namespace Task11.Application.IncomeTypes.Commands.Create
{
    public sealed class CreateIncomeTypeCommandValidator : AbstractValidator<CreateIncomeTypeCommand>
    {
        public CreateIncomeTypeCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty()
                .MaximumLength(ValidationConstants.OperationType.MaxNameLength);

            RuleFor(p => p.Description).NotEmpty()
                .MaximumLength(ValidationConstants.OperationType.MaxDescriptionLength);

            RuleFor(p => p.Amount)
                .NotEmpty()
                .Must(p => p >= 0);
        }
    }
}
