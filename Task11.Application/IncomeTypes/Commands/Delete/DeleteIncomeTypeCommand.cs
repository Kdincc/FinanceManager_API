using ErrorOr;
using MediatR;

namespace Task11.Application.IncomeTypes.Commands.Delete
{
    public sealed record DeleteIncomeTypeCommand(string IncomeTypeId) : IRequest<ErrorOr<IncomeTypesResult>>;
}
