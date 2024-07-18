using Task11.Domain.Common.Models;

namespace Task11.Domain.Common.ValueObjects
{
    public sealed class Amount : ValueObject
    {
        private Amount(decimal value)
        {
            if (value < 0)
            {
                ArgumentOutOfRangeException.ThrowIfLessThan(value, 0);
            }

            Value = Math.Round(value, 2);
        }

        public static Amount Create(decimal value) => new(value);

        public decimal Value { get; private set; }

        public static Amount operator +(Amount a, Amount b) => Create(a.Value + b.Value);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
