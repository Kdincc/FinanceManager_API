using ErrorOr;
using MediatR;
using Task11.Application.Common.Persistance;
using Task11.Domain.Common.Errors;
using Task11.Domain.IncomeFinanceOperationAggregate;
using Task11.Domain.IncomeFinanceOperationAggregate.ValueObjects;

namespace Task11.Application.IncomeFinanceOperations.Commands.Delete
{
    public sealed class DeleteIncomeFinanceOperationCommandHandler(IRepository<IncomeFinanceOperation, IncomeFinanceOperationId> incomeFinanceOperationRepository) : IRequestHandler<DeleteIncomeFinanceOperationCommand, ErrorOr<IncomeFinanceOperationResult>>
    {
        private readonly IRepository<IncomeFinanceOperation, IncomeFinanceOperationId> _incomeFinanceOperationRepository = incomeFinanceOperationRepository;

        public async Task<ErrorOr<IncomeFinanceOperationResult>> Handle(DeleteIncomeFinanceOperationCommand request, CancellationToken cancellationToken)
        {
            IncomeFinanceOperationId incomeFinanceOperationId = IncomeFinanceOperationId.Create(request.IncomeFinanceOperationId);

            IncomeFinanceOperation financeOperation = await _incomeFinanceOperationRepository.GetByIdAsync(incomeFinanceOperationId, cancellationToken);

            if (financeOperation is null)
            {
                return Errors.IncomeFinanceOperation.IncomeFinanceOperationNotFound;
            }

            await _incomeFinanceOperationRepository.DeleteAsync(financeOperation, cancellationToken);

            return new IncomeFinanceOperationResult(financeOperation);
        }
    }
}
