using ErrorOr;
using MediatR;
using Task11.Domain.Common.ValueObjects;
using Task11.Domain.ExpenseFinanceOperationAggregate.ValueObjects;
using Task11.Domain.ExpenseType.ValueObjects;

namespace Task11.Application.ExpenseFinanceOperations.Commands.Update
{
    public record UpdateExpenceFinanceOperationCommand(
        string ExpenseFinanceOperationId,
        string Date,
        string ExpenceTypeId,
        decimal Amount,
        string Name) : IRequest<ErrorOr<ExpenseFinanceOperationResult>>;
}
