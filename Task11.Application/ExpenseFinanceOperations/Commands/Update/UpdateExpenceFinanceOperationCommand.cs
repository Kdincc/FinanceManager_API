using ErrorOr;
using MediatR;

namespace Task11.Application.ExpenseFinanceOperations.Commands.Update
{
    public record UpdateExpenceFinanceOperationCommand(
        string ExpenseFinanceOperationId,
        string Date,
        string ExpenceTypeId,
        decimal Amount,
        string Name) : IRequest<ErrorOr<ExpenseFinanceOperationResult>>;
}
