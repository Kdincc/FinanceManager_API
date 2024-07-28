using MediatR;
using Task11.Application.Common.Persistance;
using Task11.Domain.IncomeFinanceOperationAggregate;

namespace Task11.Application.IncomeFinanceOperations.Queries.GetIncomeFinanceOperations
{
    public sealed class GetIncomeFinanceOperationsQueryHandler(IIncomeFinanceOperationRepository repository) : IRequestHandler<GetIncomeFinanceOperationsQuery, IEnumerable<IncomeFinanceOperationResult>>
    {
        private readonly IIncomeFinanceOperationRepository _repository = repository;

        public async Task<IEnumerable<IncomeFinanceOperationResult>> Handle(GetIncomeFinanceOperationsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<IncomeFinanceOperation> financeOperations = await _repository.GetAllAsync(cancellationToken);

            return financeOperations.Select(financeOperation => new IncomeFinanceOperationResult(financeOperation));
        }
    }
}
