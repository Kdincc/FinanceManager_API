﻿using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Application.Common.Persistance;
using Task11.Domain.Common.Errors;
using Task11.Domain.IncomeFinanceOperation.Entities;
using Task11.Domain.IncomeFinanceOperation.ValueObjects;

namespace Task11.Application.IncomeTypes.Commands.Delete
{
    public sealed class DeleteIncomeTypeCommandHandler(IRepository<IncomeType, IncomeTypeId> repository) : IRequestHandler<DeleteIncomeTypeCommand, ErrorOr<IncomeTypesResult>>
    {
        private readonly IRepository<IncomeType, IncomeTypeId> _repository = repository;

        public async Task<ErrorOr<IncomeTypesResult>> Handle(DeleteIncomeTypeCommand request, CancellationToken cancellationToken)
        {
            IncomeType incomeType = await _repository.GetByIdAsync(request.IncomeTypeId, cancellationToken);

            if (incomeType is null) 
            {
                return Errors.IncomeType.IncomeTypeNotFound;
            }

            await _repository.DeleteAsync(incomeType, cancellationToken);

            return new IncomeTypesResult(incomeType);
        }
    }
}
