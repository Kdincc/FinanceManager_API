using MediatR;
using Task11.Application.Common.Persistance;
using Task11.Domain.ExpenseType;

namespace Task11.Application.ExpenseTypes.Queries.GetExpenseTypes
{
    public sealed class GetExpenseTypesQueryHandler(IExpenseTypeRepository repository) : IRequestHandler<GetExpenseTypesQuery, IEnumerable<ExpenseTypesResult>>
    {
        private readonly IExpenseTypeRepository _repository = repository;

        public async Task<IEnumerable<ExpenseTypesResult>> Handle(GetExpenseTypesQuery request, CancellationToken cancellationToken)
        {
            IReadOnlyCollection<ExpenseType> expenseTypes = await _repository.GetAllAsync(cancellationToken);

            return expenseTypes.Select(e => new ExpenseTypesResult(e));
        }
    }
}
