using MediatR;

namespace Task11.Application.ExpenseTypes.Queries.GetExpenseTypes
{
    public record GetExpenseTypesQuery() : IRequest<IEnumerable<ExpenseTypesResult>>;
}
