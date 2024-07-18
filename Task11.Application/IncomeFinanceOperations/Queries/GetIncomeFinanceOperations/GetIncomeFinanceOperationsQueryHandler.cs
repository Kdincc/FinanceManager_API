using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Application.Common.Persistance;
using Task11.Domain.IncomeFinanceOperation;
using Task11.Domain.IncomeFinanceOperation.ValueObjects;

namespace Task11.Application.IncomeFinanceOperations.Queries.GetIncomeFinanceOperations
{
    public sealed class GetIncomeFinanceOperationsQueryHandler(IRepository<IncomeFinanceOperation, IncomeFinanceOperationId> repository) : IRequestHandler<GetIncomeFinanceOperationsQuery, IEnumerable<IncomeFinanceOperationResult>>
    {
        private readonly IRepository<IncomeFinanceOperation, IncomeFinanceOperationId> _repository = repository;

        public async Task<IEnumerable<IncomeFinanceOperationResult>> Handle(GetIncomeFinanceOperationsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<IncomeFinanceOperation> financeOperations = await _repository.GetAllAsync(cancellationToken);

            return financeOperations.Select(financeOperation => new IncomeFinanceOperationResult(financeOperation));
        }
    }
}
