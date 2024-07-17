using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task11.Contracts.ExpenseFinanceOperation
{
    public record CreateExpenseFinanceOperationRequest(DateTime Date, string ExpenseTypeId, decimal Amount, string Name);
}
