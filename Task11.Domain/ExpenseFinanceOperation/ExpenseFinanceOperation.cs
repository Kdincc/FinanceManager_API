using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Domain.Common.Models;
using Task11.Domain.Common.ValueObjects;
using Task11.Domain.ExpenseFinanceOperation.ValueObjects;

namespace Task11.Domain.ExpenseFinanceOperation
{
    public sealed class ExpenseFinanceOperation(ExpenseFinanceOperationId id, DateTime date, ExpenseTypeId expenseTypeId, Amount amount) : AggregateRoot<ExpenseFinanceOperationId>(id)
    {
        public ExpenseTypeId ExpenseTypeId => expenseTypeId;

        public DateTime Date => date;

        public Amount Amount => amount;
    }
}
