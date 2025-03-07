﻿using ErrorOr;
using MediatR;
using Task11.Application.Common.Persistance;
using Task11.Domain.Common.Errors;
using Task11.Domain.ExpenseType;
using Task11.Domain.ExpenseType.ValueObjects;

namespace Task11.Application.ExpenseTypes.Commands.Update
{
    public sealed class UpdateExpenseTypeCommandHandler(IExpenseTypeRepository repository) : IRequestHandler<UpdateExpenseTypeCommand, ErrorOr<ExpenseTypesResult>>
    {
        private readonly IExpenseTypeRepository _repository = repository;

        public async Task<ErrorOr<ExpenseTypesResult>> Handle(UpdateExpenseTypeCommand request, CancellationToken cancellationToken)
        {
            ExpenseTypeId expenseTypeId = ExpenseTypeId.Create(request.Id);

            ExpenseType expenseType = await _repository.GetByIdAsync(expenseTypeId, cancellationToken);

            if (expenseType is null)
            {
                return Errors.ExpenseType.ExpenseTypeNotFound;
            }

            expenseType.Update(request.Name, request.Description);

            if (await HasSameExpenseType(_repository, expenseType))
            {
                return Errors.ExpenseType.DuplicateExpenseType;
            }

            return new ExpenseTypesResult(expenseType);
        }

        private async Task<bool> HasSameExpenseType(IExpenseTypeRepository repository, ExpenseType expenseTypeToCheck)
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
