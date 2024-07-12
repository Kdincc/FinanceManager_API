using FluentValidation;
using Task11.Domain.Common.Сonstants;

namespace Task11.Application.IncomeTypes.Commands.Create
{
    public sealed class CreateIncomeTypeCommandValidator : AbstractValidator<CreateIncomeTypeCommand>
    {
        public CreateIncomeTypeCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty()
                .MaximumLength(ValidationConstantst.OperationType.MaxNameLength);
            RuleFor(p => p.Description).NotEmpty()
                .MaximumLength(ValidationConstantst.OperationType.MaxDescriptionLength);
        }
    }
}
