using Task11.Domain.Common.Models;
using Task11.Domain.Common.ValueObjects;

namespace Task11.Domain.IncomeType.ValueObjects
{
    public class IncomeTypeId : ValueObject
    {
        private IncomeTypeId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; private set; }

        public static IncomeTypeId CreateUniq() => new(Guid.NewGuid());

        public static IncomeTypeId Create(string value) => new(Guid.Parse(value));

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
