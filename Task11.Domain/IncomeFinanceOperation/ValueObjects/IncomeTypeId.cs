using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Domain.Common.Models;
using Task11.Domain.Common.ValueObjects;

namespace Task11.Domain.IncomeFinanceOperation.ValueObjects
{
    public class IncomeTypeId : OperationTypeId
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
