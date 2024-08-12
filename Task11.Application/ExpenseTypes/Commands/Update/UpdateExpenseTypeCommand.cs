using ErrorOr;
using MediatR;

namespace Task11.Application.ExpenseTypes.Commands.Update
{
    public record UpdateExpenseTypeCommand(string Id, string Name, string Description) : IRequest<ErrorOr<ExpenseTypesResult>>;
}
