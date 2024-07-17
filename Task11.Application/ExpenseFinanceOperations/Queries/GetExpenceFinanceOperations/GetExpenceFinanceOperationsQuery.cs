using MediatR;

namespace Task11.Application.ExpenseFinanceOperations.Queries.GetExpenceFinanceOperations
{
    public record GetExpenceFinanceOperationsQuery() : IRequest<IEnumerable<ExpenseFinanceOperationResult>>;
}
