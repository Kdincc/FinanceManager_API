using ErrorOr;
using MediatR;
using Task11.Application.Common.Persistance;
using Task11.Domain.Common.Errors;
using Task11.Domain.IncomeFinanceOperation;
using Task11.Domain.IncomeFinanceOperation.ValueObjects;
using Task11.Domain.IncomeType;
using Task11.Domain.IncomeType.ValueObjects;

namespace Task11.Application.IncomeFinanceOperations.Commands.Create
{
    public sealed class CreateIncomeFinanceOperationCommandHandler(
        IRepository<IncomeFinanceOperation, IncomeFinanceOperationId> financeOperationRepository,
        IRepository<IncomeType, IncomeTypeId> incomeTypeRepository) : IRequestHandler<CreateIncomeFinanceOperationCommand, ErrorOr<IncomeFinanceOperationResult>>
    {
        private readonly IRepository<IncomeType, IncomeTypeId> _incomeTypeRepository = incomeTypeRepository;
        private readonly IRepository<IncomeFinanceOperation, IncomeFinanceOperationId> _financeOperationRepository = financeOperationRepository;

        public  async Task<ErrorOr<IncomeFinanceOperationResult>> Handle(CreateIncomeFinanceOperationCommand request, CancellationToken cancellationToken)
        {
            IncomeType incomeType = await _incomeTypeRepository.GetByIdAsync(request.IncomeTypeId, cancellationToken);

            if (incomeType is null)
            {
                return Errors.IncomeType.IncomeTypeNotFound;
            }

            IncomeFinanceOperation financeOperation = new(
                IncomeFinanceOperationId.CreateUniq(),
                request.Date,
                request.IncomeTypeId,
                request.Amount,
                request.Name);

            await _financeOperationRepository.AddAsync(financeOperation, cancellationToken);

            return new IncomeFinanceOperationResult(financeOperation);
        }
    }
}
