using ErrorOr;
using MediatR;
using Task11.Domain.Common.ValueObjects;
using Task11.Domain.IncomeType.ValueObjects;

namespace Task11.Application.IncomeTypes.Commands.Update
{
    public record UpdateIncomeTypeCommand(IncomeTypeId Id, string Name, string Description, Amount Amount) : IRequest<ErrorOr<IncomeTypesResult>>;
}
