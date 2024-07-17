using ErrorOr;
using MediatR;
using Task11.Application.Common.Persistance;
using Task11.Domain.Common.Errors;
using Task11.Domain.IncomeType;
using Task11.Domain.IncomeType.ValueObjects;

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

            incomeTypeToUpdate.Update(request.Name, request.Description);

            if (await HasSameIncomeType(_repository, incomeTypeToUpdate))
            {
                return Errors.IncomeType.DuplicateIncomeType;
            }

            await _repository.UpdateAsync(incomeTypeToUpdate, cancellationToken);

            return new IncomeTypesResult(incomeTypeToUpdate);
        }

        private async Task<bool> HasSameIncomeType(IRepository<IncomeType, IncomeTypeId> repository, IncomeType incnomeTypeToCheck)
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
