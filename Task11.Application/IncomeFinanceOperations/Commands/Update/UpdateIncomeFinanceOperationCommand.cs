using ErrorOr;
using MediatR;

namespace Task11.Application.IncomeFinanceOperations.Commands.Update
{
    public record UpdateIncomeFinanceOperationCommand(
        string IncomeFinanceOperationId,
        string IncomeTypeId,
        string Date,
        decimal Amount,
        string Name) : IRequest<ErrorOr<IncomeFinanceOperationResult>>;
}
