using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task11.Contracts.ExpenseFinanceOperation
{
    public record UpdateExpenseFinanceOperationRequest(string ExpenseFinanceOperationId, string Name, string ExpenseTypeId, decimal Amount, DateTime Date);
    
}
