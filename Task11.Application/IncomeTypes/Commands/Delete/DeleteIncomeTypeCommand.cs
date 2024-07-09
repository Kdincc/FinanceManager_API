using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Domain.IncomeFinanceOperation.ValueObjects;

namespace Task11.Application.IncomeTypes.Commands.Delete
{
    public sealed record DeleteIncomeTypeCommand(IncomeTypeId IncomeTypeId);
}
