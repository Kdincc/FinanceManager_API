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
            IncomeType incomeType = new(IncomeTypeId.CreateUniq(), request.Name, request.Description);

            var incomeTypes =  await _repository.GetAllAsync(cancellationToken);

            if (incomeTypes.Any(i => i.Name == incomeType.Name && i.Description == incomeType.Description))
            {
                return Errors.IncomeType.DuplicateIncomeType;
            }

            await _repository.AddAsync(incomeType, cancellationToken);

            IncomeTypesResult result = new(incomeType);

            return result;
        }
    }
}
