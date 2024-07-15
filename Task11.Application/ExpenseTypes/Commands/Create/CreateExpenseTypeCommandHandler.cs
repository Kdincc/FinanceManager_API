using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Application.Common.Persistance;
using Task11.Domain.Common.Errors;
using Task11.Domain.ExpenseFinanceOperation.ValueObjects;
using Task11.Domain.ExpenseType;

namespace Task11.Application.ExpenseTypes.Commands.Create
{
    public sealed class CreateExpenseTypeCommandHandler(IRepository<ExpenseType, ExpenseTypeId> repository) : IRequestHandler<CreateExpenseTypeCommand, ErrorOr<ExpenseTypesResult>>
    {
        private readonly IRepository<ExpenseType, ExpenseTypeId> _repository = repository;

        public async Task<ErrorOr<ExpenseTypesResult>> Handle(CreateExpenseTypeCommand request, CancellationToken cancellationToken)
        {
            ExpenseType expenseType = new(ExpenseTypeId.CreateUniq(), request.Name, request.Description);

            var expenseTypes = await _repository.GetAllAsync(cancellationToken);

            if(expenseTypes.Any(e => e.Name == expenseType.Name && e.Description == expenseType.Description)) 
            {
                return Errors.ExpenseType.DuplicateExpenseType;
            }

            await _repository.AddAsync(expenseType, cancellationToken);

            return new ExpenseTypesResult(expenseType);
        }
    }
}
