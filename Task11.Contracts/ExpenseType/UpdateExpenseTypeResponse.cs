using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task11.Contracts.ExpenseType
{
    public record UpdateExpenseTypeResponse(Guid Id, string Name, string Description);
}
