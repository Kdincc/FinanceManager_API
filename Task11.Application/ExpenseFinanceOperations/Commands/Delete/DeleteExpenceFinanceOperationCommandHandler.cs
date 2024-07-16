using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Application.Common.Persistance;
using Task11.Domain.Common.Errors;
using Task11.Domain.ExpenseFinanceOperation;
using Task11.Domain.ExpenseFinanceOperation.ValueObjects;

namespace Task11.Application.ExpenseFinanceOperations.Commands.Delete
{
    public sealed class DeleteExpenceFinanceOperationCommandHandler(IRepository<ExpenseFinanceOperation, ExpenseFinanceOperationId> repository) : IRequestHandler<DeleteExpenceFinanceOperationCommand, ErrorOr<ExpenseFinanceOperationResult>>
    {
        private readonly IRepository<ExpenseFinanceOperation, ExpenseFinanceOperationId> _repository = repository;

        public async Task<ErrorOr<ExpenseFinanceOperationResult>> Handle(DeleteExpenceFinanceOperationCommand request, CancellationToken cancellationToken)
        {
            ExpenseFinanceOperation expenseFinanceOperation = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (expenseFinanceOperation is null)
            {
                return Errors.ExpenceFinanceOperation.ExpenceFinanceOperationNotFound;
            }

            await _repository.DeleteAsync(expenseFinanceOperation, cancellationToken);

            return new ExpenseFinanceOperationResult(expenseFinanceOperation);
        }
    }
}
