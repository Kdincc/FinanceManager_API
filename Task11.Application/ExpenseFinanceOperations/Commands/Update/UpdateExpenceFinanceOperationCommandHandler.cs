using ErrorOr;
using MediatR;
using Task11.Application.Common.Persistance;
using Task11.Domain.ExpenseFinanceOperation;
using Task11.Domain.ExpenseFinanceOperation.ValueObjects;
using Task11.Domain.Common.Errors;

namespace Task11.Application.ExpenseFinanceOperations.Commands.Update
{
    public sealed class UpdateExpenceFinanceOperationCommandHandler(IRepository<ExpenseFinanceOperation, ExpenseFinanceOperationId> repository) : IRequestHandler<UpdateExpenceFinanceOperationCommand, ErrorOr<ExpenseFinanceOperationResult>>
    {
        private readonly IRepository<ExpenseFinanceOperation, ExpenseFinanceOperationId> _repository = repository;

        public async Task<ErrorOr<ExpenseFinanceOperationResult>> Handle(UpdateExpenceFinanceOperationCommand request, CancellationToken cancellationToken)
        {
            ExpenseFinanceOperation expenseFinanceOperation = await _repository.GetByIdAsync(request.ExpenseFinanceOperationId, cancellationToken);

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
