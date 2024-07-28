using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Domain.ExpenseFinanceOperationAggregate;
using Task11.Domain.ExpenseFinanceOperationAggregate.ValueObjects;

namespace Task11.Application.Common.Persistance
{
    public interface IExpenseFinanceOperationRepository : IRepository<ExpenseFinanceOperation, ExpenseFinanceOperationId>
    {
    }
}
