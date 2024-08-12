using ErrorOr;
using MediatR;
using Task11.Application.Common.Persistance;
using Task11.Domain.Common.Errors;
using Task11.Domain.ExpenseType;
using Task11.Domain.ExpenseType.ValueObjects;

namespace Task11.Application.ExpenseTypes.Commands.Delete
{
    public sealed class DeleteExpenseTypeCommandHandler(IExpenseTypeRepository repository) : IRequestHandler<DeleteExpenseTypeCommand, ErrorOr<ExpenseTypesResult>>
    {
        private readonly IExpenseTypeRepository _repository = repository;

        public async Task<ErrorOr<ExpenseTypesResult>> Handle(DeleteExpenseTypeCommand request, CancellationToken cancellationToken)
        {
            ExpenseTypeId expenseTypeId = ExpenseTypeId.Create(request.ExpenseTypeId);
            ExpenseType expenseType = await _repository.GetByIdAsync(expenseTypeId, cancellationToken);

            if (expenseType is null)
            {
                return Errors.ExpenseType.ExpenseTypeNotFound;
            }

            await _repository.DeleteAsync(expenseType, cancellationToken);

            return new ExpenseTypesResult(expenseType);
        }
    }
}
