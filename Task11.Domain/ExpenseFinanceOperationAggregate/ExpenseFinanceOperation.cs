using Task11.Domain.Common.Models;
using Task11.Domain.Common.ValueObjects;
using Task11.Domain.ExpenseFinanceOperationAggregate.ValueObjects;
using Task11.Domain.ExpenseType.ValueObjects;

namespace Task11.Domain.ExpenseFinanceOperationAggregate
{
    public sealed class ExpenseFinanceOperation(ExpenseFinanceOperationId id,
                                                DateOnly date,
                                                ExpenseTypeId expenseTypeId,
                                                Amount amount,
                                                string name) : FinanceOperation<ExpenseFinanceOperationId>(id, name, amount, date)
    {
        public ExpenseTypeId ExpenseTypeId { get; private set; } = expenseTypeId;

        public void Update(DateOnly date, ExpenseTypeId expenseTypeId, Amount amount, string name)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

            Date = date;
            ExpenseTypeId = expenseTypeId;
            Amount = amount;
            Name = name;
        }
    }
}
