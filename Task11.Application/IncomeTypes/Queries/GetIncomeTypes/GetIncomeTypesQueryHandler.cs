using MediatR;
using Task11.Application.Common.Persistance;
using Task11.Domain.IncomeFinanceOperation.ValueObjects;
using Task11.Domain.IncomeType;

namespace Task11.Application.IncomeTypes.Queries.GetIncomeTypes
{
    public sealed class GetIncomeTypesQueryHandler(IRepository<IncomeType, IncomeTypeId> repository) : IRequestHandler<GetIncomeTypesQuery, IEnumerable<IncomeType>>
    {
        private readonly IRepository<IncomeType, IncomeTypeId> _repository = repository;

        public async Task<IEnumerable<IncomeType>> Handle(GetIncomeTypesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync(cancellationToken);
        }
    }
}
