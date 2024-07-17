using ErrorOr;
using MediatR;

namespace Task11.Application.ExpenseTypes.Commands.Create
{
    public record CreateExpenseTypeCommand(string Name, string Description) : IRequest<ErrorOr<ExpenseTypesResult>>;
}
