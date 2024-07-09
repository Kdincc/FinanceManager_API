using ErrorOr;
using MediatR;
using Task11.Application.Common.Persistance;
using Task11.Domain.IncomeFinanceOperation.Entities;
using Task11.Domain.IncomeFinanceOperation.ValueObjects;

namespace Task11.Application.IncomeTypes.Commands.Create
{
    public sealed class CreateIncomeTypeCommandHandler(IRepository<IncomeType> repository) : IRequestHandler<CreateIncomeTypeCommand, ErrorOr<IncomeTypesResult>>
    {
        private readonly IRepository<IncomeType> _repository = repository;

        public async Task<ErrorOr<IncomeTypesResult>> Handle(CreateIncomeTypeCommand request, CancellationToken cancellationToken)
        {
            IncomeType incomeType = new(IncomeTypeId.CreateUniq(), request.Name, request.Description);

            await _repository.AddAsync(incomeType, cancellationToken);

            IncomeTypesResult result = new(incomeType);

            return result;
        }
    }
}
