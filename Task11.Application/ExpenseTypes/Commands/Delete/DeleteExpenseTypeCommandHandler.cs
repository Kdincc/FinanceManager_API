﻿using ErrorOr;
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

namespace Task11.Application.ExpenseTypes.Commands.Delete
{
    public sealed class DeleteExpenseTypeCommandHandler(IRepository<ExpenseType, ExpenseTypeId> repository) : IRequestHandler<DeleteExpenseTypeCommand, ErrorOr<ExpenseTypesResult>>
    {
        private readonly IRepository<ExpenseType, ExpenseTypeId> _repository = repository;

        public async Task<ErrorOr<ExpenseTypesResult>> Handle(DeleteExpenseTypeCommand request, CancellationToken cancellationToken)
        {
            ExpenseType expenseType = await _repository.GetByIdAsync(request.ExpenseTypeId, cancellationToken);

            if (expenseType is null) 
            {
                return Errors.ExpenseType.ExpenseTypeNotFound; 
            }

            await _repository.DeleteAsync(expenseType, cancellationToken);

            return new ExpenseTypesResult(expenseType);
        }
    }
}
