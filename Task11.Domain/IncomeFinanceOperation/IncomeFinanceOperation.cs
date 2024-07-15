using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Domain.Common.Models;
using Task11.Domain.Common.ValueObjects;
using Task11.Domain.IncomeFinanceOperation.ValueObjects;
using Task11.Domain.IncomeType.ValueObjects;

namespace Task11.Domain.IncomeFinanceOperation
{
    public sealed class IncomeFinanceOperation(
        IncomeFinanceOperationId id,
        DateTime date,
        IncomeTypeId incomeTypeId,
        Amount amount,
        string name) : AggregateRoot<IncomeFinanceOperationId>(id)
    {
        public IncomeTypeId IncomeTypeId { get; private set; } = incomeTypeId;

        public DateTime Date { get; private set; } = date;

        public Amount Amount { get; private set; } = amount;

        public string Name { get; private set; } = name;
    }
}
