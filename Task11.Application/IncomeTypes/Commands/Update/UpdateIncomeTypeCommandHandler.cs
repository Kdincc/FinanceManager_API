using ErrorOr;
using MediatR;
using Task11.Application.Common.Persistance;
using Task11.Domain.Common.Errors;
using Task11.Domain.IncomeType;
using Task11.Domain.IncomeType.ValueObjects;

namespace Task11.Application.IncomeTypes.Commands.Update
{
    public sealed class UpdateIncomeTypeCommandHandler(IIncomeTypeRepository repository) : IRequestHandler<UpdateIncomeTypeCommand, ErrorOr<IncomeTypesResult>>
    {
        private readonly IIncomeTypeRepository _repository = repository;

        public async Task<ErrorOr<IncomeTypesResult>> Handle(UpdateIncomeTypeCommand request, CancellationToken cancellationToken)
        {
            IncomeTypeId incomeTypeId = IncomeTypeId.Create(request.Id);

            IncomeType incomeTypeToUpdate = await _repository.GetByIdAsync(incomeTypeId, cancellationToken);

            if (incomeTypeToUpdate is null)
            {
                return Errors.IncomeType.IncomeTypeNotFound;
            }

            incomeTypeToUpdate.Update(request.Name, request.Description);

            if (await HasSameIncomeType(_repository, incomeTypeToUpdate))
            {
                return Errors.IncomeType.DuplicateIncomeType;
            }

            await _repository.UpdateAsync(incomeTypeToUpdate, cancellationToken);

            return new IncomeTypesResult(incomeTypeToUpdate);
        }

        private async Task<bool> HasSameIncomeType(IIncomeTypeRepository repository, IncomeType incnomeTypeToCheck)
        {
            await foreach (var incomeType in repository.GetAllAsAsyncEnumerable())
            {
                if (incomeType.HasSameNameAndDescription(incnomeTypeToCheck))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
