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

            if (await HasSameExpenseType(_repository, expenseType)) 
            {
                return Errors.ExpenseType.DuplicateExpenseType;
            }

            await _repository.AddAsync(expenseType, cancellationToken);

            return new ExpenseTypesResult(expenseType);
        }

        private async Task<bool> HasSameExpenseType(IRepository<ExpenseType, ExpenseTypeId> repository, ExpenseType expenseTypeToCheck)
        {
            await foreach (var expenseType in repository.GetAllAsAsyncEnumerable())
            {
                if (expenseType.HasSameNameAndDescription(expenseTypeToCheck))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
