using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Application.Common.Persistance;
using Task11.Domain.IncomeFinanceOperation;
using Task11.Domain.IncomeFinanceOperation.ValueObjects;
using Task11.Domain.IncomeType;
using Task11.Domain.IncomeType.ValueObjects;
using Task11.Domain.Common.Errors;

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
            IncomeType incomeType = await _incomeTypeRepository.GetByIdAsync(request.IncomeTypeId, cancellationToken);

            if (incomeType is null)
            {
                return Errors.IncomeType.IncomeTypeNotFound;
            }

            IncomeFinanceOperation financeOperation = await _incomeFinanceOperationRepository.GetByIdAsync(request.IncomeFinanceOperationId, cancellationToken);

            if (financeOperation is null)
            {
                return Errors.IncomeFinanceOperation.IncomeFinanceOperationNotFound;
            }

            financeOperation.Update(
                request.Date,
                request.IncomeTypeId,
                request.Amount,
                request.Name);

            await _incomeFinanceOperationRepository.UpdateAsync(financeOperation, cancellationToken);

            return new IncomeFinanceOperationResult(financeOperation);
        }
    }
}
