﻿using ErrorOr;
using MediatR;
using Task11.Application.Common.Persistance;
using Task11.Domain.Common.Errors;
using Task11.Domain.ExpenseFinanceOperation.ValueObjects;
using Task11.Domain.IncomeFinanceOperation.ValueObjects;
using Task11.Domain.IncomeType;

namespace Task11.Application.IncomeTypes.Commands.Create
{
    public sealed class CreateIncomeTypeCommandHandler(IRepository<IncomeType, IncomeTypeId> repository) : IRequestHandler<CreateIncomeTypeCommand, ErrorOr<IncomeTypesResult>>
    {
        private readonly IRepository<IncomeType, IncomeTypeId> _repository = repository;

        public async Task<ErrorOr<IncomeTypesResult>> Handle(CreateIncomeTypeCommand request, CancellationToken cancellationToken)
        {
            IncomeType incomeTypeToCreate = new(IncomeTypeId.CreateUniq(), request.Name, request.Description);

            if (await HasSameIncomeType(_repository, incomeTypeToCreate)) 
            {
                return Errors.IncomeType.IncomeTypeNotFound;
            }

            await _repository.AddAsync(incomeTypeToCreate, cancellationToken);

            IncomeTypesResult result = new(incomeTypeToCreate);

            return result;
        }

        private async Task<bool> HasSameIncomeType(IRepository<IncomeType, IncomeTypeId> repository, IncomeType incnomeTypeToCheck)
        {
            await foreach (var incomeType in repository.GetAllAsAsyncEnumerable())
            {
                if (incomeType.HasSameNameAndDescription(incnomeTypeToCheck))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
