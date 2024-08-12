using MediatR;

namespace Task11.Application.IncomeFinanceOperations.Queries.GetIncomeFinanceOperations
{
    public record GetIncomeFinanceOperationsQuery() : IRequest<IEnumerable<IncomeFinanceOperationResult>>;
}
