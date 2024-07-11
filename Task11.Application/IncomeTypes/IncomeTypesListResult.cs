using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Domain.IncomeType;

namespace Task11.Application.IncomeTypes
{
    public record IncomeTypesListResult(List<IncomeType> IncomeTypes);
}
