using ErrorOr;
using MediatR;

namespace Task11.Application.IncomeTypes.Commands.Update
{
    public record UpdateIncomeTypeCommand(string Id, string Name, string Description) : IRequest<ErrorOr<IncomeTypesResult>>;
}
