﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Domain.FinanceOperationAggregate.ValueObjects;

namespace Task11.Domain.FinanceOperationAggregate.Entities
{
    public sealed class ExpenseType(ExpenseTypeId id,
                                   string name,
                                   string description) : OperationType<ExpenseTypeId>(id, name, description)
    {
    }
}
