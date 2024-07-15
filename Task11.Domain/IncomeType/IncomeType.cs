using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Domain.Common.Entities;
using Task11.Domain.IncomeType.ValueObjects;

namespace Task11.Domain.IncomeType
{
    public sealed class IncomeType(IncomeTypeId id,
                                   string name,
                                   string description) : OperationType<IncomeTypeId>(id, name, description)
    {
    }
}
