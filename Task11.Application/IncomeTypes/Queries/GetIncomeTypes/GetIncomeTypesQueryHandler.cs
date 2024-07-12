using MediatR;
using Task11.Application.Common.Persistance;
using Task11.Domain.IncomeFinanceOperation.ValueObjects;
using Task11.Domain.IncomeType;

namespace Task11.Application.IncomeTypes.Queries.GetIncomeTypes
{
    public sealed class GetIncomeTypesQueryHandler(IRepository<IncomeType, IncomeTypeId> repository) : IRequestHandler<GetIncomeTypesQuery, IEnumerable<IncomeTypesResult>>
    {
        private readonly IRepository<IncomeType, IncomeTypeId> _repository = repository;

        public async Task<IEnumerable<IncomeTypesResult>> Handle(GetIncomeTypesQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<IncomeType> incomeTypes = await _repository.GetAllAsync(cancellationToken);

            return incomeTypes.Select(incomeType => new IncomeTypesResult(incomeType));
        }
    }
}
