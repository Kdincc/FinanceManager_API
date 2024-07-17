using FluentValidation;

namespace Task11.Application.ExpenseFinanceOperations.Commands.Delete
{
    public sealed class DeleteExpenceFinanceOperationCommandValidator : AbstractValidator<DeleteExpenceFinanseOperationCommand>
    {
        public DeleteExpenceFinanceOperationCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
