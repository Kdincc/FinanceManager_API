using ErrorOr;
using MediatR;
using Task11.Application.IncomeTypes;

namespace Task11.Application.IncomeType.Commands.Create
{
    public record class CreateIncomeTypeCommand(string Name, string Description) : IRequest<ErrorOr<IncomeTypesResult>>;
}
