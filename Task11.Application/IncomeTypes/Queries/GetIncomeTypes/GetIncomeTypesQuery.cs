using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task11.Application.IncomeTypes.Queries.GetIncomeTypes
{
    public record GetIncomeTypesQuery() : IRequest;
}
