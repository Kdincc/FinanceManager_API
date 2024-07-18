using ErrorOr;
using MediatR;
using Task11.Application.Common.Persistance;
using Task11.Domain.Common.Errors;
using Task11.Domain.ExpenseFinanceOperationAggregate;
using Task11.Domain.ExpenseFinanceOperationAggregate.ValueObjects;
using Task11.Domain.ExpenseType;
using Task11.Domain.ExpenseType.ValueObjects;

namespace Task11.Application.ExpenseFinanceOperations.Commands.Update
{
    public sealed class UpdateExpenceFinanceOperationCommandHandler(IRepository<ExpenseFinanceOperation, ExpenseFinanceOperationId> repository, IRepository<ExpenseType, ExpenseTypeId> expenseTypeRepository) : IRequestHandler<UpdateExpenceFinanceOperationCommand, ErrorOr<ExpenseFinanceOperationResult>>
    {
        private readonly IRepository<ExpenseFinanceOperation, ExpenseFinanceOperationId> _repository = repository;
        private readonly IRepository<ExpenseType, ExpenseTypeId> _expenseTypeRepository = expenseTypeRepository;

        public async Task<ErrorOr<ExpenseFinanceOperationResult>> Handle(UpdateExpenceFinanceOperationCommand request, CancellationToken cancellationToken)
        {
            ExpenseFinanceOperation expenseFinanceOperation = await _repository.GetByIdAsync(request.ExpenseFinanceOperationId, cancellationToken);
            ExpenseType expenseType = await _expenseTypeRepository.GetByIdAsync(request.ExpenceTypeId, cancellationToken);

            if (expenseType is null)
            {
                return Errors.ExpenseType.ExpenseTypeNotFound;
            }

            if (expenseFinanceOperation is null)
            {
                return Errors.ExpenceFinanceOperation.ExpenceFinanceOperationNotFound;
            }

            expenseFinanceOperation.Update(
                request.Date,
                request.ExpenceTypeId,
                request.Amount,
                request.Name);

            await _repository.UpdateAsync(expenseFinanceOperation, cancellationToken);

            return new ExpenseFinanceOperationResult(expenseFinanceOperation);
        }
    }
}
