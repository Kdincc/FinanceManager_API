using MediatR;
using Task11.Application.Common.Persistance;
using Task11.Domain.ExpenseFinanceOperation;
using Task11.Domain.ExpenseFinanceOperation.ValueObjects;

namespace Task11.Application.ExpenseFinanceOperations.Queries.GetExpenceFinanceOperations
{
    public sealed class GetExpenceFinanceOperationsQueryHandler(IRepository<ExpenseFinanceOperation, ExpenseFinanceOperationId> repository) : IRequestHandler<GetExpenceFinanceOperationsQuery, IEnumerable<ExpenseFinanceOperationResult>>
    {
        private readonly IRepository<ExpenseFinanceOperation, ExpenseFinanceOperationId> _repository = repository;

        public async Task<IEnumerable<ExpenseFinanceOperationResult>> Handle(GetExpenceFinanceOperationsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<ExpenseFinanceOperation> operations = await _repository.GetAllAsync(cancellationToken);

            return operations.Select(operation => new ExpenseFinanceOperationResult(operation));
        }
    }
}
