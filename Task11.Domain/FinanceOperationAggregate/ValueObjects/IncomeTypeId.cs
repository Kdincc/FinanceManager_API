using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task11.Domain.FinanceOperationAggregate.ValueObjects
{
    internal class IncomeTypeId
    {
        private IncomeTypeId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; private set; }

        public static IncomeTypeId CreateUniq() => new(Guid.NewGuid());

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
