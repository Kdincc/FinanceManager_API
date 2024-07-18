using ErrorOr;
using MediatR;
using Task11.Domain.ExpenseFinanceOperationAggregate.ValueObjects;

namespace Task11.Application.ExpenseFinanceOperations.Commands.Delete
{
    public record DeleteExpenceFinanseOperationCommand(ExpenseFinanceOperationId Id) : IRequest<ErrorOr<ExpenseFinanceOperationResult>>;
}
