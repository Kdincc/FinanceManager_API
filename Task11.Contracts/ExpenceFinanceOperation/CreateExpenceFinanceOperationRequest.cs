using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task11.Contracts.ExpenceFinanceOperation
{
    public record CreateExpenceFinanceOperationRequest(DateTime Date, string ExpenceTypeId, decimal Amount, string Name);
}
