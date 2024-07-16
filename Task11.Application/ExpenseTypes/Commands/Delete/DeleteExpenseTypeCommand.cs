using ErrorOr;
using MediatR;
using Task11.Domain.ExpenseType.ValueObjects;

namespace Task11.Application.ExpenseTypes.Commands.Delete
{
    public record DeleteExpenseTypeCommand(ExpenseTypeId ExpenseTypeId) : IRequest<ErrorOr<ExpenseTypesResult>>;
}
