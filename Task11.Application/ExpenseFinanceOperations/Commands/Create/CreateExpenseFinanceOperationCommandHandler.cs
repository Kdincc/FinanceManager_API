using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Application.Common.Persistance;
using Task11.Domain.ExpenseFinanceOperation;
using Task11.Domain.ExpenseFinanceOperation.ValueObjects;

namespace Task11.Application.ExpenseFinanceOperations.Commands.Create
{
    public sealed class CreateExpenseFinanceOperationCommandHandler(IRepository<ExpenseFinanceOperation, ExpenseFinanceOperationId> repository) : IRequestHandler<CreateExpenseFinanaceOperationCommand, ExpenseFinanceOperationResult>
    {
        private readonly IRepository<ExpenseFinanceOperation, ExpenseFinanceOperationId> _repository = repository;

        public async Task<ExpenseFinanceOperationResult> Handle(CreateExpenseFinanaceOperationCommand request, CancellationToken cancellationToken)
        {
            ExpenseFinanceOperation expenseFinanceOperation = new(
                ExpenseFinanceOperationId.Create(Guid.NewGuid()),
                request.Date,
                request.ExpenseTypeId,
                request.Amount,
                request.Name);

            await _repository.AddAsync(expenseFinanceOperation, cancellationToken);

            return new ExpenseFinanceOperationResult(expenseFinanceOperation);
        }
    }
}
