using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Application.Common.Persistance;
using Task11.Domain.Common.Errors;
using Task11.Domain.IncomeFinanceOperation.ValueObjects;
using Task11.Domain.IncomeType;

namespace Task11.Application.IncomeTypes.Commands.Update
{
    public sealed class UpdateIncomeTypeCommandHandler(IRepository<IncomeType, IncomeTypeId> repository) : IRequestHandler<UpdateIncomeTypeCommand, ErrorOr<IncomeTypesResult>>
    {
        private readonly IRepository<IncomeType, IncomeTypeId> _repository = repository;

        public async Task<ErrorOr<IncomeTypesResult>> Handle(UpdateIncomeTypeCommand request, CancellationToken cancellationToken)
        {
            IncomeType incomeType = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (incomeType is null) 
            {
                return Errors.IncomeType.IncomeTypeNotFound;
            }

            incomeType.ChangeName(request.Name);
            incomeType.ChangeDescription(request.Description);

            await _repository.UpdateAsync(incomeType, cancellationToken);

            return new IncomeTypesResult(incomeType);
        }
    }
}
