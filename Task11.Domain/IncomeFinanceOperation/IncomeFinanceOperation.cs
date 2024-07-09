using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Domain.Common.Models;
using Task11.Domain.Common.ValueObjects;
using Task11.Domain.IncomeFinanceOperation.ValueObjects;

namespace Task11.Domain.IncomeFinanceOperation
{
    public sealed class IncomeFinanceOperation(ExpenseFinanceOperationId id, DateTime date, IncomeTypeId incomeTypeId, Amount amount) : AggregateRoot<ExpenseFinanceOperationId>(id)
    {
        public IncomeTypeId IncomeTypeId => incomeTypeId;

        public DateTime Date => date;

        public Amount Amount => amount;
    }
}
