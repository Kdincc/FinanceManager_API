using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Domain.IncomeType;
using Task11.Domain.IncomeType.ValueObjects;

namespace Task11.Application.Common.Persistance
{
    public interface IIncomeTypeRepository : IRepository<IncomeType, IncomeTypeId>
    {
    }
}
