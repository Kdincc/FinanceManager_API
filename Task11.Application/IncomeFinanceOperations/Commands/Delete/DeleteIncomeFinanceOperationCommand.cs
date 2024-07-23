using ErrorOr;
using MediatR;

namespace Task11.Application.IncomeFinanceOperations.Commands.Delete
{
    public record DeleteIncomeFinanceOperationCommand(string IncomeFinanceOperationId) : IRequest<ErrorOr<IncomeFinanceOperationResult>>;
}
