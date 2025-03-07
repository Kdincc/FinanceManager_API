﻿using ErrorOr;
using MediatR;
using Task11.Application.Common.Persistance;
using Task11.Domain.Common.Errors;
using Task11.Domain.ExpenseFinanceOperationAggregate;
using Task11.Domain.ExpenseFinanceOperationAggregate.ValueObjects;

namespace Task11.Application.ExpenseFinanceOperations.Commands.Delete
{
    public sealed class DeleteExpenceFinanceOperationCommandHandler(IExpenseFinanceOperationRepository repository) : IRequestHandler<DeleteExpenceFinanceOperationCommand, ErrorOr<ExpenseFinanceOperationResult>>
    {
        private readonly IExpenseFinanceOperationRepository _repository = repository;

        public async Task<ErrorOr<ExpenseFinanceOperationResult>> Handle(DeleteExpenceFinanceOperationCommand request, CancellationToken cancellationToken)
        {
            ExpenseFinanceOperation expenseFinanceOperation = await _repository.GetByIdAsync(
                ExpenseFinanceOperationId.Create(request.Id),
                cancellationToken);

            if (expenseFinanceOperation is null)
            {
                return Errors.ExpenceFinanceOperation.ExpenceFinanceOperationNotFound;
            }

            await _repository.DeleteAsync(expenseFinanceOperation, cancellationToken);

            return new ExpenseFinanceOperationResult(expenseFinanceOperation);
        }
    }
}
