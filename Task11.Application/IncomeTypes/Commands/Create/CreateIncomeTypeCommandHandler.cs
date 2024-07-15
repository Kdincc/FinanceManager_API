using ErrorOr;
using MediatR;
using Task11.Application.Common.Persistance;
using Task11.Domain.Common.Errors;
using Task11.Domain.IncomeFinanceOperation.ValueObjects;
using Task11.Domain.IncomeType;

namespace Task11.Application.IncomeTypes.Commands.Create
{
    public sealed class CreateIncomeTypeCommandHandler(IRepository<IncomeType, IncomeTypeId> repository) : IRequestHandler<CreateIncomeTypeCommand, ErrorOr<IncomeTypesResult>>
    {
        private readonly IRepository<IncomeType, IncomeTypeId> _repository = repository;

        public async Task<ErrorOr<IncomeTypesResult>> Handle(CreateIncomeTypeCommand request, CancellationToken cancellationToken)
        {
            IncomeType incomeTypeToCreate = new(IncomeTypeId.CreateUniq(), request.Name, request.Description);

            await foreach (var incomeType in _repository.GetAllAsAsyncEnumerable())
            {
                if (incomeType.HasSameNameAndDescription(incomeTypeToCreate))
                {
                    return Errors.IncomeType.DuplicateIncomeType;
                }
            }

            await _repository.AddAsync(incomeTypeToCreate, cancellationToken);

            IncomeTypesResult result = new(incomeTypeToCreate);

            return result;
        }
    }
}
