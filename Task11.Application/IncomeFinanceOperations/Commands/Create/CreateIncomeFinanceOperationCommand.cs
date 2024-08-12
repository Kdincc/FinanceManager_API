using ErrorOr;
using MediatR;

namespace Task11.Application.IncomeFinanceOperations.Commands.Create
{
    public record CreateIncomeFinanceOperationCommand(string Date, string IncomeTypeId, decimal Amount, string Name) : IRequest<ErrorOr<IncomeFinanceOperationResult>>;
}
