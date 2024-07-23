using ErrorOr;
using MediatR;
using Task11.Application.Common.Persistance;
using Task11.Domain.Common.Errors;
using Task11.Domain.Common.ValueObjects;
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
            Amount amount = Amount.Create(request.Amount);
            ExpenseFinanceOperationId expenseFinanceOperationId = ExpenseFinanceOperationId.Create(request.ExpenseFinanceOperationId);
            ExpenseTypeId expenseTypeId = ExpenseTypeId.Create(request.ExpenceTypeId);

            ExpenseFinanceOperation expenseFinanceOperation = await _repository.GetByIdAsync
                (expenseFinanceOperationId,
                cancellationToken);

            ExpenseType expenseType = await _expenseTypeRepository.GetByIdAsync(expenseTypeId, cancellationToken);

            if (expenseType is null)
            {
                return Errors.ExpenseType.ExpenseTypeNotFound;
            }

            if (expenseFinanceOperation is null)
            {
                return Errors.ExpenceFinanceOperation.ExpenceFinanceOperationNotFound;
            }

            expenseFinanceOperation.Update(
                DateOnly.Parse(request.Date),
                expenseTypeId,
                amount,
                request.Name);

            await _repository.UpdateAsync(expenseFinanceOperation, cancellationToken);

            return new ExpenseFinanceOperationResult(expenseFinanceOperation);
        }
    }
}
