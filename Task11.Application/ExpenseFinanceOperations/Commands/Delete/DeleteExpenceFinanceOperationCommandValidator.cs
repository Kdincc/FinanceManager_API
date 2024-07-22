using FluentValidation;
using Task11.Application.Properties;

namespace Task11.Application.ExpenseFinanceOperations.Commands.Delete
{
    public sealed class DeleteExpenceFinanceOperationCommandValidator : AbstractValidator<DeleteExpenceFinanseOperationCommand>
    {
        public DeleteExpenceFinanceOperationCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .Must(x => Guid.TryParse(x, out _))
                .WithMessage(ValidationErrorMessages.IncorrectIdFormatError);
        }
    }
}
