using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Domain.Common.ValueObjects;
using Task11.Domain.IncomeFinanceOperationAggregate;
using Task11.Domain.IncomeFinanceOperationAggregate.ValueObjects;
using Task11.Domain.IncomeType.ValueObjects;

namespace Task11.Application.Common.DTOs
{
    public sealed class IncomeFinanceOperationDto(IncomeFinanceOperation incomeFinanceOperation)
    {
        public IncomeFinanceOperationId Id => incomeFinanceOperation.Id;

        public DateTime Date => incomeFinanceOperation.Date;

        public Amount Amount => incomeFinanceOperation.Amount;

        public string Name => incomeFinanceOperation.Name;

        public IncomeTypeId IncomeTypeId => incomeFinanceOperation.IncomeTypeId;
    }
}
