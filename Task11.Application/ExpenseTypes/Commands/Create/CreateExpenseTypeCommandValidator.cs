using FluentValidation;
using Task11.Domain.Common.Сonstants;

namespace Task11.Application.ExpenseTypes.Commands.Create
{
    public sealed class CreateExpenseTypeCommandValidator : AbstractValidator<CreateExpenseTypeCommand>
    {
        public CreateExpenseTypeCommandValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty()
                .MaximumLength(ValidationConstants.OperationType.MaxNameLength);
            RuleFor(e => e.Description)
                .NotEmpty()
                .MaximumLength(ValidationConstants.OperationType.MaxDescriptionLength);
        }
    }
}
