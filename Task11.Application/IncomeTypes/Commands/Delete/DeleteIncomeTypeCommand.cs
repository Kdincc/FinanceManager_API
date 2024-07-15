using ErrorOr;
using MediatR;
using Task11.Domain.IncomeType.ValueObjects;

namespace Task11.Application.IncomeTypes.Commands.Delete
{
    public sealed record DeleteIncomeTypeCommand(IncomeTypeId IncomeTypeId) : IRequest<ErrorOr<IncomeTypesResult>>;
}
