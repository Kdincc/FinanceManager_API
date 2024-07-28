using ErrorOr;
using MediatR;
using Task11.Application.Common.Persistance;
using Task11.Domain.Common.Errors;
using Task11.Domain.ExpenseType;
using Task11.Domain.ExpenseType.ValueObjects;

namespace Task11.Application.ExpenseTypes.Commands.Create
{
    public sealed class CreateExpenseTypeCommandHandler(IExpenseTypeRepository repository) : IRequestHandler<CreateExpenseTypeCommand, ErrorOr<ExpenseTypesResult>>
    {
        private readonly IExpenseTypeRepository _repository = repository;

        public async Task<ErrorOr<ExpenseTypesResult>> Handle(CreateExpenseTypeCommand request, CancellationToken cancellationToken)
        {
            ExpenseType expenseType = new(ExpenseTypeId.CreateUniq(), request.Name, request.Description);

            if (await HasSameExpenseType(_repository, expenseType))
            {
                return Errors.ExpenseType.DuplicateExpenseType;
            }

            await _repository.AddAsync(expenseType, cancellationToken);

            return new ExpenseTypesResult(expenseType);
        }

        private async Task<bool> HasSameExpenseType(IRepository<ExpenseType, ExpenseTypeId> repository, ExpenseType expenseTypeToCheck)
        {
            await foreach (var expenseType in repository.GetAllAsAsyncEnumerable())
            {
                if (expenseType.HasSameNameAndDescription(expenseTypeToCheck))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
