using Task11.Domain.Common.Models;
using Task11.Domain.Common.ValueObjects;
using Task11.Domain.IncomeFinanceOperationAggregate.ValueObjects;
using Task11.Domain.IncomeType.ValueObjects;

namespace Task11.Domain.IncomeFinanceOperationAggregate
{
    public sealed class IncomeFinanceOperation(
        IncomeFinanceOperationId id,
        DateOnly date,
        IncomeTypeId incomeTypeId,
        Amount amount,
        string name) : AggregateRoot<IncomeFinanceOperationId>(id)
    {
        public IncomeTypeId IncomeTypeId { get; private set; } = incomeTypeId;

        public DateOnly Date { get; private set; } = date;

        public Amount Amount { get; private set; } = amount;

        public string Name { get; private set; } = name;

        public void Update(DateOnly date, IncomeTypeId incomeTypeId, Amount amount, string name)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

            Date = date;
            IncomeTypeId = incomeTypeId;
            Amount = amount;
            Name = name;
        }
    }
}
