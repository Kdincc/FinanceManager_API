using ErrorOr;
using MediatR;

namespace Task11.Application.ExpenseFinanceOperations.Commands.Create
{
    public record CreateExpenseFinanaceOperationCommand(
        string Date,
        string ExpenseTypeId,
        decimal Amount,
        string Name) : IRequest<ErrorOr<ExpenseFinanceOperationResult>>;
}
