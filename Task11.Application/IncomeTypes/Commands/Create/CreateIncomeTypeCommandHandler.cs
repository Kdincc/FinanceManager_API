using ErrorOr;
using MediatR;
using Task11.Application.Common.Persistance;
using Task11.Domain.Common.Errors;
using Task11.Domain.IncomeType.ValueObjects;

namespace Task11.Application.IncomeTypes.Commands.Create
{
    public sealed class CreateIncomeTypeCommandHandler(Common.Persistance.IncomeType repository) : IRequestHandler<CreateIncomeTypeCommand, ErrorOr<IncomeTypesResult>>
    {
        private readonly Common.Persistance.IncomeType _repository = repository;

        public async Task<ErrorOr<IncomeTypesResult>> Handle(CreateIncomeTypeCommand request, CancellationToken cancellationToken)
        {
            Domain.IncomeType.IncomeType incomeTypeToCreate = new(IncomeTypeId.CreateUniq(), request.Name, request.Description);

            if (await HasSameIncomeType(_repository, incomeTypeToCreate))
            {
                return Errors.IncomeType.DuplicateIncomeType;
            }

            await _repository.AddAsync(incomeTypeToCreate, cancellationToken);

            IncomeTypesResult result = new(incomeTypeToCreate);

            return result;
        }

        private async Task<bool> HasSameIncomeType(IRepository<Domain.IncomeType.IncomeType, IncomeTypeId> repository, Domain.IncomeType.IncomeType incnomeTypeToCheck)
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
