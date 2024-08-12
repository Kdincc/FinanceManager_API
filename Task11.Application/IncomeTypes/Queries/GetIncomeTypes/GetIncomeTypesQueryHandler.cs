using MediatR;
using Task11.Application.Common.Persistance;
using Task11.Domain.IncomeType;

namespace Task11.Application.IncomeTypes.Queries.GetIncomeTypes
{
    public sealed class GetIncomeTypesQueryHandler(IIncomeTypeRepository repository) : IRequestHandler<GetIncomeTypesQuery, IEnumerable<IncomeTypesResult>>
    {
        private readonly IIncomeTypeRepository _repository = repository;

        public async Task<IEnumerable<IncomeTypesResult>> Handle(GetIncomeTypesQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<IncomeType> incomeTypes = await _repository.GetAllAsync(cancellationToken);

            return incomeTypes.Select(incomeType => new IncomeTypesResult(incomeType));
        }
    }
}
