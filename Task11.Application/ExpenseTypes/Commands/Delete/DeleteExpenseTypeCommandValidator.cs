using FluentValidation;

namespace Task11.Application.ExpenseTypes.Commands.Delete
{
    public sealed class DeleteExpenseTypeCommandValidator : AbstractValidator<DeleteExpenseTypeCommand>
    {
        public DeleteExpenseTypeCommandValidator()
        {
            RuleFor(p => p.ExpenseTypeId)
                .NotEmpty()
                .Must(x => Guid.TryParse(x, out _));
        }
    }
}
