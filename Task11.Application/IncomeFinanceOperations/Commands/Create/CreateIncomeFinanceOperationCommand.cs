using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Domain.Common.ValueObjects;
using Task11.Domain.IncomeType.ValueObjects;

namespace Task11.Application.IncomeFinanceOperations.Commands.Create
{
    public record CreateIncomeFinanceOperationCommand(DateOnly Date, IncomeTypeId IncomeTypeId, Amount Amount, string Name) : IRequest<ErrorOr<IncomeFinanceOperationResult>>;
}
