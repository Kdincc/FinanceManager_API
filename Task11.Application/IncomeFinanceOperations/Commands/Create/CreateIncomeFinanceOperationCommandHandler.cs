using ErrorOr;
using MediatR;
using Task11.Application.Common.Persistance;
using Task11.Domain.Common.Errors;
using Task11.Domain.Common.ValueObjects;
using Task11.Domain.IncomeFinanceOperationAggregate;
using Task11.Domain.IncomeFinanceOperationAggregate.ValueObjects;
using Task11.Domain.IncomeType.ValueObjects;

namespace Task11.Application.IncomeFinanceOperations.Commands.Create
{
    public sealed class CreateIncomeFinanceOperationCommandHandler(
        IIncomeFinanceOperationRepository financeOperationRepository,
        Common.Persistance.IncomeType incomeTypeRepository) : IRequestHandler<CreateIncomeFinanceOperationCommand, ErrorOr<IncomeFinanceOperationResult>>
    {
        private readonly Common.Persistance.IncomeType _incomeTypeRepository = incomeTypeRepository;
        private readonly IIncomeFinanceOperationRepository _financeOperationRepository = financeOperationRepository;

        public async Task<ErrorOr<IncomeFinanceOperationResult>> Handle(CreateIncomeFinanceOperationCommand request, CancellationToken cancellationToken)
        {
            Amount amount = Amount.Create(request.Amount);
            IncomeTypeId incomeTypeId = IncomeTypeId.Create(request.IncomeTypeId);

            Domain.IncomeType.IncomeType incomeType = await _incomeTypeRepository.GetByIdAsync(incomeTypeId, cancellationToken);

            if (incomeType is null)
            {
                return Errors.IncomeType.IncomeTypeNotFound;
            }

            IncomeFinanceOperation financeOperation = new(
                IncomeFinanceOperationId.CreateUniq(),
                DateOnly.Parse(request.Date),
                incomeTypeId,
                amount,
                request.Name);

            await _financeOperationRepository.AddAsync(financeOperation, cancellationToken);

            return new IncomeFinanceOperationResult(financeOperation);
        }
    }
}
