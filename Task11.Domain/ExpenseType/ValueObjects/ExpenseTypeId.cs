using Task11.Domain.Common.Models;
using Task11.Domain.Common.ValueObjects;

namespace Task11.Domain.ExpenseType.ValueObjects
{
    public sealed class ExpenseTypeId : ValueObject
    {
        private ExpenseTypeId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; private set; }

        public static ExpenseTypeId CreateUniq() => new(Guid.NewGuid());

        public static ExpenseTypeId Create(string value) => new(Guid.Parse(value));

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
