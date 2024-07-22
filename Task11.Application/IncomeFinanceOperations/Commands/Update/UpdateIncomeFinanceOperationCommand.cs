using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Domain.Common.ValueObjects;
using Task11.Domain.IncomeFinanceOperationAggregate.ValueObjects;
using Task11.Domain.IncomeType.ValueObjects;

namespace Task11.Application.IncomeFinanceOperations.Commands.Update
{
    public record UpdateIncomeFinanceOperationCommand(
        IncomeFinanceOperationId IncomeFinanceOperationId,
        IncomeTypeId IncomeTypeId,
        string Date,
        Amount Amount,
        string Name) : IRequest<ErrorOr<IncomeFinanceOperationResult>>;
}
