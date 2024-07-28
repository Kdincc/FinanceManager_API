using MediatR;
using Task11.Application.Common.Persistance;
using Task11.Domain.ExpenseFinanceOperationAggregate;

namespace Task11.Application.ExpenseFinanceOperations.Queries.GetExpenceFinanceOperations
{
    public sealed class GetExpenceFinanceOperationsQueryHandler(IExpenseFinanceOperationRepository repository) : IRequestHandler<GetExpenceFinanceOperationsQuery, IEnumerable<ExpenseFinanceOperationResult>>
    {
        private readonly IExpenseFinanceOperationRepository _repository = repository;

        public async Task<IEnumerable<ExpenseFinanceOperationResult>> Handle(GetExpenceFinanceOperationsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<ExpenseFinanceOperation> operations = await _repository.GetAllAsync(cancellationToken);

            return operations.Select(operation => new ExpenseFinanceOperationResult(operation));
        }
    }
}
