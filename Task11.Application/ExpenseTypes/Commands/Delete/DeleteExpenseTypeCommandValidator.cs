using FluentValidation;
using Task11.Application.Properties;

namespace Task11.Application.ExpenseTypes.Commands.Delete
{
    public sealed class DeleteExpenseTypeCommandValidator : AbstractValidator<DeleteExpenseTypeCommand>
    {
        public DeleteExpenseTypeCommandValidator()
        {
            RuleFor(p => p.ExpenseTypeId)
                .NotEmpty()
                .Must(x => Guid.TryParse(x, out _))
                .WithMessage(ValidationErrorMessages.IncorrectIdFormatError);
        }
    }
}
