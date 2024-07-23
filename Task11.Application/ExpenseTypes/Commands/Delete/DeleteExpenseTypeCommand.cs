using ErrorOr;
using MediatR;

namespace Task11.Application.ExpenseTypes.Commands.Delete
{
    public record DeleteExpenseTypeCommand(string ExpenseTypeId) : IRequest<ErrorOr<ExpenseTypesResult>>;
}
