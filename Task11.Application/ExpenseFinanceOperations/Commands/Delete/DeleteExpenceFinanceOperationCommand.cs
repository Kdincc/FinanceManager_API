using ErrorOr;
using MediatR;
using Task11.Domain.ExpenseFinanceOperation.ValueObjects;

namespace Task11.Application.ExpenseFinanceOperations.Commands.Delete
{
    public record DeleteExpenceFinanceOperationCommand(ExpenseFinanceOperationId Id) : IRequest<ErrorOr<ExpenseFinanceOperationResult>>;
}
