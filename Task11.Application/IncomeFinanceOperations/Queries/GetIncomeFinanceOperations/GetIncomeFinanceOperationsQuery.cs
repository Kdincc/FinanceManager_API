using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task11.Application.IncomeFinanceOperations.Queries.GetIncomeFinanceOperations
{
    public record GetIncomeFinanceOperationsQuery() : IRequest<IEnumerable<IncomeFinanceOperationResult>>;
}
