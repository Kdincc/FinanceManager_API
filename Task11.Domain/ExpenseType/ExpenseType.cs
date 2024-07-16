using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Domain.Common.Entities;
using Task11.Domain.Common.ValueObjects;
using Task11.Domain.ExpenseType.ValueObjects;

namespace Task11.Domain.ExpenseType
{
    public sealed class ExpenseType(ExpenseTypeId id,
                                   string name,
                                   string description,
                                   Amount amount) : OperationType<ExpenseTypeId>(id, name, description, amount)
    {
    }
}
