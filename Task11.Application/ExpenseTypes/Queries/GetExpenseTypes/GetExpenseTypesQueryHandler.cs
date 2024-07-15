using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Application.Common.Persistance;
using Task11.Domain.ExpenseType;
using Task11.Domain.ExpenseType.ValueObjects;

namespace Task11.Application.ExpenseTypes.Queries.GetExpenseTypes
{
    public sealed class GetExpenseTypesQueryHandler(IRepository<ExpenseType, ExpenseTypeId> repository) : IRequestHandler<GetExpenseTypesQuery, IEnumerable<ExpenseTypesResult>>
    {
        private readonly IRepository<ExpenseType, ExpenseTypeId> _repository = repository;

        public async Task<IEnumerable<ExpenseTypesResult>> Handle(GetExpenseTypesQuery request, CancellationToken cancellationToken)
        {
            IReadOnlyCollection<ExpenseType> expenseTypes = await _repository.GetAllAsync(cancellationToken);

            return expenseTypes.Select(e => new ExpenseTypesResult(e));
        }
    }
}
