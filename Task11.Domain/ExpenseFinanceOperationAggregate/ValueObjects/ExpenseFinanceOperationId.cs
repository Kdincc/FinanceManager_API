using Task11.Domain.Common.Models;

namespace Task11.Domain.ExpenseFinanceOperationAggregate.ValueObjects
{
    public sealed class ExpenseFinanceOperationId : ValueObject
    {
        private ExpenseFinanceOperationId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; private set; }

        public static ExpenseFinanceOperationId CreateUniq() => new(Guid.NewGuid());

        public static ExpenseFinanceOperationId Create(string value) => new(Guid.Parse(value));

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
