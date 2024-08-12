using Task11.Domain.Common.Models;
using Task11.Domain.ExpenseType.ValueObjects;

namespace Task11.Domain.ExpenseType
{
    public sealed class ExpenseType(ExpenseTypeId id,
                                   string name,
                                   string description) : OperationType<ExpenseTypeId>(id, name, description)
    {
    }
}
