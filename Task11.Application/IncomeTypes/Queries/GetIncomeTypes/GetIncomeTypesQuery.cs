using MediatR;

namespace Task11.Application.IncomeTypes.Queries.GetIncomeTypes
{
    public record GetIncomeTypesQuery() : IRequest<IEnumerable<IncomeTypesResult>>;
}
