using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Domain.Common.Models;

namespace Task11.Domain.FinanceOperationAggregate.ValueObjects
{
    public sealed class Amount : ValueObject
    {
        public Amount(decimal value) 
        {
            if (value < 0)
            {
                ArgumentOutOfRangeException.ThrowIfLessThan(value, 0);
            }

            Value = Math.Round(value, 2);
        }

        public decimal Value { get; private set; }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
