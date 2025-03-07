﻿using Task11.Domain.Common.Models;
using Task11.Domain.IncomeType.ValueObjects;

namespace Task11.Domain.IncomeType
{
    public sealed class IncomeType : OperationType<IncomeTypeId>
    {
        public IncomeType(IncomeTypeId id,
                                   string name,
                                   string description) : base(id, name, description)
        {
        }
    }
}
