using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task11.Contracts.ExpenseType
{
    public record CreateExpenseTypeRequest(string Name, string Description);
}
