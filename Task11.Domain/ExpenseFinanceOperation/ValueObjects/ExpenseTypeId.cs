using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Domain.Common.Models;
using Task11.Domain.Common.ValueObjects;

namespace Task11.Domain.ExpenseFinanceOperation.ValueObjects
{
    public sealed class ExpenseTypeId : OperationTypeId
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
