using ErrorOr;
using MediatR;
using Task11.Application.Common.Persistance;
using Task11.Domain.Common.Errors;
using Task11.Domain.IncomeFinanceOperation.ValueObjects;
using Task11.Domain.IncomeType;

namespace Task11.Application.IncomeTypes.Commands.Update
{
    public sealed class UpdateIncomeTypeCommandHandler(IRepository<IncomeType, IncomeTypeId> repository) : IRequestHandler<UpdateIncomeTypeCommand, ErrorOr<IncomeTypesResult>>
    {
        private readonly IRepository<IncomeType, IncomeTypeId> _repository = repository;

        public async Task<ErrorOr<IncomeTypesResult>> Handle(UpdateIncomeTypeCommand request, CancellationToken cancellationToken)
        {
            IncomeType incomeTypeToUpdate = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (incomeTypeToUpdate is null)
            {
                return Errors.IncomeType.IncomeTypeNotFound;
            }

            incomeTypeToUpdate.ChangeName(request.Name);
            incomeTypeToUpdate.ChangeDescription(request.Description);

            await foreach(var incomeType in _repository.GetAllAsAsyncEnumerable())
            {
                if (incomeType.HasSameNameAndDescription(incomeTypeToUpdate))
                {
                    return Errors.IncomeType.DuplicateIncomeType;
                }
            }

            await _repository.UpdateAsync(incomeTypeToUpdate, cancellationToken);

            return new IncomeTypesResult(incomeTypeToUpdate);
        }
    }
}
