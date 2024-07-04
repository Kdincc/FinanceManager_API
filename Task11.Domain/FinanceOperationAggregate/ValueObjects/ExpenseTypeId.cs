using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Domain.Common.Models;

namespace Task11.Domain.FinanceOperationAggregate.ValueObjects
{
    public sealed class ExpenseTypeId : ValueObject
    {
        private ExpenseTypeId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; private set; }

        public static ExpenseTypeId CreateUniq() => new(Guid.NewGuid());

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
