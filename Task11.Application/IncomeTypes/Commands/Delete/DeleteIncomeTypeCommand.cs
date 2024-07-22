using ErrorOr;
using MediatR;
using Task11.Domain.IncomeType.ValueObjects;

namespace Task11.Application.IncomeTypes.Commands.Delete
{
    public sealed record DeleteIncomeTypeCommand(string IncomeTypeId) : IRequest<ErrorOr<IncomeTypesResult>>;
}
