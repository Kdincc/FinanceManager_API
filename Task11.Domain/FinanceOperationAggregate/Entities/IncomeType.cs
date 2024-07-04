﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Domain.FinanceOperationAggregate.ValueObjects;

namespace Task11.Domain.FinanceOperationAggregate.Entities
{
    public sealed class IncomeType(IncomeTypeId id,
                                   string name,
                                   string description,
                                   string sender) : OperationType<IncomeTypeId>(id, name, description)
    {
        public string Sender { get; private set; } = sender;
    }
}
