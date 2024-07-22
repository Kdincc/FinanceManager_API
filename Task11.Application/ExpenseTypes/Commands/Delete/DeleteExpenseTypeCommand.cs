using ErrorOr;
using MediatR;
using Task11.Domain.ExpenseType.ValueObjects;

namespace Task11.Application.ExpenseTypes.Commands.Delete
{
    public record DeleteExpenseTypeCommand(string ExpenseTypeId) : IRequest<ErrorOr<ExpenseTypesResult>>;
}
