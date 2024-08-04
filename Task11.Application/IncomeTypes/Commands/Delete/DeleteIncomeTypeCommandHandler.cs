using ErrorOr;
using MediatR;
using Task11.Application.Common.Persistance;
using Task11.Domain.Common.Errors;
using Task11.Domain.IncomeType;
using Task11.Domain.IncomeType.ValueObjects;

namespace Task11.Application.IncomeTypes.Commands.Delete
{
    public sealed class DeleteIncomeTypeCommandHandler(IIncomeTypeRepository repository) : IRequestHandler<DeleteIncomeTypeCommand, ErrorOr<IncomeTypesResult>>
    {
        private readonly IIncomeTypeRepository _repository = repository;

        public async Task<ErrorOr<IncomeTypesResult>> Handle(DeleteIncomeTypeCommand request, CancellationToken cancellationToken)
        {
            IncomeTypeId incomeTypeId = IncomeTypeId.Create(request.Id);

            IncomeType incomeType = await _repository.GetByIdAsync(incomeTypeId, cancellationToken);

            if (incomeType is null)
            {
                return Errors.IncomeType.IncomeTypeNotFound;
            }

            await _repository.DeleteAsync(incomeType, cancellationToken);

            return new IncomeTypesResult(incomeType);
        }
    }
}
