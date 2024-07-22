using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Application.Common.Persistance;
using Task11.Domain.IncomeFinanceOperationAggregate;
using Task11.Domain.IncomeFinanceOperationAggregate.ValueObjects;
using Task11.Domain.IncomeType;
using Task11.Domain.IncomeType.ValueObjects;
using Task11.Domain.Common.Errors;
using Task11.Domain.Common.ValueObjects;

namespace Task11.Application.IncomeFinanceOperations.Commands.Update
{
    public sealed class UpdateIncomeFinanceOperationCommandHandler(
        IRepository<IncomeFinanceOperation, IncomeFinanceOperationId> incomeFinanceOperationRepository,
        IRepository<IncomeType, IncomeTypeId> incomeTypeRepository) : IRequestHandler<UpdateIncomeFinanceOperationCommand, ErrorOr<IncomeFinanceOperationResult>>
    {
        private readonly IRepository<IncomeFinanceOperation, IncomeFinanceOperationId> _incomeFinanceOperationRepository = incomeFinanceOperationRepository;
        private readonly IRepository<IncomeType, IncomeTypeId> _incomeTypeRepository = incomeTypeRepository;

        public async Task<ErrorOr<IncomeFinanceOperationResult>> Handle(UpdateIncomeFinanceOperationCommand request, CancellationToken cancellationToken)
        {
            Amount amount = Amount.Create(request.Amount);
            IncomeTypeId incomeTypeId = IncomeTypeId.Create(request.IncomeTypeId);
            IncomeFinanceOperationId incomeFinanceOperationId = IncomeFinanceOperationId.Create(request.IncomeFinanceOperationId);

            IncomeType incomeType = await _incomeTypeRepository.GetByIdAsync(incomeTypeId, cancellationToken);

            if (incomeType is null)
            {
                return Errors.IncomeType.IncomeTypeNotFound;
            }

            IncomeFinanceOperation financeOperation = await _incomeFinanceOperationRepository.GetByIdAsync(incomeFinanceOperationId, cancellationToken);

            if (financeOperation is null)
            {
                return Errors.IncomeFinanceOperation.IncomeFinanceOperationNotFound;
            }

            financeOperation.Update(
                DateOnly.Parse(request.Date),
                incomeTypeId,
                amount,
                request.Name);

            await _incomeFinanceOperationRepository.UpdateAsync(financeOperation, cancellationToken);

            return new IncomeFinanceOperationResult(financeOperation);
        }
    }
}
