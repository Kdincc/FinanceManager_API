﻿using ErrorOr;
using MediatR;
using Task11.Application.Common.Persistance;
using Task11.Domain.Common.Errors;
using Task11.Domain.Common.ValueObjects;
using Task11.Domain.ExpenseFinanceOperationAggregate;
using Task11.Domain.ExpenseFinanceOperationAggregate.ValueObjects;
using Task11.Domain.ExpenseType;
using Task11.Domain.ExpenseType.ValueObjects;

namespace Task11.Application.ExpenseFinanceOperations.Commands.Create
{
    public sealed class CreateExpenseFinanceOperationCommandHandler(
        IRepository<ExpenseFinanceOperation, ExpenseFinanceOperationId> expenseFinanceOperationRepository,
        IRepository<ExpenseType, ExpenseTypeId> expenseTypeRepository) : IRequestHandler<CreateExpenseFinanaceOperationCommand, ErrorOr<ExpenseFinanceOperationResult>>
    {
        private readonly IRepository<ExpenseFinanceOperation, ExpenseFinanceOperationId> _expenseFinanceOperationRepository = expenseFinanceOperationRepository;
        private readonly IRepository<ExpenseType, ExpenseTypeId> _expenseTypeRepository = expenseTypeRepository;

        public async Task<ErrorOr<ExpenseFinanceOperationResult>> Handle(CreateExpenseFinanaceOperationCommand request, CancellationToken cancellationToken)
        {
            ExpenseTypeId expenseTypeId = ExpenseTypeId.Create(request.ExpenseTypeId);
            Amount amount = Amount.Create(request.Amount);

            var expenseType = await _expenseTypeRepository.GetByIdAsync(expenseTypeId, cancellationToken);

            if (expenseType is null)
            {
                return Errors.ExpenseType.ExpenseTypeNotFound;
            }

            ExpenseFinanceOperation expenseFinanceOperation = new(
                ExpenseFinanceOperationId.CreateUniq(),
                DateOnly.Parse(request.Date),
                expenseTypeId,
                amount,
                request.Name);

            await _expenseFinanceOperationRepository.AddAsync(expenseFinanceOperation, cancellationToken);

            return new ExpenseFinanceOperationResult(expenseFinanceOperation);
        }
    }
}
