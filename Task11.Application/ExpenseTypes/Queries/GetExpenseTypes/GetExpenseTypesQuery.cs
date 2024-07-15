using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task11.Application.ExpenseTypes.Queries.GetExpenseTypes
{
    public record GetExpenseTypesQuery() : IRequest<IEnumerable<ExpenseTypesResult>>;
}
