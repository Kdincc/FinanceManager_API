using ErrorOr;
using MediatR;

namespace Task11.Application.IncomeTypes.Commands.Create
{
    public record class CreateIncomeTypeCommand(string Name, string Description) : IRequest<ErrorOr<IncomeTypesResult>>;
}
