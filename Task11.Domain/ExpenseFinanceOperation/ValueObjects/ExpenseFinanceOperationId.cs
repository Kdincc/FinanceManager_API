using Task11.Domain.Common.Models;

namespace Task11.Domain.ExpenseFinanceOperation.ValueObjects
{
    public sealed class ExpenseFinanceOperationId : ValueObject
    {
        private ExpenseFinanceOperationId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; private set; }

        public static ExpenseFinanceOperationId CreateUniq() => new(Guid.NewGuid());

        public static ExpenseFinanceOperationId Create(Guid value) => new(value);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
