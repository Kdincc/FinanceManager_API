using ErrorOr;
using MediatR;
using Task11.Domain.Common.ValueObjects;

namespace Task11.Application.IncomeTypes.Commands.Create
{
    public record class CreateIncomeTypeCommand(string Name, string Description, decimal Amount) : IRequest<ErrorOr<IncomeTypesResult>>;
}
